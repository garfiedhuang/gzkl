using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Client.UI.Models
{
    /// <summary>
    /// 接口基础模型
    /// </summary>
    public class InterfaceBaseModel : ObservableObject
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
        /// 接口名称
        /// </summary>
        public string InterfaceName { get; set; }

        private string _accessDbPath;
        /// <summary>
        /// 数据库路径
        /// </summary>
        public string AccessDbPath
        {
            get { return _accessDbPath; }
            set { _accessDbPath = value; RaisePropertyChanged(); }
        }

        private string _accessDbName;
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string AccessDbName
        {
            get { return _accessDbName; }
            set { _accessDbName = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 数据库用户
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// 数据库密码
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; } = "";

        /// <summary>
        /// 是否启用 0-否 1-是
        /// </summary>
        public int IsEnabled { get; set; } = 0;

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
