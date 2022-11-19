using CommonServiceLocator;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Client.UI.Common
{
    internal class RegisterInfo
    {
        private static RegisterInfo instance;

        /// <summary>
        /// 注册码
        /// </summary>
        public string RegCode { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegTime { get; set; }

        public static RegisterInfo GetInstance()
        {
            lock ("instance")
            {
                if (instance == null)
                {
                    instance = new RegisterInfo();
                }

                return instance;
            }
        }

        public RegisterInfo GetRegisterInfo(string fullName)
        {
            var registerInfo = new RegisterInfo();

            try
            {
                (string registerCode, string registerTime, string hostName, string cpu) = ServiceLocator.Current.GetInstance<RegisterViewModel>().GetRegisterInfo(fullName);

                RegCode = registerCode;

                if (!string.IsNullOrEmpty(registerTime) && DateTime.TryParse(registerTime, out var tempRegisterTime))
                {
                    RegTime = tempRegisterTime;
                }
                else
                {
                    RegTime = DateTime.MinValue;
                }

            }
            catch { }

            return registerInfo;
        }

        public void Dispose()
        {

        }
    }
}
