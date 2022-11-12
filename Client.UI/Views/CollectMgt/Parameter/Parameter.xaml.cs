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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var model = this.DataContext as ParameterViewModel;

            if (string.IsNullOrEmpty(model.Model.SerialPort))
            {
                MessageBox.Show("请选择【串行口】", "操作提示");
                return;
            }

            if (string.IsNullOrEmpty(model.Model.Tester))
            {
                MessageBox.Show("请选择【试验机】", "操作提示");
                return;
            }
            if (string.IsNullOrEmpty(model.Model.TesterName))
            {
                MessageBox.Show("请输入【试验机名称】", "操作提示");
                return;
            }
            if (string.IsNullOrEmpty(model.Model.SensorRange))
            {
                MessageBox.Show("请选择【传感器量程(kN)】", "操作提示");
                return;
            }
            if (string.IsNullOrEmpty(model.Model.FirstGear))
            {
                MessageBox.Show("请输入【第一档】", "操作提示");
                return;
            }
            if (string.IsNullOrEmpty(model.Model.SecondGear))
            {
                MessageBox.Show("请输入【第二档】", "操作提示");
                return;
            }
            if (string.IsNullOrEmpty(model.Model.ThirdGear))
            {
                MessageBox.Show("请输入【第三档】", "操作提示");
                return;
            }
            if (string.IsNullOrEmpty(model.Model.ExitMinValue))
            {
                MessageBox.Show("请输入【退出最小值(%)】", "操作提示");
                return;
            }
            else if (int.TryParse(model.Model.ExitMinValue, out var percent))
            {
                if (percent < 0 || percent > 100)
                {
                    MessageBox.Show("【退出最小值(%)】取值范围1-100", "操作提示");
                    return;
                }
            }
            else
            {
                MessageBox.Show("【退出最小值(%)】必须是数字，且取值范围1-100", "操作提示");
                return;
            }
            if (string.IsNullOrEmpty(model.Model.FailureJudgment))
            {
                MessageBox.Show("请输入【破坏判断】", "操作提示");
                return;
            }
            if (string.IsNullOrEmpty(model.Model.DrawnRange))
            {
                MessageBox.Show("请输入【绘图范围】", "操作提示");
                return;
            }
            if (string.IsNullOrEmpty(model.Model.AdjustedFactor))
            {
                MessageBox.Show("请输入【调整系数】", "操作提示");
                return;
            }
            if (string.IsNullOrEmpty(model.Model.AutoSwitchRatio))
            {
                MessageBox.Show("请输入【自动切换比例(%)】", "操作提示");
                return;
            }
            else if (int.TryParse(model.Model.AutoSwitchRatio, out var percent))
            {
                if (percent < 0 || percent > 100)
                {
                    MessageBox.Show("【自动切换比例(%)】取值范围1-100", "操作提示");
                    return;
                }
            }
            else
            {
                MessageBox.Show("【自动切换比例(%)】必须是数字，且取值范围1-100", "操作提示");
                return;
            }
            if (string.IsNullOrEmpty(model.Model.SavePath))
            {
                MessageBox.Show("请选择【保存路径】", "操作提示");
                return;
            }

            if (string.IsNullOrEmpty(model.Model.CollectType))
            {
                MessageBox.Show("请选择【采集类型】","操作提示");
                return;
            }

            if (string.IsNullOrEmpty(model.Model.WuxiSuggestedDecimalDigit))
            {
                MessageBox.Show("请选择【无锡建议小数位】", "操作提示");
                return;
            }

            var rbCollectType = this.gbCollectType.FindName($"rb{model.Model.CollectType}") as RadioButton;
            var rbDecimalDigitType = this.gbCollectType.FindName($"rb{model.Model.WuxiSuggestedDecimalDigit}") as RadioButton;

            var collectType = model.Model.CollectType;
            var decimalDigitType = model.Model.WuxiSuggestedDecimalDigit;

            if (rbCollectType != null)
            {
                collectType = $"{collectType}#{rbCollectType.Content}";
            }
            if (rbDecimalDigitType != null)
            {
                decimalDigitType = $"{decimalDigitType}#{rbDecimalDigitType.Content}";
            }

            model.Save(collectType, decimalDigitType);
        }
    }
}
