using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;

namespace GZKL.Client.UI.Models
{
    /// <summary>
    /// 菜单页签模型
    /// </summary>
    public class MenuTabModel : ObservableObject
    {
        private string header;
        private FrameworkElement content;
        private string code;

        public string Header
        {
            get { return header; }
            set { header = value; RaisePropertyChanged(); }
        }

        public FrameworkElement Content
        {
            get { return content; }
            set { content = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 实际存储icon图标编码
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; RaisePropertyChanged(); }
        }
    }
}
