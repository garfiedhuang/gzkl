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

namespace GZKL.Client.UI.Views.SystemMgt.Menu
{
    /// <summary>
    /// Menu.xaml 的交互逻辑
    /// </summary>
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as MenuViewModel;

            viewModel.Add();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as MenuViewModel;
            var selectedItem = this.dgMenuData.SelectedItem as MenuModel;

            if (selectedItem == null)
            {
                MessageBox.Show("请选择一条记录", "提示信息");
                return;
            }

            viewModel.Delete(selectedItem.Id);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as MenuViewModel;
            var selectedItem = this.dgMenuData.SelectedItem as MenuModel;

            if (selectedItem == null)
            {
                MessageBox.Show("请选择一条记录", "提示信息");
                return;
            }

            viewModel.Edit(selectedItem.Id);
        }
    }
}
