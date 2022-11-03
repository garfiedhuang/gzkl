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
using GZKL.Cilent.UI.ViewsModels;
using HandyControl.Controls;
using MessageBox = HandyControl.Controls.MessageBox;

namespace GZKL.Cilent.UI.Views.SystemMgt.User
{
    /// <summary>
    /// User.xaml 的交互逻辑
    /// </summary>
    public partial class User : UserControl
    {
        public User()
        {
            InitializeComponent();

            //UserViewModel userViewModel = new UserViewModel();
            //this.DataContext = userViewModel;
        }
    }
}
