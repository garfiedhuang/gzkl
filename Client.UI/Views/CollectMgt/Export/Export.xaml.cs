﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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
using GZKL.Client.UI.Models;
using GZKL.Client.UI.ViewsModels;

namespace GZKL.Client.UI.Views.CollectMgt.Export
{
    /// <summary>
    /// Export.xaml 的交互逻辑
    /// </summary>
    public partial class Export : UserControl
    {
        public Export()
        {
            InitializeComponent();
        }

        private void ExportControl_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as ExportViewModel).Query();
        }

        private void dgData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = this.dgData.SelectedItem as ExportModel;

            var imgPath = @"D:\work\source\gzkl\Client.UI\Assets\Images\Img.jpg";
            new HandyControl.Controls.ImageBrowser(new Uri(imgPath)).Show();

            /*
             procedure TfrmDataEpt.dxDBGrid1DblClick(Sender: TObject);
var openfile:string;
begin
 if not dxMemData1.Active then exit;
 if dxMemData1.RecordCount=0 then exit;
 openfile:=formatdatetime('yyyymmdd',dxMemData1.fieldbyname('TestTime').AsDateTime)+'-'+dxMemData1.fieldbyname('testno').AsString
            +'-'+dxMemData1.fieldbyname('No').AsString+'-'+dxMemData1.fieldbyname('ExperimentNo').AsString;
 openfile:=saveDataDir+openfile+'.bmp';
// openfile:='c:\aaa.JPG';
 if not FileExists(openfile) then
 begin
   showmessage('曲线文件不存在！');
 end
 else
 begin
   frmImg:=TfrmImg.Create(self);
   frmImg.Image1.Picture.LoadFromFile(openfile);
   frmImg.Image1.Width:=frmImg.Image1.Picture.Width ;
   frmImg.Image1.Height:=frmImg.Image1.Picture.Height ;
  try
    frmImg.ShowModal;
  finally
    frmImg.release;
  end;
 end;

end;
             */
        }


        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckAll_Click(object sender, RoutedEventArgs e)
        {
            this.dgData.SelectAll();
        }

        /// <summary>
        /// 反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInverse_Click(object sender, RoutedEventArgs e)
        {
            var rowNums = new List<long>();
            foreach (object o in dgData.SelectedItems)
            {
                rowNums.Add((o as ExportModel).RowNum);
            }
            dgData.SelectAll();
            foreach (object z in dgData.Items)
            {
                if (rowNums.Contains<long>((z as ExportModel).RowNum))
                {
                    dgData.SelectedItems.Remove(z);
                }
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            var selected = this.dgData.SelectedItems;

            if (selected.Count == 0)
            {
                MessageBox.Show($"请至少选择一条记录进行操作", "提示信息");
                return;
            }

            var exportModels =new List<ExportModel>();
            foreach (var item in selected)
            {
                exportModels.Add(item as ExportModel);
            }

            (this.DataContext as ExportViewModel).Export(exportModels);
        }

        /// <summary>
        /// 全部导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportAll_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as ExportViewModel;

            if (viewModel.TModels?.Count == 0)
            {
                MessageBox.Show($"未有可导出数据，请重新查询", "提示信息");
                return;
            }

            viewModel.ExportAll();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            var selected = this.dgData.SelectedItems;

            if (selected.Count != 1)
            {
                MessageBox.Show($"请选择一条记录进行操作", "提示信息");
                return;
            }

            var directry = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"export");

            if (Directory.Exists(directry))
            {
                System.Diagnostics.Process.Start(directry);
            }
        }
    }
}
