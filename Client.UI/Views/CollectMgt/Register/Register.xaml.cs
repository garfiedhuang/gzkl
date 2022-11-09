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

namespace GZKL.Client.UI.Views.CollectMgt.Register
{
    /// <summary>
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class Register : UserControl
    {
        public Register()
        {
            InitializeComponent();

            var model = this.DataContext as RegisterViewModel;

            var computerInfo = ComputerInfo.GetInstance().ReadComputerInfo();
            if (computerInfo != null)
            {
                model.HostName = computerInfo.HostName;
                model.CPU = computerInfo.CPU;
                model.FullName = $"{model.HostName}-{model.CPU}";

                var registerInfo = model.GetRegisterInfo($"{computerInfo.HostName}-{computerInfo.CPU}");

                model.RegisterCode = registerInfo.Item1;
                model.RegisterTime = registerInfo.Item2;
            }

            if (string.IsNullOrEmpty(model?.RegisterCode))
            {
                model.RegisterButtonVisibility = (int)Visibility.Visible;
            }
        }

    }
}
