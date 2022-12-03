using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.Views.CollectMgt.Clear;
using GZKL.Client.UI.ViewsModels;
using MessageBox = HandyControl.Controls.MessageBox;

namespace GZKL.Client.UI.Views.CollectMgt.Backup
{
    /// <summary>
    /// Backup.xaml 的交互逻辑
    /// </summary>
    public partial class Backup : UserControl
    {
        public Backup()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            var selected = this.dgData.SelectedItems;

            if (selected.Count != 1)
            {
                MessageBox.Show($"请选择一条记录进行操作", "提示信息");
                return;
            }

            var model = selected[0] as BackupModel;

            if (File.Exists(model.SavePath))
            {
                var fileInfo = new FileInfo(model.SavePath);
                System.Diagnostics.Process.Start(fileInfo.DirectoryName);
            }            
        }
    }
}
