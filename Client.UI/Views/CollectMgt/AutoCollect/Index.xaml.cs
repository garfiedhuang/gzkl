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
using GZKL.Client.UI.Factories;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.ViewsModels;
using MessageBox = HandyControl.Controls.MessageBox;

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
            try
            {
                var viewModel = this.DataContext as AutoCollectViewModel;
                var collectDataEnum = (CollectDataEnum)viewModel.Model.InterfaceId;

                //采集引擎工厂
                var collectEngine = CreateCollectEngine.Create(collectDataEnum);

                //查询本地数据库
                viewModel.QueryData();

                //查询设备数据库
                collectEngine.QueryDeviceData(viewModel);

                //启用【数据入库】按钮
                this.btnSave.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"操作提示");
            }
        }

        /// <summary>
        /// 数据入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var viewModel = this.DataContext as AutoCollectViewModel;
                var collectDataEnum = (CollectDataEnum)viewModel.Model.InterfaceId;

                //采集引擎工厂
                var collectEngine = CreateCollectEngine.Create(collectDataEnum);

                //写入数据库
                collectEngine.ImportData(viewModel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "操作提示");
            }
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

        /// <summary>
        /// 系统检测项，单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTestItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var viewModel = this.DataContext as AutoCollectViewModel;

                viewModel.SelectorData?.Clear();

                if (string.IsNullOrEmpty(viewModel.Model.QueryTestNo))
                {
                    viewModel.GetSystemTestItemData(false);
                }
                else
                {
                    viewModel.GetSystemTestItemData();
                }

                viewModel.SystemTestItemData?.ForEach(item =>
                {
                    viewModel.SelectorData.Add(new SelectorModel()
                    {
                        Id = item.Id,
                        ItemNo = item.TestItemNo,
                        ItemName = item.TestItemName,
                        CreateDt = item.CreateDt,
                        UpdateDt = item.UpdateDt,
                    });
                });

                Selector view = new Selector("SystemTestItem");
                var r = view.ShowDialog();
                if (r.Value)
                {

                }
                e.Handled = true;//阻止冒泡
            }
        }

        /// <summary>
        /// 接口检测项，单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbInterfaceTestItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var viewModel = this.DataContext as AutoCollectViewModel;

                viewModel.SelectorData?.Clear();

                if (string.IsNullOrEmpty(viewModel.Model.SystemTestItemNo))
                {
                    //未选择系统检测项
                    viewModel.GetInterfaceTestItemData(false);
                }
                else
                {
                    //已选择系统检测项
                    viewModel.GetInterfaceTestItemData();
                }

                viewModel.InterfaceTestItemData?.ForEach(item =>
                {
                    viewModel.SelectorData.Add(new SelectorModel()
                    {
                        Id = item.Id,
                        ItemNo = item.Id.ToString(),
                        ItemName = item.TestItemName,
                        CreateDt = item.CreateDt,
                        UpdateDt = item.UpdateDt,
                    });
                });

                Selector view = new Selector("InterfaceTestItem");
                var r = view.ShowDialog();
                if (r.Value)
                {

                }
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
