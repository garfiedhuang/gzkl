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
using GZKL.Client.UI.ViewsModels;

namespace GZKL.Client.UI.Views.CollectMgt.Export
{
    /// <summary>
    /// Export.xaml 的交互逻辑
    /// </summary>
    public partial class Export : UserControl
    {
        public Export()
        {
            InitializeComponent();
        }

        private void ExportControl_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as ExportViewModel).Query();
        }

        private void dgData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
