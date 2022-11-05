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
}
