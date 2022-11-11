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

            var model = this.DataContext as ParameterViewModel;

            //var computerInfo = ComputerInfo.GetInstance().ReadComputerInfo();
            //if (computerInfo != null)
            //{
            //    model.HostName = computerInfo.HostName;
            //    model.CPU = computerInfo.CPU;
            //    model.FullName = $"{model.HostName}-{model.CPU}";

            //    var ParameterInfo = model.GetParameterInfo($"{computerInfo.HostName}-{computerInfo.CPU}");

            //    model.ParameterCode = ParameterInfo.Item1;
            //    model.ParameterTime = ParameterInfo.Item2;
            //}

            //if (string.IsNullOrEmpty(model?.ParameterCode))
            //{
            //    model.ParameterButtonVisibility = Visibility.Visible;
            //    model.Status = "未注册";
            //}
            //else
            //{
            //    model.ParameterButtonVisibility = Visibility.Hidden;
            //    model.Status = "已注册";
            //}
        }

    }
}
