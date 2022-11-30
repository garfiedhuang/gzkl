using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Client.UI.Models
{
    public class LoginModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = "";

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = "";

        /// <summary>
        /// 是否自动登录
        /// </summary>
        public bool AutoLogin { get; set; } = false;

        /// <summary>
        /// 是否记住密码
        /// </summary>
        public bool RememberPassword { get; set; } = false;
    }

    public class LoginSuccessModel
    {
        public UserModel User { get; set; }

        public RoleModel Role { get; set; }

        public List<MenuModel> Menus { get; set; }
    }
}
