using System;
using System.Collections.Generic;
using System.Data;
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
using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.ViewsModels;
using MessageBox = HandyControl.Controls.MessageBox;

namespace GZKL.Client.UI.Views.CollectMgt.Clear
{
    /// <summary>
    /// Clear.xaml 的交互逻辑
    /// </summary>
    public partial class Clear : UserControl
    {
        public Clear()
        {
            InitializeComponent();
        }

        private void OrgControl_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as OrgViewModel).Query();
        }

        private void dgData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selected = this.dgData.SelectedItems;

            if (selected.Count != 1)
            {
                MessageBox.Show($"请选择一条记录进行编辑", "提示信息");
                return;
            }

            var clearModel = selected[0] as ClearModel;

            HandyControl.Controls.Dialog.Show(new View(clearModel));
        }
    }
}
