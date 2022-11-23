using GZKL.Client.UI.Common;
using GZKL.Client.UI.ViewsModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Client.UI.Factories.Collect
{
    public class InterfaceId5 : ICollectFactory
    {
        public CollectDataEnum DataEnum => throw new NotImplementedException();

        public override void ImportData(AutoCollectViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public override void QueryDeviceData(AutoCollectViewModel viewModel)
        {
            var baseInterface = viewModel.InterfaceData.FirstOrDefault(w => w.Id == viewModel.Model.InterfaceId);
            if (baseInterface == null)
            {
                throw new Exception("获取选中的接口数据失败");
            }

            var baseInterfaceTestItem = viewModel.InterfaceTestItemData.FirstOrDefault(w => w.Id == viewModel.Model.InterfaceTestItemId);

            if (baseInterfaceTestItem == null)
            {
                throw new Exception("获取选中的接口检测项数据失败");
            }

            if (string.IsNullOrEmpty(baseInterfaceTestItem.TableMaster))
            {
                throw new Exception($"数据库接口[{baseInterfaceTestItem.TestItemName}]配置错误，栏位[table_master]值不能为空，请检查！");
            }
            var tableMasterSql = $"SELECT * FROM {baseInterfaceTestItem.TableMaster} WHERE [编号] ='{viewModel.Model.QuerySampleNo}' AND [试验状态]='已完成'";

            var path = $"{baseInterface.AccessDbPath}";

            if (string.IsNullOrEmpty(path) || !System.IO.File.Exists(path))
            {
                throw new Exception($"数据库文件不存在，请检查！{path}");
            }

            viewModel.Model.UnfinishTestData = OdbcHelper.DataTable(tableMasterSql, path);

        }
    }
}
