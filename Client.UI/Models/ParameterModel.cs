using GalaSoft.MvvmLight;
using GZKL.Client.UI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Client.UI.Models
{
    /// <summary>
    /// 参数模型
    /// </summary>
    public class ParameterModel
    {
        /// <summary>
        /// 串行口
        /// </summary>
        public string SerialPort { get; set; }

        /// <summary>
        /// 试验机
        /// </summary>
        public string Tester { get; set; }

        /// <summary>
        /// 试验机名称
        /// </summary>
        public string TesterName { get; set; }

        /// <summary>
        /// 传感器量程(kN)
        /// </summary>
        public string SensorRange { get; set; }

        /// <summary>
        /// 第一档
        /// </summary>
        public string FirstGear { get; set; }

        /// <summary>
        /// 第二档
        /// </summary>
        public string SecondGear { get; set; }

        /// <summary>
        /// 第三档
        /// </summary>
        public string ThirdGear { get; set; }

        /// <summary>
        /// 退出最小值(%)
        /// </summary>
        public string ExitMinValue { get; set; }

        /// <summary>
        /// 破坏判断
        /// </summary>
        public string FailureJudgment { get; set; }

        /// <summary>
        /// 绘图范围
        /// </summary>
        public string DrawnRange { get; set; }

        /// <summary>
        /// 调整系数
        /// </summary>
        public string AdjustedFactor { get; set; }

        /// <summary>
        /// 自动切换比例(%)
        /// </summary>
        public string AutoSwitchRatio { get; set; }

        /// <summary>
        /// 双通道
        /// </summary>
        public bool TwoChannel { get; set; }

        /// <summary>
        /// 自动切换
        /// </summary>
        public bool AutoSwitch { get; set; }

        /// <summary>
        /// 保存数据
        /// </summary>
        public bool SaveData { get; set; }

        /// <summary>
        /// 保存图形
        /// </summary>
        public bool SaveGraph { get; set; }

        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// 试验机类型
        /// </summary>
        public string TesterType { get; set; }

        /// <summary>
        /// 无锡建议小数位
        /// </summary>
        public string WuxiSuggestedDecimalDigits { get; set; }
    }
}
