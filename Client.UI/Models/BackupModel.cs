using GalaSoft.MvvmLight;
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

        private string savePath = "";
        /// <summary>
        /// 保存路径a
        /// </summary>
        [Description("保存路径")]
        public string SavePath
        {
            get { return savePath; }
            set { savePath = value; RaisePropertyChanged(); }
        }
    }
}
