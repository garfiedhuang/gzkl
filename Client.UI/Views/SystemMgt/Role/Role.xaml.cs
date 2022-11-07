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
using GZKL.Client.UI.ViewsModels;

namespace GZKL.Client.UI.Views.SystemMgt.Role
{
    /// <summary>
    /// Role.xaml 的交互逻辑
    /// </summary>
    public partial class Role : UserControl
    {
        public Role()
        {
            InitializeComponent();
        }

        private void RoleControl_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as RoleViewModel).Query();
        }
    }
}
