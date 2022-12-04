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

namespace GZKL.Client.UI.Views.CollectMgt.Parameter
{
    /// <summary>
    /// Parameter.xaml 的交互逻辑
    /// </summary>
    public partial class Parameter : UserControl
    {
        public Parameter()
        {
            InitializeComponent();
        }

        private void ParameterControl_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as ParameterViewModel).Query();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selected = this.dgData.SelectedItems;

            if (selected.Count != 1)
            {
                MessageBox.Show($"请选择一条记录进行编辑", "提示信息");
                return;
            }

            var id = (selected[0] as ParameterModel).Id;

            var viewModel = this.DataContext as ParameterViewModel;

            viewModel.Edit(id);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = this.dgData.SelectedItems;

            if (selected.Count == 0)
            {
                MessageBox.Show($"请至少选择一条记录进行删除", "提示信息");
                return;
            }

            var models = new List<ParameterModel>();

            foreach (var item in selected)
            {
                models.Add(item as ParameterModel);
            }

            var viewModel = this.DataContext as ParameterViewModel;

            viewModel.Delete(models);
        }
    }
}
