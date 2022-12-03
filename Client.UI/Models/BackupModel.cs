using GalaSoft.MvvmLight;
using System;
using System.ComponentModel;

namespace GZKL.Client.UI.Models
{
    /// <summary>
    /// 参数模型
    /// </summary>
    public class BackupModel : ObservableObject
    {
        public BackupModel()
        { }

        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        public long RowNum { get; set; }

        /// <summary>
        /// 备份编号
        /// </summary>
        public string BackupNo { get; set; }

        /// <summary>
        /// 备份时间
        /// </summary>
        public DateTime? BackupTime { get; set; }

        private string savePath;
        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath
        {
            get { return savePath; }
            set { savePath = value;RaisePropertyChanged(); }
        }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; } = "";

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
}
