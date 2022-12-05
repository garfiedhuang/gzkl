using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GZKL.Client.UI.Common;
using MessageBox = HandyControl.Controls.MessageBox;
using System;
using System.Text;
using System.Windows;
using System.Data;
using System.Data.SqlClient;

namespace GZKL.Client.UI.ViewsModels
{
    public class RegisterViewModel : ViewModelBase
    {
        #region Construct and property

        /// <summary>
        /// 构造函数
        /// </summary>
        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(this.Register);
        }

        private string status;
        /// <summary>
        /// 注册状态 未注册/已注册
        /// </summary>
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value; RaisePropertyChanged();
            }
        }

        private string hostName;
        /// <summary>
        /// 主机名称
        /// </summary>
        public string HostName
        {
            get { return hostName; }
            set { hostName = value; RaisePropertyChanged(); }
        }

        private string cpu;
        /// <summary>
        /// cpu
        /// </summary>
        public string CPU
        {
            get { return cpu; }
            set { cpu = value; RaisePropertyChanged(); }
        }

        private string fullName;
        /// <summary>
        /// 本机信息
        /// </summary>
        public string FullName
        {
            get
            {
                return fullName = $"{HostName}-{CPU}";
            }
            set { fullName = value; RaisePropertyChanged(); }
        }

        private string registerCode;
        /// <summary>
        /// 注册码
        /// </summary>
        public string RegisterCode
        {
            get { return registerCode; }
            set { registerCode = value; RaisePropertyChanged(); }
        }

        private string registerTime;
        /// <summary>
        /// 注册时间
        /// </summary>
        public string RegisterTime
        {
            get { return registerTime; }
            set
            {
                registerTime = value; RaisePropertyChanged();
            }
        }

        private Visibility registerButtonVisibility = Visibility.Hidden;
        /// <summary>
        /// 注册按钮可见
        /// </summary>
        public Visibility RegisterButtonVisibility
        {
            get { return registerButtonVisibility; }
            set
            {
                registerButtonVisibility = value; RaisePropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// 注册
        /// </summary>
        public RelayCommand RegisterCommand { get; set; }

        #endregion


        #region Command implement

        /// <summary>
        /// 获取注册信息
        /// </summary>
        /// <param name="fullName">格式：HostName-CPU</param>
        /// <returns></returns>
        public (string registerCode, string registerTime, string hostName, string cpu) GetRegisterInfo(string fullName)
        {
            try
            {
                var sql = new StringBuilder(@"SELECT [id],[category],[value],[text],[remark]
                ,[is_enabled],[is_deleted],[create_dt],[create_user_id],[update_dt],[update_user_id]
                FROM [dbo].[sys_config] WHERE [category]=@category AND [is_deleted]=0");

                var parameters = new SqlParameter[] { new SqlParameter("@category", $"System-{fullName}") };

                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data != null && data.Rows.Count == 3)
                    {
                        var value = string.Empty;
                        foreach (DataRow dr in data.Rows)
                        {
                            value = dr["value"].ToString();

                            switch (value)
                            {
                                case "Register":
                                    registerCode = dr["text"].ToString();
                                    registerTime = dr["remark"].ToString();
                                    break;
                                case "HostName":
                                    hostName = dr["text"].ToString();
                                    break;
                                case "CPU":
                                    cpu = dr["text"].ToString();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }

            return (registerCode, registerTime,hostName,cpu);
        }

        /// <summary>
        /// 注册
        /// </summary>
        public void Register()
        {
            try
            {
                string sql = "";
                SqlParameter[] parameters = null;
                int rowCount = 0;

                //判断是否存在注册信息？
                sql = "SELECT COUNT(1) FROM [dbo].[sys_config] WHERE [category]=@category AND [value]=@value AND [is_deleted]=0";
                parameters = new SqlParameter[] { new SqlParameter("@category", $"System-{FullName}"), new SqlParameter("@value", $"Register") };

                rowCount = Convert.ToInt32(SQLHelper.ExecuteScalar(sql, parameters) ?? "0");

                if (rowCount > 0)
                {
                    MessageBox.Show($"当前电脑【{FullName}】已存在注册记录，请勿重复注册", "提示信息");
                    return;
                }

                var result = 0;
                var registerCode = SecurityHelper.DESEncrypt(FullName);
                var registerTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                var value = string.Empty;
                var text = string.Empty;
                var remark = string.Empty;

                //注册信息写入数据库
                sql = @"INSERT INTO [dbo].[sys_config]
           ([category]
           ,[value]
           ,[text]
           ,[remark]
           ,[is_enabled]
           ,[is_deleted]
           ,[create_dt]
           ,[create_user_id]
           ,[update_dt]
           ,[update_user_id])
     VALUES
           (@category
           ,@value
           ,@text
           ,@remark
           ,@is_enabled
           ,0
           ,@create_dt
           ,@user_id
           ,@create_dt
           ,@user_id)";

                for (var i = 0; i < 3; i++)
                {
                    switch (i)
                    {
                        case 0:
                            value = "Register";
                            text = registerCode;
                            remark = registerTime;
                            break;
                        case 1:
                            value = "HostName";
                            text = HostName;
                            remark = "系统信息";
                            break;
                        case 2:
                            value = "CPU";
                            text = CPU;
                            remark = "系统信息";
                            break;
                    }
                    parameters = new SqlParameter[] {
                    new SqlParameter("@category", $"System-{FullName}"),
                    new SqlParameter("@value", value),
                    new SqlParameter("@text", text),
                    new SqlParameter("@remark", remark),
                    new SqlParameter("@is_enabled", 1),
                    new SqlParameter("@create_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SqlParameter("@user_id", SessionInfo.Instance.UserInfo.Id)
                    };

                    result += SQLHelper.ExecuteNonQuery(sql, parameters);
                }

                if (result == 3)
                {
                    RegisterCode = registerCode;
                    RegisterTime = registerTime;
                    Status = "已注册";
                    RegisterButtonVisibility = Visibility.Hidden;
                    var res = MessageBox.Show($"当前电脑{HostName}注册成功", "提示信息");
                }
                else
                {
                    throw new Exception($"当前电脑【{FullName}】注册失败，请与管理员联系");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        #endregion

        #region Privates

        #endregion
    }
}
