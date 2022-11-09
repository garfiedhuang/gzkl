using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GZKL.Client.UI.Models;
using System.Windows;
using HandyControl.Data;
using System.Data.SqlClient;
using GZKL.Client.UI.Common;
using System.Data;
using System.Windows.Controls;
using MessageBox = HandyControl.Controls.MessageBox;
using GZKL.Client.UI.Views.CollectMgt.Register;
using GalaSoft.MvvmLight.Messaging;

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

        /// <summary>
        /// 注册状态 未注册/已注册
        /// </summary>
        public string Status
        {
            get
            {
                if (string.IsNullOrEmpty(RegisterCode))
                {
                   return "未注册";
                }
                else
                {
                   return "已注册";
                }
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
            set { fullName = value;RaisePropertyChanged(); }
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
        public (string, string) GetRegisterInfo(string fullName)
        {
            var registerCode = string.Empty;
            var registerTime = string.Empty;

            try
            {
                var sql = new StringBuilder(@"SELECT [id],[category],[value],[text],[remark]
                ,[is_enabled],[is_deleted],[create_dt],[create_user_id],[update_dt],[update_user_id]
                FROM [dbo].[sys_config] WHERE [category]='System' AND [Value]=@value AND [is_deleted]=0");

                var parameters = new SqlParameter[] { new SqlParameter("@value", $"Register-{fullName}") };

                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data != null && data.Rows.Count > 0)
                    {
                        registerCode = data.Rows[0]["text"].ToString();
                        registerTime = data.Rows[0]["remark"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }

            return (registerCode, registerTime);
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
                parameters = new SqlParameter[] { new SqlParameter("@category", "System"), new SqlParameter("@value", $"Register-{FullName}") };

                rowCount = Convert.ToInt32(SQLHelper.ExecuteScalar(sql, parameters) ?? "0");

                if (rowCount > 0)
                {
                    MessageBox.Show($"当前电脑【{FullName}】已存在注册记录，请勿重复注册", "提示信息");
                    return;
                }

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

                parameters = new SqlParameter[] {
                    new SqlParameter("@category", "System"),
                    new SqlParameter("@value", $"Register-{FullName}"),
                    new SqlParameter("@text", model.Text),
                    new SqlParameter("@remark", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SqlParameter("@is_enabled", 1),
                    new SqlParameter("@create_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SqlParameter("@user_id", SessionInfo.Instance.Session.Id)
                };

                var result = SQLHelper.ExecuteNonQuery(sql, parameters);




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
