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
        }
        
        ///// <summary>
        ///// 加载行事件，处理序号显示问题
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        //{
        //    e.Row.Header = e.Row.GetIndex() + 1 + ((dgPagination.PageIndex - 1) * dgPagination.DataCountPerPage);
        //}

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as UserViewModel).Query();
        }

        private void ckbSelectedAll_Checked(object sender, RoutedEventArgs e)
        {
            //foreach (DataRow row in dt.Rows)
            //{
            //    row["IsChecked"] = true;
            //}
        }

        private void ckbSelectedAll_Unchecked(object sender, RoutedEventArgs e)
        {
            //foreach (DataRow row in dt.Rows)
            //{
            //    row["IsChecked"] = false;
            //}
        }

    }
}
