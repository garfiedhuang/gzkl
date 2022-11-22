using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.ViewsModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Client.UI.Factories.Collect
{
    public class InterfaceId1 : ICollectFactory
    {
        public CollectDataEnum DataEnum => throw new NotImplementedException();

        public void ImportData()
        {
            throw new NotImplementedException();
        }

        public void QueryData(AutoCollectViewModel viewModel)
        {
            var baseInterfaceTestItem = viewModel.InterfaceTestItemData.FirstOrDefault(w=>w.Id==viewModel.Model.InterfaceTestItemId);

            if (baseInterfaceTestItem == null)
            {
                throw new Exception("获取选中的接口检测项数据失败");
            }

            var tableMasterSql = $"SELECT * FROM {baseInterfaceTestItem.TableMaster} WHERE Zuhao ='{viewModel.Model.QuerySampleNo}'";
            var tableDetailSql = $@"SELECT * FROM {baseInterfaceTestItem.TableDetail} WHERE Zuhao ='{viewModel.Model.QuerySampleNo}'";

        }
    }
}
