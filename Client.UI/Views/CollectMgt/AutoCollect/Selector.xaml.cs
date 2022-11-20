﻿using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.ViewsModels;
using System;
using System.Collections.Generic;
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

namespace GZKL.Client.UI.Views.CollectMgt.AutoCollect
{
    /// <summary>
    /// Selector.xaml 的交互逻辑
    /// </summary>
    public partial class Selector : Window
    {
        public string DataType { get; set; }

        /// <summary>
        /// 选择器数据源
        /// </summary>
        public List<SelectorModel> CloneSelectorData { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataType"></param>
        public Selector(string dataType)
        {
            InitializeComponent();
     
            DataType = dataType;

            var viewModel = this.DataContext as AutoCollectViewModel;

            CloneSelectorData = CollectionHelper.Clone(viewModel.SelectorData);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as AutoCollectViewModel;
            var selectedItem = this.dgData.SelectedItem as SelectorModel;

            if (selectedItem == null)
            {
                MessageBox.Show("请选择检测类型记录", "提示信息");
                return;
            }

            viewModel.Model.TestTypeNo = selectedItem.ItemNo;

            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            var keyword = this.txtQuery.Text.Trim();

            if (!string.IsNullOrEmpty(keyword))
            {
                var viewModel = this.DataContext as AutoCollectViewModel;

                var filterData = this.CloneSelectorData.Where(w => w.ItemNo.Contains(keyword) || w.ItemName.Contains(keyword))?.ToList();

                viewModel.SelectorData = CollectionHelper.Clone(filterData);
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            this.txtQuery.Text = string.Empty;

            var viewModel = this.DataContext as AutoCollectViewModel;
            viewModel.SelectorData = CollectionHelper.Clone(this.CloneSelectorData);
        }
    }
}
