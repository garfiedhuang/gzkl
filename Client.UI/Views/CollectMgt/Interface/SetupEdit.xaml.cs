using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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
using MessageBox = HandyControl.Controls.MessageBox;

namespace GZKL.Client.UI.Views.CollectMgt.Interface
{
    /// <summary>
    /// SetupEdit.xaml 的交互逻辑
    /// </summary>
    public partial class SetupEdit : Window
    {
        private InterfaceTestItemRelationInfo _model;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="interfaceBaseModel"></param>
        /// <param name="interfaceInfos"></param>
        /// <param name="interfaceTestItemInfos"></param>
        /// <param name="systemTestItemInfos"></param>
        public SetupEdit(
            InterfaceTestItemRelationInfo interfaceBaseModel,
            List<InterfaceInfo> interfaceInfos,
            List<InterfaceTestItemInfo> interfaceTestItemInfos,
            List<SystemTestItemInfo> systemTestItemInfos)
        {
            InitializeComponent();

            this._model = interfaceBaseModel;

            this.DataContext = new
            {
                Model = interfaceBaseModel,
                InterfaceInfos = interfaceInfos,
                InterfaceTestItemInfos = interfaceTestItemInfos,
                SystemTestItemInfos = systemTestItemInfos
            };
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_model.InterfaceId == 0)
            {
                HandyControl.Controls.Growl.Warning("请选择接口！");
                return;
            }
            if (_model.InterfaceTestItemId == 0)
            {
                HandyControl.Controls.Growl.Warning("请选择接口检测项！");
                return;
            }
            if (string.IsNullOrEmpty(_model.SystemTestItemNo))
            {
                HandyControl.Controls.Growl.Warning("请选择系统检测项！");
                return;
            }

            var sql = @"SELECT COUNT(1) FROM [dbo].[base_interface_relation] WHERE test_item_id=@interfaceTestItemId AND test_item_no=@systemTestItemNo";
            var parameters = new SqlParameter[] {
                    new SqlParameter("@interfaceTestItemId", _model.InterfaceTestItemId),
                    new SqlParameter("@systemTestItemNo", _model.SystemTestItemNo)};

            var result = Convert.ToInt32(SQLHelper.ExecuteScalar(sql, parameters));

            if (result > 0)
            {
                HandyControl.Controls.Growl.Warning($"记录{_model.InterfaceTestItemId}|{_model.SystemTestItemNo}已存在，请勿重复添加");
                return;
            }

            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnPre_Click(object sender, RoutedEventArgs e)
        {
            this.sbStep.Prev();
            this.ChangeStep();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            this.sbStep.Next();
            this.ChangeStep();
        }

        private void ChangeStep()
        {
            switch (this.sbStep.StepIndex)
            {

                case 0:
                    this.spStep1.Visibility = Visibility.Visible;
                    this.spStep2.Visibility = Visibility.Collapsed;
                    this.spStep3.Visibility = Visibility.Collapsed;
                    this.spStep4.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    this.spStep1.Visibility = Visibility.Collapsed;
                    this.spStep2.Visibility = Visibility.Visible;
                    this.spStep3.Visibility = Visibility.Collapsed;
                    this.spStep4.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    this.spStep1.Visibility = Visibility.Collapsed;
                    this.spStep2.Visibility = Visibility.Collapsed;
                    this.spStep3.Visibility = Visibility.Visible;
                    this.spStep4.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    this.spStep1.Visibility = Visibility.Collapsed;
                    this.spStep2.Visibility = Visibility.Collapsed;
                    this.spStep3.Visibility = Visibility.Collapsed;
                    this.spStep4.Visibility = Visibility.Visible;
                    break;
            }
            if (this.sbStep.StepIndex == 0)
            {
                this.btnPre.IsEnabled = false;
            }
            else
            {
                this.btnPre.IsEnabled = true;
            }

            if (this.sbStep.StepIndex == 3)
            {
                this.btnNext.Content = "保存";
            }
            else
            {
                this.btnNext.Content = "下一步";
            }
        }

        private void dgInterfaceSelectData_Selected(object sender, RoutedEventArgs e)
        {
            var item = this.dgInterfaceSelectData.SelectedItem as InterfaceInfo;

            this._model.InterfaceId = item.Id;
            this._model.InterfaceName = item.InterfaceName;
        }

        private void dgInterfaceTestItemData_Selected(object sender, RoutedEventArgs e)
        {
            var item = this.dgInterfaceTestItemData.SelectedItem as InterfaceTestItemInfo;

            this._model.InterfaceTestItemId = item.Id;
            this._model.InterfaceTestItemName = item.TestItemName;
        }

        private void dgSystemTestItemData_Selected(object sender, RoutedEventArgs e)
        {
            var item = this.dgSystemTestItemData.SelectedItem as SystemTestItemInfo;
            this._model.SystemTestItemNo = item.TestItemNo;
            this._model.SystemTestItemName = item.TestItemName;
        }
    }
}
