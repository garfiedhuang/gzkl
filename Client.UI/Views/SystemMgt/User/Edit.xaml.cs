using GalaSoft.MvvmLight.Messaging;
using GZKL.Cilent.UI.Models;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GZKL.Cilent.UI.Views.SystemMgt.User
{
    /// <summary>
    /// Edit.xaml 的交互逻辑
    /// </summary>
    public partial class Edit : Window
    {
        public Edit(UserModel userModel)
        {
            InitializeComponent();

            var sexData = new List<KeyValuePair<int, string>>();
            sexData.Add(new KeyValuePair<int, string>(0, "0-未知"));
            sexData.Add(new KeyValuePair<int, string>(1, "1-男"));
            sexData.Add(new KeyValuePair<int, string>(2, "2-女"));

            var isEnabledData = new List<KeyValuePair<int, string>>();
            isEnabledData.Add(new KeyValuePair<int, string>(0, "0-否"));
            isEnabledData.Add(new KeyValuePair<int, string>(1, "1-是"));

            this.DataContext = new { Model = userModel, SexData = sexData, IsEnabledData = isEnabledData };
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtName.Text))
            {
                this.txtName.IsError = true;
                this.txtName.ErrorStr = "不能为空";
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
