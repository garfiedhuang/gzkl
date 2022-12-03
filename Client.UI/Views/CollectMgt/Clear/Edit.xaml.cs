using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using HandyControl.Data;
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

namespace GZKL.Client.UI.Views.CollectMgt.Clear
{
    /// <summary>
    /// Edit.xaml 的交互逻辑
    /// </summary>
    public partial class Edit : Window
    {
        private ClearQueryModel ClearQueryModel { get; set; }

        public Edit(ClearModel clearModel, ClearQueryModel queryModel)
        {
            InitializeComponent();

            ClearQueryModel = queryModel;

            this.DataContext = new { Model = clearModel, QueryModel = queryModel };
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            switch (ClearQueryModel.ClearType)
            {
                case "试验日期":
                    if (ClearQueryModel.StartTestDt == null || ClearQueryModel.EndTestDt == null)
                    {
                        HandyControl.Controls.Growl.Warning("试验日期不能为空");
                        return;
                    }
                    else
                    {
                        ClearQueryModel.StartTestNo = string.Empty;
                        ClearQueryModel.EndTestNo = string.Empty;
                        ClearQueryModel.StartSampleNo = string.Empty;
                        ClearQueryModel.EndSampleNo = string.Empty;
                    }
                    break;
                case "检测编号":
                    if (string.IsNullOrEmpty(ClearQueryModel.StartTestNo) || string.IsNullOrEmpty(ClearQueryModel.EndTestNo))
                    {
                        HandyControl.Controls.Growl.Warning("检测编号不能为空");
                        return;
                    }
                    else
                    {
                        ClearQueryModel.StartTestDt = null;
                        ClearQueryModel.EndTestDt = null;
                        ClearQueryModel.StartSampleNo = string.Empty;
                        ClearQueryModel.EndSampleNo = string.Empty;
                    }
                    break;

                case "样品编号":
                    if (string.IsNullOrEmpty(ClearQueryModel.StartSampleNo) || string.IsNullOrEmpty(ClearQueryModel.EndSampleNo))
                    {
                        HandyControl.Controls.Growl.Warning("样品编号不能为空");
                        return;
                    }
                    else
                    {
                        ClearQueryModel.StartTestDt = null;
                        ClearQueryModel.EndTestDt = null;
                        ClearQueryModel.StartTestNo = string.Empty;
                        ClearQueryModel.EndTestNo = string.Empty;
                    }
                    break;
            }

            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
