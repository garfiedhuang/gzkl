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

            viewModel.SelectInterfaceDb(selectedItem);
        }

        private void btnSetInterface_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
