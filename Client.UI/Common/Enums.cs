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
        Meal = 2
    }

    public enum BoolEnum
    {
        [Description("否")]
        Disabled = 0,

        [Description("是")]
        Enabled = 1
    }

    public enum MenuType
    {
        /// <summary>
        /// 根菜单
        /// </summary>
        [Description("根菜单")]
        Root = 1,

        /// <summary>
        /// 一级菜单
        /// </summary>
        [Description("一级菜单")]
        Primary,

        /// <summary>
        /// 二级菜单
        /// </summary>
        [Description("二级菜单")]
        Secondary,

        /// <summary>
        /// 三级菜单
        /// </summary>
        [Description("三级菜单")]
        Third
    }

    /// <summary>
    /// 采集数据枚举
    /// </summary>
    public enum CollectDataEnum
    {

        /*
序号  接口  DBneme
1	SuperTest6  MeasDB.mdb
2	TestSoft    TestSoft.mdb
3	SuperTest5  MeasDB.mdb
4	TestMaster  Test.mdb
5	SmartTest   SmartTest.mdb
6	PowerTest   SansMachine-CN.mdb
7	PowerTest V3.0C SansMachine.mdb
8	SmartTest-Y SmartTest-Y.mdb
9	tye tye.mdb
10	SmartTest-2	SmartTest.mdb
11	SmartTest   test.mdb
12	testMachine testMachine.mdb
13	MaxTest MaxTest.mdb
        */


        /// <summary>
        /// 杭州鑫高=>压力机
        /// </summary>
        [Description("杭州鑫高=>压力机")]
        Interface1 = 1,

        /// <summary>
        /// 杭州鑫高=>万能机 l.lss
        /// 由于数据都存在同一个表，因此对数据要进行过滤
        /// </summary>
        [Description("杭州鑫高=>万能机 l.lss")]
        Interface2 = 2,

        /// <summary>
        /// 杭州鑫高
        /// 由于数据都存在同一个表，因此对数据要进行过滤
        /// </summary>
        [Description("杭州鑫高")]
        Interface3 = 3,

        /// <summary>
        /// 杭州鑫高(testMast)
        /// </summary>
        [Description("杭州鑫高(TestMaster)")]
        Interface4 = 4,

        /// <summary>
        /// 济南试金(smartTest)
        /// </summary>
        [Description("济南试金(smartTest)")]
        Interface5 = 5,

        /// <summary>
        /// 新三思(smartTest)
        /// </summary>
        [Description("新三思(smartTest)")]
        Interface6 = 6,

        /// <summary>
        /// 新三思(smartTest)
        /// </summary>
        [Description("新三思(smartTest)")]
        Interface7 = 7
    }
}
