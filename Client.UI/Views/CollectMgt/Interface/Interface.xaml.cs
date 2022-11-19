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

namespace GZKL.Client.UI.Views.CollectMgt.Interface
{
    /// <summary>
    /// Interface.xaml 的交互逻辑
    /// </summary>
    public partial class Interface : UserControl
    {
        public Interface()
        {
            InitializeComponent();
        }

        private void btnSelectInterfaceDb_Click(object sender, RoutedEventArgs e)
        {
            var viewModel =this.DataContext as InterfaceViewModel;
            var selectedItem = this.dgInterfaceSelectData.SelectedItem as InterfaceInfo;

            if (selectedItem == null)
            {
                MessageBox.Show("请选择接口记录", "提示信息");
                return;
            }

            viewModel.SelectInterfaceDb(selectedItem);
        }

        private void btnSetInterface_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as InterfaceViewModel;
            var selectedItem = this.dgInterfaceSelectData.SelectedItem as InterfaceInfo;

            if (selectedItem == null)
            {
                MessageBox.Show("请选择接口记录", "提示信息");
                return;
            }

            viewModel.SetInterface(selectedItem);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as InterfaceViewModel;
            var interfaceInfo = this.dgInterfaceSelectData.SelectedItem as InterfaceInfo;
            var interfaceTestItemInfo = this.dgInterfaceTestItemData.SelectedItem as InterfaceTestItemInfo;
            var systemTestItemInfo = this.dgSystemTestItemData.SelectedItem as SystemTestItemInfo;

            if (interfaceInfo == null)
            {
                MessageBox.Show("请选择接口记录", "提示信息");
                return;
            }

            if (interfaceTestItemInfo == null)
            {
                MessageBox.Show("请选择接口对应检测项目记录", "提示信息");
                return;
            }

            if (systemTestItemInfo == null)
            {
                MessageBox.Show("请选择系统对应检测项目记录", "提示信息");
                return;
            }

            viewModel.AddTestItem(interfaceInfo, interfaceTestItemInfo, systemTestItemInfo);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as InterfaceViewModel;
            var selectedItem = this.dgInterfaceTestItemRelationData.SelectedItem as InterfaceTestItemRelationInfo;

            if (selectedItem == null)
            {
                MessageBox.Show("请选择接口与检测项目关系记录", "提示信息");
                return;
            }

            viewModel.DeleteTestItem(selectedItem);
        }
    }
}
