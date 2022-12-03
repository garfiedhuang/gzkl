using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
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

        public Edit(ClearModel clearModel,ClearQueryModel queryModel)
        {
            InitializeComponent();

            this.DataContext = new { Model = clearModel,QueryModel =queryModel };
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            //if (string.IsNullOrEmpty(this.txtOrgNo.Text))
            //{
            //    this.txtOrgNo.IsError = true;
            //    this.txtOrgNo.ErrorStr = "不能为空";
            //    return;
            //}
            //if (string.IsNullOrEmpty(this.txtOrgName.Text))
            //{
            //    this.txtOrgName.IsError = true;
            //    this.txtOrgName.ErrorStr = "不能为空";
            //    return;
            //}
            //if (string.IsNullOrEmpty(this.cmbIsEnabled.Text))
            //{
            //    this.cmbIsEnabled.IsError = true;
            //    this.cmbIsEnabled.ErrorStr = "不能为空";
            //    return;
            //}

            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
