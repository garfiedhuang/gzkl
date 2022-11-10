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
using GZKL.Client.UI.Views.CollectMgt.Parameter;
using GalaSoft.MvvmLight.Messaging;

namespace GZKL.Client.UI.ViewsModels
{
    public class ParameterViewModel : ViewModelBase
    {
        #region Construct and property

        /// <summary>
        /// 构造函数
        /// </summary>
        public ParameterViewModel()
        {
            ParameterCommand = new RelayCommand(this.Parameter);
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

        private string parameterCode;
        /// <summary>
        /// 注册码
        /// </summary>
        public string ParameterCode
        {
            get { return parameterCode; }
            set { parameterCode = value; RaisePropertyChanged(); }
        }

        private string parameterTime;
        /// <summary>
        /// 注册时间
        /// </summary>
        public string ParameterTime
        {
            get { return parameterTime; }
            set
            {
                parameterTime = value; RaisePropertyChanged();
            }
        }

        private Visibility parameterButtonVisibility = Visibility.Hidden;
        /// <summary>
        /// 注册按钮可见
        /// </summary>
        public Visibility ParameterButtonVisibility
        {
            get { return parameterButtonVisibility; }
            set
            {
                parameterButtonVisibility = value; RaisePropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// 注册
        /// </summary>
        public RelayCommand ParameterCommand { get; set; }

        #endregion


        #region Command implement

        /// <summary>
        /// 获取注册信息
        /// </summary>
        /// <param name="fullName">格式：HostName-CPU</param>
        /// <returns></returns>
        public (string, string) GetParameterInfo(string fullName)
        {
            var ParameterCode = string.Empty;
            var ParameterTime = string.Empty;

            try
            {
                var sql = new StringBuilder(@"SELECT [id],[category],[value],[text],[remark]
                ,[is_enabled],[is_deleted],[create_dt],[create_user_id],[update_dt],[update_user_id]
                FROM [dbo].[sys_config] WHERE [category]='System' AND [Value]=@value AND [is_deleted]=0");

                var parameters = new SqlParameter[] { new SqlParameter("@value", $"Parameter-{fullName}") };

                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data != null && data.Rows.Count > 0)
                    {
                        ParameterCode = data.Rows[0]["text"].ToString();
                        ParameterTime = data.Rows[0]["remark"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }

            return (ParameterCode, ParameterTime);
        }

        /// <summary>
        /// 注册
        /// </summary>
        public void Parameter()
        {
            try
            {
                string sql = "";
                SqlParameter[] parameters = null;
                int rowCount = 0;

                //判断是否存在注册信息？
                sql = "SELECT COUNT(1) FROM [dbo].[sys_config] WHERE [category]=@category AND [value]=@value AND [is_deleted]=0";
                parameters = new SqlParameter[] { new SqlParameter("@category", "System"), new SqlParameter("@value", $"Parameter-{FullName}") };

                rowCount = Convert.ToInt32(SQLHelper.ExecuteScalar(sql, parameters) ?? "0");

                if (rowCount > 0)
                {
                    MessageBox.Show($"当前电脑【{FullName}】已存在注册记录，请勿重复注册", "提示信息");
                    return;
                }

                var parameterCode = SecurityHelper.DESEncrypt(FullName);
                var parameterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

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
                    new SqlParameter("@value", $"Parameter-{FullName}"),
                    new SqlParameter("@text", parameterCode),
                    new SqlParameter("@remark", parameterTime),
                    new SqlParameter("@is_enabled", 1),
                    new SqlParameter("@create_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SqlParameter("@user_id", SessionInfo.Instance.Session.Id)
                };

                var result = SQLHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    ParameterCode = parameterCode;
                    ParameterTime = parameterTime;
                    Status = "已注册";
                    ParameterButtonVisibility = Visibility.Hidden;
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
