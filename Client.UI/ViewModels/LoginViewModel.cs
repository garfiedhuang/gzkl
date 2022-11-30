using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GZKL.Client.UI.Views;
using GZKL.Client.UI.Common;
using System.Net;
using System.Data.SqlClient;
using GZKL.Client.UI.Models;
using System.Data;

namespace GZKL.Client.UI.ViewsModels
{
     public class LoginViewModel: ViewModelBase
    {

        public LoginViewModel()
        {
            loginCommand = new RelayCommand<object>(LoginMethod);

            var loginModel = GetLoginSetting();

            if (loginModel.RememberPassword)
            {
                UserName = loginModel.UserName;
                PassWord = loginModel.Password;
            }

            this.AutoLogin = loginModel.AutoLogin;
            this.RememberPassword = loginModel.RememberPassword;

        }

        #region =====Data
        /// <summary>
        /// 用户名
        /// </summary>
        private string userName="admin";
        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 密码
        /// </summary>
        private string passWord="123456";
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 自动登录
        /// </summary>
        private bool autoLogin = false;
        public bool AutoLogin
        {
            get { return autoLogin; }
            set { autoLogin = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 记住密码
        /// </summary>
        private bool rememberPassword = false;
        public bool RememberPassword
        {
            get { return rememberPassword; }
            set { rememberPassword = value; RaisePropertyChanged(); }
        }

        #endregion

        #region ====Command
        public RelayCommand<object> loginCommand { get; set; }
        #endregion

        private void LoginMethod(object o)
        {
            var values = (object[])o;
            var psdControl =(PasswordBox) values[1];
            string psdStr = psdControl.Password;
            if (string.IsNullOrEmpty(userName))
            {
                Messenger.Default.Send("登录名不能为空", "UserNameErrorToken");
                return;
            }
            if (string.IsNullOrEmpty(psdStr))
            {
                psdControl.IsError = true;
                psdControl.ErrorStr = "密码不能为空";
                return;
            }

            PassWord = psdStr;

            //保存登录会话
            SessionInfo.Instance.Session = new UserModel()
            {
                Id = 1,
                Name = "admin",
                Phone = "18611111234"
            };

            MainWindow mainView = new MainWindow();
            (values[0] as System.Windows.Window).Close();
            mainView.ShowDialog();
        }

        public LoginSuccessModel Login()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取登录设置
        /// </summary>
        /// <returns></returns>
        public LoginModel GetLoginSetting()
        {
            var result = new LoginModel();
            var hostName = Dns.GetHostName().ToUpper();

            var sql = @"SELECT * FROM [sys_config] WHERE [category]=@category AND [is_deleted]=0";
            var parameters = new SqlParameter[] { new SqlParameter("@category",hostName) };

            using (var dt = SQLHelper.GetDataTable(sql, parameters))
            {
                foreach (DataRow dr in dt?.Rows)
                {
                    var category = dr["category"].ToString();
                    switch (category)
                    {
                        case "UserName":
                            result.UserName= dr["category"].ToString();
                            break;
                        case "Password":
                            result.UserName = dr["category"].ToString();
                            break;
                        case "AutoLogin":
                            result.AutoLogin =Convert.ToBoolean(dr["AutoLogin"] ??"false");
                            break;
                        case "RememberPassword":
                            result.RememberPassword = Convert.ToBoolean(dr["RememberPassword"] ?? "false");
                            break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 保存登录设置
        /// </summary>
        /// <param name="loginModel"></param>
        public void SaveLoginSetting(LoginModel loginModel)
        {
            var hostName = Dns.GetHostName().ToUpper();

            var sql = $@"BEGIN
   DECLARE @iRC INT;

   SELECT @iRC=COUNT(1) FROM [sys_config] WHERE [category]='{hostName}' AND [value]='UserName' AND [is_deleted]=0;

   IF @iRC=0
      BEGIN
	  INSERT INTO [dbo].[sys_config]
           ([category],[value],[text],[remark]
           ,[is_enabled],[is_deleted],[create_dt],[create_user_id],[update_dt],[update_user_id])
      VALUES
           ('{hostName}','UserName','{loginModel.UserName}','登录设置，用户名'
           ,1,0,getdate(),1,getdate(),1)
	  END
	ELSE
	  BEGIN
	  UPDATE [dbo].[sys_config] SET [text]='{loginModel.UserName}',[update_dt]=GETDATE() WHERE  [category]='{hostName}' AND [value]='UserName' AND [is_deleted]=0;
	  END

   SELECT @iRC=COUNT(1) FROM [sys_config] WHERE [category]='{hostName}' AND [value]='Password' AND [is_deleted]=0;

   IF @iRC=0
      BEGIN
	  INSERT INTO [dbo].[sys_config]
           ([category],[value],[text],[remark]
           ,[is_enabled],[is_deleted],[create_dt],[create_user_id],[update_dt],[update_user_id])
      VALUES
           ('{hostName}','Password','{loginModel.Password}','登录设置，用户密码'
           ,1,0,getdate(),1,getdate(),1)
	  END
	ELSE
	  BEGIN
	  UPDATE [dbo].[sys_config] SET [text]='{loginModel.Password}',[update_dt]=GETDATE() WHERE  [category]='{hostName}' AND [value]='Password' AND [is_deleted]=0;
	  END

   SELECT @iRC=COUNT(1) FROM [sys_config] WHERE [category]='{hostName}' AND [value]='AutoLogin' AND [is_deleted]=0;

   IF @iRC=0
      BEGIN
	  INSERT INTO [dbo].[sys_config]
           ([category],[value],[text],[remark]
           ,[is_enabled],[is_deleted],[create_dt],[create_user_id],[update_dt],[update_user_id])
      VALUES
           ('{hostName}','AutoLogin','{loginModel.AutoLogin}','登录设置，是否自动登录'
           ,1,0,getdate(),1,getdate(),1)
	  END
	ELSE
	  BEGIN
	  UPDATE [dbo].[sys_config] SET [text]='{loginModel.AutoLogin}',[update_dt]=GETDATE() WHERE  [category]='{hostName}' AND [value]='AutoLogin' AND [is_deleted]=0;
	  END

   SELECT @iRC=COUNT(1) FROM [sys_config] WHERE [category]='{hostName}' AND [value]='RememberPassword' AND [is_deleted]=0;

   IF @iRC=0
      BEGIN
	  INSERT INTO [dbo].[sys_config]
           ([category],[value],[text],[remark]
           ,[is_enabled],[is_deleted],[create_dt],[create_user_id],[update_dt],[update_user_id])
      VALUES
           ('{hostName}','RememberPassword','{loginModel.RememberPassword}','登录设置，是否记住密码'
           ,1,0,getdate(),1,getdate(),1)
	  END
	ELSE
	  BEGIN
	  UPDATE [dbo].[sys_config] SET [text]='{loginModel.RememberPassword}',[update_dt]=GETDATE() WHERE  [category]='{hostName}' AND [value]='RememberPassword' AND [is_deleted]=0;
	  END
END";

            var result =SQLHelper.ExecuteNonQuery(sql);
        }
    }
}
