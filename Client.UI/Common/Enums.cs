using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Client.UI.Common
{
    public enum SexEnum
    {
        [Description("未知")]
        None = 0,

        [Description("男")]
        Femal = 1,

        [Description("女")]
        Meal =2
    }

    public enum BoolEnum
    {
        [Description("否")]
        Disabled = 0,

        [Description("是")]
        Enabled = 1
    }

    /// <summary>
    /// 采集数据枚举
    /// </summary>
    public enum CollectDataEnum
    {
        /// <summary>
        /// 杭州鑫高=>压力机
        /// </summary>
        [Description("杭州鑫高=>压力机")]
        Device1 = 1,

        /// <summary>
        /// 杭州鑫高=>万能机
        /// 由于数据都存在同一个表，因此对数据要进行过滤
        /// </summary>
        [Description("杭州鑫高=>万能机")]
        Device2 = 2,

        /// <summary>
        /// 杭州鑫高=>万能机
        /// 由于数据都存在同一个表，因此对数据要进行过滤
        /// </summary>
        [Description("杭州鑫高=>万能机")]
        Device3 = 3,

        /// <summary>
        /// 杭州鑫高(testMast)
        /// </summary>
        [Description("杭州鑫高(testMast)")]
        Device4 = 4,

        /// <summary>
        /// 济南试金(smartTest)
        /// </summary>
        [Description("济南试金(smartTest)")]
        Device5 = 5,

        /// <summary>
        /// 济南试金(smartTest)
        /// </summary>
        [Description("济南试金(smartTest)")]
        Device6 = 6,
    }
}
