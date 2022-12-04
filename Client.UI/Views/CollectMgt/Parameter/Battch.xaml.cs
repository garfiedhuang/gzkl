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
    /// Battch.xaml 的交互逻辑
    /// </summary>
    public partial class Battch : UserControl
    {
        public Battch()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var model = this.DataContext as BattchViewModel;

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

            var firstGear = 0.0;
            var secondGear = 0.0;
            var thirdGear = 0.0;

            if (string.IsNullOrEmpty(model.Model.FirstGear))
            {
                MessageBox.Show("请输入【第一档】", "操作提示");
                return;
            }
            else if (!double.TryParse(model.Model.FirstGear, out firstGear))
            {
                MessageBox.Show("【第一档】必须是数字", "操作提示");
                return;
            }

            if (string.IsNullOrEmpty(model.Model.SecondGear))
            {
                MessageBox.Show("请输入【第二档】", "操作提示");
                return;
            }
            else if (!double.TryParse(model.Model.SecondGear, out secondGear))
            {
                MessageBox.Show("【第二档】必须是数字", "操作提示");
                return;
            }

            if (string.IsNullOrEmpty(model.Model.ThirdGear))
            {
                MessageBox.Show("请输入【第三档】", "操作提示");
                return;
            }
            else if (!double.TryParse(model.Model.ThirdGear, out thirdGear))
            {
                MessageBox.Show("【第三档】必须是数字", "操作提示");
                return;
            }

            if (firstGear <= secondGear || secondGear <= thirdGear)
            {
                MessageBox.Show("输入错误,第一二三档依次减小,请正确输入", "操作提示");
                return;
            }


            if (string.IsNullOrEmpty(model.Model.ExitMinValue))
            {
                MessageBox.Show("请输入【退出最小值(%)】", "操作提示");
                return;
            }
            else if (int.TryParse(model.Model.ExitMinValue, out var percent))
            {
                if (percent < 1 || percent > 100)
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
            else if (int.TryParse(model.Model.FailureJudgment, out var percent))
            {
                if (percent < 1 || percent > 100)
                {
                    MessageBox.Show("【破坏判断】取值范围1-100", "操作提示");
                    return;
                }
            }
            else
            {
                MessageBox.Show("【破坏判断】必须是数字，且取值范围1-100", "操作提示");
                return;
            }

            if (string.IsNullOrEmpty(model.Model.DrawnRange))
            {
                MessageBox.Show("请输入【绘图范围】", "操作提示");
                return;
            }
            else if (int.TryParse(model.Model.DrawnRange, out var percent))
            {
                if (percent < 1 || percent > 100)
                {
                    MessageBox.Show("【绘图范围】取值范围1-100", "操作提示");
                    return;
                }
            }
            else
            {
                MessageBox.Show("【绘图范围】必须是数字，且取值范围1-100", "操作提示");
                return;
            }

            if (string.IsNullOrEmpty(model.Model.AdjustedFactor))
            {
                MessageBox.Show("请输入【调整系数】", "操作提示");
                return;
            }
            else if (double.TryParse(model.Model.AdjustedFactor, out var percent))
            {
                if (percent < 0.1 || percent > 1.0)
                {
                    MessageBox.Show("【调整系数】取值范围0.1-1.0", "操作提示");
                    return;
                }
            }
            else
            {
                MessageBox.Show("【调整系数】必须是数字，且取值范围0.1-1.0", "操作提示");
                return;
            }

            if (string.IsNullOrEmpty(model.Model.AutoSwitchRatio))
            {
                MessageBox.Show("请输入【自动切换比例(%)】", "操作提示");
                return;
            }
            else if (int.TryParse(model.Model.AutoSwitchRatio, out var percent))
            {
                if (percent < 1 || percent > 100)
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
                MessageBox.Show("请选择【采集类型】", "操作提示");
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

        private void cmbTester_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var model = this.DataContext as BattchViewModel;

            model.TesterSelectionChanged();
        }
    }
}
