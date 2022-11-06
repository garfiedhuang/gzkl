using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Cilent.UI.Models
{
    /// <summary>
    /// 用户模型
    /// </summary>
    public class UserModel : ObservableObject
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        public long RowNum { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImg { get; set; }

        /// <summary>
        /// 性别 0-未知 1-男 2-女
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; } = DateTime.Now;

        /// <summary>
        /// 是否启用 0-否 1-是
        /// </summary>
        public int IsEnabled { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDt { get; set; }

        /// <summary>
        /// 是否选中?
        /// </summary>
        private bool isSelected = false;

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; RaisePropertyChanged("IsSelected"); }
        }

    }
}
