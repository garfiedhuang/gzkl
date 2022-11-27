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
            //var MenuInfo = this.dgMenuSelectData.SelectedItem as MenuInfo;
            //var MenuTestItemInfo = this.dgMenuTestItemData.SelectedItem as MenuTestItemInfo;
            //var systemTestItemInfo = this.dgSystemTestItemData.SelectedItem as SystemTestItemInfo;

            //if (MenuInfo == null)
            //{
            //    MessageBox.Show("请选择接口记录", "提示信息");
            //    return;
            //}

            //if (MenuTestItemInfo == null)
            //{
            //    MessageBox.Show("请选择接口对应检测项目记录", "提示信息");
            //    return;
            //}

            //if (systemTestItemInfo == null)
            //{
            //    MessageBox.Show("请选择系统对应检测项目记录", "提示信息");
            //    return;
            //}

            //viewModel.AddTestItem(MenuInfo, MenuTestItemInfo, systemTestItemInfo);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as MenuViewModel;
            //var selectedItem = this.dgMenuTestItemRelationData.SelectedItem as MenuTestItemRelationInfo;

            //if (selectedItem == null)
            //{
            //    MessageBox.Show("请选择接口与检测项目关系记录", "提示信息");
            //    return;
            //}

            //viewModel.DeleteTestItem(selectedItem);
        }
    }
}
