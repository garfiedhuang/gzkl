using GZKL.Client.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Client.UI.Factories
{
    public interface ICollectFactory
    {
        /// <summary>
        /// 采集数据枚举
        /// </summary>
        CollectDataEnum DataEnum { get; }

        /// <summary>
        /// 查询数据
        /// </summary>
        void QueryData();

        /// <summary>
        /// 导入数据
        /// </summary>
        void ImportData();
    }
}
