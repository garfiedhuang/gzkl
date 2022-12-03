using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using HandyControl.Data;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using MessageBox = HandyControl.Controls.MessageBox;

namespace GZKL.Client.UI.Views.CollectMgt.Backup
{
    /// <summary>
    /// Edit.xaml 的交互逻辑
    /// </summary>
    public partial class Edit : Window
    {
        private BackupModel BackupModel { get; set; }

        public Edit(BackupModel backupModel)
        {
            InitializeComponent();

            this.Owner = Application.Current.MainWindow;

            this.BackupModel = backupModel;

            this.DataContext = new { Model = backupModel};

            //初始化当前备份目录
            var backupPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backup");
            if (!System.IO.Directory.Exists(backupPath))
            {
                System.IO.Directory.CreateDirectory(backupPath);
            }

            backupModel.SavePath = backupPath;
        }

        /// <summary>
        /// 选择
        /// </summary>
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog("请选择一个文件夹");
                dialog.IsFolderPicker = true; //选择文件还是文件夹（true:选择文件夹，false:选择文件）
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    BackupModel.SavePath = dialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(BackupModel.SavePath))
            {
                HandyControl.Controls.Growl.Warning("请选择保存路径！");
                return;
            }

            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
