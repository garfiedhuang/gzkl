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

namespace GZKL.Client.UI.Views.SystemMgt.Role
{
    /// <summary>
    /// Edit.xaml 的交互逻辑
    /// </summary>
    public partial class Edit : Window
    {
        public Edit(RoleModel roleModel)
        {
            InitializeComponent();

            this.DataContext = new { Model = roleModel};

            if (roleModel.Id != 0)
            {
                this.txtName.IsReadOnly = true;
            }
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
