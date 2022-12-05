using GalaSoft.MvvmLight;
using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Threading.Tasks;

namespace GZKL.Client.UI.ViewsModels
{
    public class LoginViewModel : ViewModelBase
    {

        public LoginViewModel()
        {
            //var loginModel = GetLoginSetting();
            //if (loginModel.RememberPassword)
            //{
            //    UserName= loginModel.UserName;
            //    Password= loginModel.Password;
            //}

            //this.AutoLogin = loginModel.AutoLogin;
            //this.RememberPassword = loginModel.RememberPassword;
        }

        #region =====Data
        /// <summary>
        /// 用户名
        /// </summary>
        private string userName = "";
        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); LoginError = ""; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        private string password = "";
        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(); LoginError = ""; }
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

        /// <summary>
        /// 登录错误
        /// </summary>
        private string loginError = "";
        public string LoginError
        {
            get { return loginError; }
            set { loginError = value; RaisePropertyChanged(); }
        }

        #endregion

        #region ====Command

        #endregion

        public void LoginMethod(object o)
        {
            var loginResult = new LoginSuccessModel();

            try
            {
                //执行登录
                loginResult = DbLogin();

                //保存登录设置
                this.SaveLoginSetting(new LoginModel()
                {
                    AutoLogin = autoLogin,
                    RememberPassword = rememberPassword,
                    UserName = userName,
                    Password = password
                });

                var mainWindow = new MainWindow(loginResult);

                //关闭登录窗口
                (o as System.Windows.Window).Close();

                if (loginResult.User?.Id > 0)
                {
                    //开启主画面
                    var result = mainWindow.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                LoginError = ex?.Message;
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public LoginSuccessModel DbLogin()
        {
            var result = new LoginSuccessModel();

            //用户
            var sql = @"SELECT TOP 1 * FROM [sys_user] WHERE [is_deleted]=0 AND [name]=@userName AND [password]=@password";
            var parameters = new SqlParameter[] { new SqlParameter("@userName", userName), new SqlParameter("@password", Password) };

            using (var dt = SQLHelper.GetDataTable(sql, parameters))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    var dataRow = dt.Rows[0];

                    result.User = new UserModel()
                    {
                        Id = Convert.ToInt64(dataRow["id"]),
                        Name = dataRow["name"].ToString(),
                        Email = dataRow["email"].ToString(),
                        Phone = dataRow["phone"].ToString(),
                        HeadImg = dataRow["head_img"].ToString(),
                        Sex = Convert.ToInt32(dataRow["sex"]),
                        Birthday = Convert.ToDateTime(dataRow["birthday"] ?? DateTime.MinValue),
                        IsEnabled = Convert.ToInt32(dataRow["is_enabled"]),
                        CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                        UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                    };
                }
            }

            if (result.User == null || result.User.Id == 0)
            {
                throw new Exception("账号或密码不对！");
            }
            else if (result.User.IsEnabled == 0)
            {
                throw new Exception("当前账号已停用！");
            }


            //角色
            sql = @"SELECT TOP 1 r.* FROM [sys_role] r INNER JOIN [sys_user_role] ur ON r.id=ur.role_id WHERE r.[is_deleted]=0 AND ur.[is_deleted]=0 AND ur.[user_id]=@userId";
            parameters = new SqlParameter[] { new SqlParameter("@userId", result.User.Id) };

            using (var dt = SQLHelper.GetDataTable(sql, parameters))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    var dataRow = dt.Rows[0];

                    result.Role = new RoleModel()
                    {
                        Id = Convert.ToInt64(dataRow["id"]),
                        Name = dataRow["name"].ToString(),
                        Remark = dataRow["remark"].ToString(),
                        CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                        UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                    };
                }
            }

            if (result.Role == null || result.Role.Id == 0)
            {
                throw new Exception("当前账号未分配角色，请联系软件开发商！");
            }

            //权限
            sql = @"SELECT m.* FROM [sys_role_menu] rm INNER JOIN [sys_menu] m ON rm.menu_id=m.id WHERE rm.is_deleted=0 AND m.is_deleted=0 AND rm.role_id=@roleId";
            parameters = new SqlParameter[] { new SqlParameter("@roleId", result.Role.Id) };

            using (var dt = SQLHelper.GetDataTable(sql, parameters))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.Menus = new List<MenuModel>();
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        result.Menus.Add(new MenuModel()
                        {

                            Id = Convert.ToInt64(dataRow["id"]),
                            ParentId = Convert.ToInt64(dataRow["parent_id"]),
                            Name = dataRow["name"].ToString(),
                            Url = dataRow["url"].ToString(),
                            Icon = dataRow["icon"].ToString(),
                            Type = Convert.ToInt32(dataRow["type"]),
                            Sort = Convert.ToInt32(dataRow["sort"]),
                            IsEnabled = Convert.ToInt32(dataRow["is_enabled"]),
                            CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                            UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),

                        });
                    }
                }
            }


            return result;
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
            var parameters = new SqlParameter[] { new SqlParameter("@category", hostName) };

            using (var dt = SQLHelper.GetDataTable(sql, parameters))
            {
                foreach (DataRow dr in dt?.Rows)
                {
                    var category = dr["value"].ToString();
                    switch (category)
                    {
                        case "UserName":
                            result.UserName = dr["text"].ToString();
                            break;
                        case "Password":
                            result.Password = dr["text"].ToString();
                            break;
                        case "AutoLogin":
                            result.AutoLogin = Convert.ToBoolean(dr["text"] ?? "true");
                            break;
                        case "RememberPassword":
                            result.RememberPassword = Convert.ToBoolean(dr["text"] ?? "false");
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
            Task.Run(() =>
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

                var result = SQLHelper.ExecuteNonQuery(sql);
            });
        }
    }
}
