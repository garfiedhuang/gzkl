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
    /// Setup.xaml 的交互逻辑
    /// </summary>
    public partial class Setup : UserControl
    {
        public Setup()
        {
            InitializeComponent();
        }

        private void SetupControl_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as InterfaceViewModel).Query();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = this.dgData.SelectedItems;

            if (selected.Count == 0)
            {
                MessageBox.Show($"请至少选择一条记录进行删除", "提示信息");
                return;
            }

            var models = new List<InterfaceTestItemRelationInfo>();

            foreach (var item in selected)
            {
                models.Add(item as InterfaceTestItemRelationInfo);
            }

            var viewModel = this.DataContext as InterfaceViewModel;
            //viewModel.Delete(models);
        }
    }
}
