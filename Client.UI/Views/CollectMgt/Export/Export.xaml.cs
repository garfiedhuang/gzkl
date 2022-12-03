using System;
using System.Collections.Generic;
using System.Data;
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
 openfile:=formatdatetime('yyyymmdd',dxMemData1.fieldbyname('PlayTime').AsDateTime)+'-'+dxMemData1.fieldbyname('testno').AsString
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

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExportAll_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
