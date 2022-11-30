using GalaSoft.MvvmLight;
using GZKL.Client.UI.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Client.UI.Models
{
    /// <summary>
    /// 菜单模型
    /// </summary>
    public class MenuModel : ObservableObject
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 菜单名称,当前父菜单下必须唯一
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { set; get; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否启用 0-否 1-是
        /// </summary>
        public int IsEnabled { get; set; } = 1;

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

    public class MenuDataModel : ObservableObject
    {
        public int Index { get; set; }


        private string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }


        /// <summary>
        /// 是否选中?
        /// </summary>
        private bool isSelected = false;

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; RaisePropertyChanged(); }
        }

        public bool IsExpanded { get; set; }

        public string Remark { get; set; }

        public MenuType Type { get; set; }

        public string ImgPath { get; set; }

        public ObservableCollection<MenuDataModel> DataList { get; set; }

        // Card
        public string Header { get; set; }

        public string Content { get; set; }

        public string Footer { get; set; }

        // Avatar
        public string DisplayName { get; set; }

        public string Link { get; set; }

        public string AvatarUri { get; set; }
    }

}
