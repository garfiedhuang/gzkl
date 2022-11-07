using GZKL.Client.UI.Models;
using System;
using System.Collections.Generic;
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

namespace GZKL.Client.UI.Views.SystemMgt.Config
{
    /// <summary>
    /// Edit.xaml 的交互逻辑
    /// </summary>
    public partial class Edit : Window
    {
        public Edit(ConfigModel configModel)
        {
            InitializeComponent();

            var isEnabledData = new List<KeyValuePair<int, string>>();
            isEnabledData.Add(new KeyValuePair<int, string>(0, "0-否"));
            isEnabledData.Add(new KeyValuePair<int, string>(1, "1-是"));

            this.DataContext = new { Model = configModel, IsEnabledData = isEnabledData };

            if (configModel.Id != 0)
            {
                this.txtCategory.IsReadOnly = true;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtCategory.Text))
            {
                this.txtCategory.IsError = true;
                this.txtCategory.ErrorStr = "不能为空";
                return;
            }
            if (string.IsNullOrEmpty(this.txtValue.Text))
            {
                this.txtValue.IsError = true;
                this.txtValue.ErrorStr = "不能为空";
                return;
            }
            if (string.IsNullOrEmpty(this.txtText.Text))
            {
                this.txtText.IsError = true;
                this.txtText.ErrorStr = "不能为空";
                return;
            }
            if (string.IsNullOrEmpty(this.cmbIsEnabled.Text))
            {
                this.cmbIsEnabled.IsError = true;
                this.cmbIsEnabled.ErrorStr = "不能为空";
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
