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
    public class InterfaceId4 : ICollectFactory
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

            if (string.IsNullOrEmpty(baseInterfaceTestItem.TableMaster))
            {
                throw new Exception($"数据库接口[{baseInterfaceTestItem.TestItemName}]配置错误，栏位[table_master]值不能为空，请检查！");
            }
            var tableMasterSql = $"SELECT * FROM {baseInterfaceTestItem.TableMaster} WHERE GroupName ='{viewModel.Model.QuerySampleNo}' AND State=2";

            var path = $"{baseInterface.AccessDbPath}\\{baseInterfaceTestItem.TestItemName}\\{baseInterface.AccessDbName}";

            if (string.IsNullOrEmpty(path) || !System.IO.File.Exists(path))
            {
                throw new Exception($"数据库文件不存在，请检查！{path}");
            }

            //主表数据
            viewModel.Model.UnfinishTestData = OleDbHelper.DataTable(tableMasterSql, path);

            if (viewModel.Model.UnfinishTestData?.Rows?.Count > 0)
            {
                //此设备检测数据库是动态的，即数据库名称不固定
                var accessDbPath = baseInterface.AccessDbPath.ToUpper().Trim();
                var groupId = viewModel.Model.UnfinishTestData.Rows[0]["GroupID"].ToString();
                var tempTestDetailDbPath = $"{accessDbPath}{baseInterfaceTestItem.TestItemName}\\data\\{groupId}.mdb";

                if (string.IsNullOrEmpty(tempTestDetailDbPath) || !System.IO.File.Exists(tempTestDetailDbPath))
                {
                    throw new Exception($"数据库文件不存在，请检查！{tempTestDetailDbPath}");
                }

                //明细表数据
                if (string.IsNullOrEmpty(baseInterfaceTestItem.TableDetail))
                {
                    throw new Exception($"数据库接口[{baseInterfaceTestItem.TestItemName}]配置错误，栏位[table_detail]值不能为空，请检查！");
                }
                var tableDetailSql = $"SELECT * FROM {baseInterfaceTestItem.TableDetail} ORDER BY testCode";
                viewModel.Model.UnfinishTestDetailData = OleDbHelper.DataTable(tableDetailSql, tempTestDetailDbPath);

                //dot数据
                if (string.IsNullOrEmpty(baseInterfaceTestItem.TableDot))
                {
                    throw new Exception($"数据库接口[{baseInterfaceTestItem.TestItemName}]配置错误，栏位[table_dot]值不能为空，请检查！");
                }

                var tableDotSql = $"SELECT * FROM {baseInterfaceTestItem.TableDot} WHERE 1=1 ORDER BY testid,dataIndex";

                viewModel.Model.UnfinishOriginalData = OleDbHelper.DataTable(tableDotSql, tempTestDetailDbPath);

            }

        }
    }
}
