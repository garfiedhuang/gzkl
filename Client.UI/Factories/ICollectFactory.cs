using GZKL.Client.UI.Common;
using GZKL.Client.UI.ViewsModels;
using System;
using System.Collections.Generic;
using System.Data;
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
        /// 查询数据(第三方设备Access数据库)
        /// </summary>
        /// <param name="viewModel"></param>
        void QueryData(AutoCollectViewModel viewModel);

        /// <summary>
        /// 导入数据
        /// </summary>
        void ImportData();
    }
}
