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
using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.ViewsModels;

namespace GZKL.Client.UI.Views.CollectMgt.AutoCollect
{
    /// <summary>
    /// Index.xaml 的交互逻辑
    /// </summary>
    public partial class Index : UserControl
    {
        public Index()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 数据入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 检测类型，单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTestType_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var viewModel = this.DataContext as AutoCollectViewModel;

                viewModel.SelectorData?.Clear();

                viewModel.TestTypeData?.ForEach(item =>
                {
                    viewModel.SelectorData.Add(new SelectorModel()
                    {
                        Id = item.Id,
                        ItemNo = item.TestTypeNo,
                        ItemName = item.TestTypeName,
                        CreateDt = item.CreateDt,
                        UpdateDt = item.UpdateDt,
                    });
                });

                Selector view = new Selector("TestType");
                var r = view.ShowDialog();
                if (r.Value)
                {

                }
                e.Handled = true;//阻止冒泡
            }
        }

        private void cmbTestItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var model = this.DataContext as AutoCollectViewModel;
                model.Model.SystemTestItemNo = "TT02";


                e.Handled = true;//阻止冒泡
            }
        }

        private void cmbInterfaceTestItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var model = this.DataContext as AutoCollectViewModel;
                model.Model.InterfaceTestItemNo = "TT02";


                e.Handled = true;//阻止冒泡
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            var model = this.DataContext as AutoCollectViewModel;

            model.InitData();
            model.SetInterface();
        }
    }
}
