using GalaSoft.MvvmLight;
using GZKL.Client.UI.Common;
using HandyControl.Controls;
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
    public class ParameterModel:ObservableObject
    {
        public ParameterModel()
        { }

        /// <summary>
        /// 串行口
        /// </summary>
        [Description("串行口")]
        public string SerialPort { get; set; }

        /// <summary>
        /// 试验机
        /// </summary>
        [Description("通道号")]
        public string Tester { get; set; }

        /// <summary>
        /// 试验机名称
        /// </summary>
        [Description("试验机型号")]
        public string TesterName { get; set; }

        /// <summary>
        /// 传感器量程(kN)
        /// </summary>
        [Description("最大量程")]
        public string SensorRange { get; set; }


        private string rangeFactor;
        /// <summary>
        /// 量程系数 = 传感器量程(kN) / 4095
        /// </summary>
        [Description("量程系数")]
        //public string RangeFactor { get; set; }
        public string RangeFactor
        {
            get
            {
                int.TryParse(SensorRange, out var value);
                return rangeFactor = System.Math.Round(value / 4095 * 1.0, 4).ToString();
            }
            set
            {
                rangeFactor = value;
            }
        }

        /// <summary>
        /// 第一档
        /// </summary>
        [Description("量程1")]
        public string FirstGear { get; set; }

        /// <summary>
        /// 第二档
        /// </summary>
        [Description("量程2")]
        public string SecondGear { get; set; }

        /// <summary>
        /// 第三档
        /// </summary>
        [Description("量程3")]
        public string ThirdGear { get; set; }

        /// <summary>
        /// 退出最小值(%)
        /// </summary>
        [Description("自动结束最小值")]
        public string ExitMinValue { get; set; }

        /// <summary>
        /// 破坏判断
        /// </summary>
        [Description("破坏判断")]
        public string FailureJudgment { get; set; }

        /// <summary>
        /// 当前量程号
        /// </summary>
        [Description("当前量程号")]
        public string CurrentRangeNo { get; set; }

        /// <summary>
        /// 绘图范围
        /// </summary>
        [Description("绘图范围")]
        public string DrawnRange { get; set; }

        /// <summary>
        /// 绘图间隔
        /// </summary>
        [Description("绘图间隔")]
        public string DrawnInterval { get; set; } = "3";

        /// <summary>
        /// 调整系数
        /// </summary>
        [Description("调整系数")]
        public string AdjustedFactor { get; set; }


        private string autoSwitchRatio;
        /// <summary>
        /// 自动切换比例(%)
        /// </summary>
        [Description("切换比例")]
        public string AutoSwitchRatio
        {
            get { return autoSwitchRatio; }
            set { autoSwitchRatio = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 补偿有效
        /// </summary>
        [Description("补偿有效")]
        public bool CompensationEffect { get; set; }

        /// <summary>
        /// 双通道
        /// </summary>
        [Description("双通道")]
        public bool TwoChannel { get; set; }


        private bool autoSwitch;
        /// <summary>
        /// 自动切换
        /// </summary>
        [Description("自动切换")]
        public bool AutoSwitch
        {
            get { return autoSwitch; }
            set { autoSwitch = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        [Description("是否保存数据")]
        public bool SaveData { get; set; }

        /// <summary>
        /// 保存图形
        /// </summary>
        [Description("是否保存图片")]
        public bool SaveGraph { get; set; }


        private string savePath;
        /// <summary>
        /// 保存路径
        /// </summary>
        [Description("保存路径")]
        public string SavePath
        {
            get { return savePath; }
            set { savePath = value;RaisePropertyChanged(); }
        }


        /// <summary>
        /// 试验机类型
        /// </summary>
        [Description("采集类型")]
        public string CollectType { get; set; }

        /// <summary>
        /// 无锡建议小数位
        /// </summary>
        [Description("TYE小数位")]
        public string WuxiSuggestedDecimalDigit { get; set; }

        ///// <summary>
        ///// 试验机类型
        ///// </summary>
        //[Description("采集类型")]
        //public CollectTypeModel CollectType { get; set; } = new CollectTypeModel();

        ///// <summary>
        ///// 无锡建议小数位
        ///// </summary>
        //[Description("TYE小数位")]
        //public WuxiSuggestedDecimalDigitModel WuxiSuggestedDecimalDigit { get; set; } = new WuxiSuggestedDecimalDigitModel();
    }

    /*
    public class CollectTypeModel
    {
        public CompBottonModel T001 { get; set; } = new CompBottonModel() { Tag = "T001", Content = "三和采集SSY" };
        public CompBottonModel T002 { get; set; } = new CompBottonModel() { Tag = "T002", Content = "三和采集KLGK" };
        public CompBottonModel T003 { get; set; } = new CompBottonModel() { Tag = "T003", Content = "无锡建议TYE" };
        public CompBottonModel T004 { get; set; } = new CompBottonModel() { Tag = "T004", Content = "无锡中科SYE" };
        public CompBottonModel T005 { get; set; } = new CompBottonModel() { Tag = "T005", Content = "上海申克" };
        public CompBottonModel T006 { get; set; } = new CompBottonModel() { Tag = "T006", Content = "上海华龙HL-3" };
        public CompBottonModel T007 { get; set; } = new CompBottonModel() { Tag = "T007", Content = "龙盛SZ**E(惠州)" };
        public CompBottonModel T008 { get; set; } = new CompBottonModel() { Tag = "T008", Content = "杭州鑫高WES-06" };
        public CompBottonModel T009 { get; set; } = new CompBottonModel() { Tag = "T009", Content = "杭州鑫高YA-06" };
        public CompBottonModel T010 { get; set; } = new CompBottonModel() { Tag = "T010", Content = "无锡建议TYE-2000C" };
        public CompBottonModel T011 { get; set; } = new CompBottonModel() { Tag = "T011", Content = "龙盛LM-02(潮安)" };
        public CompBottonModel T012 { get; set; } = new CompBottonModel() { Tag = "T012", Content = "龙盛LM-02(广州)" };
        public CompBottonModel T013 { get; set; } = new CompBottonModel() { Tag = "T013", Content = "龙盛LM-02" };
        public CompBottonModel T014 { get; set; } = new CompBottonModel() { Tag = "T014", Content = "肯特WE-300S" };
    }

    public class WuxiSuggestedDecimalDigitModel
    {
        public CompBottonModel DDT001 { get; set; } = new CompBottonModel() { Tag = "DDT001", Content = "0.100" };
        public CompBottonModel DDT002 { get; set; } = new CompBottonModel() { Tag = "DDT002", Content = "0.010" };
        public CompBottonModel DDT003 { get; set; } = new CompBottonModel() { Tag = "DDT003", Content = "0.001" };
    }

    public class CompBottonModel : ObservableObject
    {
        public CompBottonModel()
        {
            //构造函数
        }

        private string tag;
        /// <summary>
        /// 单选框相关
        /// </summary>
        public string Tag
        {
            get { return tag; }
            set { tag = value; RaisePropertyChanged(() => Tag); }
        }

        private string content;
        /// <summary>
        /// 单选框相关
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; RaisePropertyChanged(() => Content); }
        }

        private bool isCheck;
        /// <summary>
        /// 单选框是否选中
        /// </summary>
        public bool IsCheck
        {
            get { return isCheck; }
            set { isCheck = value; RaisePropertyChanged(() => IsCheck); }
        }
    }
    */
}
