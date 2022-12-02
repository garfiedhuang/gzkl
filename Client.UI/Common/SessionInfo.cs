using GZKL.Client.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Client.UI.Common
{
    public class SessionInfo
    {
        private static SessionInfo instance = null;
        private static object obj = new object();
        private SessionInfo() { }
        public static SessionInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (obj)
                    {
                        if (instance == null)
                        {
                            instance = new SessionInfo();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        internal UserModel UserInfo { get; set; }

        /// <summary>
        /// 电脑信息
        /// </summary>
        internal ComputerInfo ComputerInfo { get; set; }

        /// <summary>
        /// 注册信息
        /// </summary>
        internal RegisterInfo RegisterInfo { get; set; }
    }
}
