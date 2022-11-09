using GalaSoft.MvvmLight;
using GZKL.Client.UI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Client.UI.Models
{
    /// <summary>
    /// 注册模型
    /// </summary>
    public class RegisterModel
    {
        private string status;
        /// <summary>
        /// 注册状态 未注册/已注册
        /// </summary>
        public string Status
        {
            get
            {
                if (string.IsNullOrEmpty(RegisterCode))
                {
                    return status = "未注册";
                }
                else
                {
                    return status = "已注册";
                }
            }
            set
            {
                status = value;
            }
        }

        /// <summary>
        /// 主机名称
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// cpu
        /// </summary>
        public string CPU { get; set; }

        private string fullName;
        /// <summary>
        /// 本机信息
        /// </summary>
        public string FullName
        {
            get {
                return fullName=$"{HostName}-{CPU}";
            }
            set { fullName = value; }
        }

        /// <summary>
        /// 注册码
        /// </summary>
        public string RegisterCode { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public string RegisterTime { get; set; }
    }
}
