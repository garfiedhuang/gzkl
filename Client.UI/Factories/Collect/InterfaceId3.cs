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
    public class InterfaceId3 : ICollectFactory
    {
        public CollectDataEnum DataEnum => throw new NotImplementedException();

        public override void ImportData()
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

            var tableMasterSql = $"SELECT * FROM {baseInterfaceTestItem.TableMaster} WHERE Zuhao ='{viewModel.Model.QuerySampleNo}'";
            var tableDetailSql = $"SELECT * FROM {baseInterfaceTestItem.TableDetail} WHERE Zuhao ='{viewModel.Model.QuerySampleNo}'";

            var dsnName = $"AutoAcs_{baseInterface.Uid}_{nameof(InterfaceId3)}DB";
            var pwd = baseInterface.Pwd;
            var database = baseInterface.AccessDbPath;
            var path = $"DSN={dsnName}";

            if (string.IsNullOrEmpty(database) || !System.IO.File.Exists(database))
            {
                throw new Exception($"数据库文件不存在，请检查！{database}");
            }

            DsnHelper.CreateDSN(dsnName, pwd, database);//创建DSN

            viewModel.Model.UnfinishTestData = OdbcDBHelper.DataTable(tableMasterSql, path);
            viewModel.Model.UnfinishTestDetailData = OdbcDBHelper.DataTable(tableDetailSql, path);

        }
    }
}
