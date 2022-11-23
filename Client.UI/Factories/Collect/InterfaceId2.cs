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
    public class InterfaceId2 : ICollectFactory
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
            var tableMasterSql = $"SELECT * FROM {baseInterfaceTestItem.TableMaster} WHERE testID ='{viewModel.Model.QuerySampleNo}' AND IntestID<>0 AND TestName='{baseInterfaceTestItem.TestItemName}'";
            
            var path = baseInterface.AccessDbPath;
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
                var accessDbName = baseInterface.AccessDbName.ToUpper().Trim();

                var tempTestDetailDbPath = string.Empty;

                if (accessDbPath.Contains(accessDbName))
                {
                    tempTestDetailDbPath = $"{accessDbPath.Replace(accessDbName,"")}data\\{baseInterfaceTestItem.TestItemName}\\{viewModel.Model.QuerySampleNo}.mdb";
                }

                if (string.IsNullOrEmpty(tempTestDetailDbPath) || !System.IO.File.Exists(tempTestDetailDbPath))
                {
                    throw new Exception($"数据库文件不存在，请检查！{tempTestDetailDbPath}");
                }

                var tableDetailSql = $"TRANSFORM Last(Params.ParamValue) AS ParamValue SELECT Params.InTestID, Last(Params.ParamValue) AS [总计] FROM Params WHERE IntestID<>0 and ParamType='结果参数' GROUP BY Params.InTestID PIVOT Params.ParamName";//表交叉查询

                //明细表数据
                viewModel.Model.UnfinishTestDetailData = OleDbHelper.DataTable(tableDetailSql, tempTestDetailDbPath);

                //dot数据
                if (string.IsNullOrEmpty(baseInterfaceTestItem.TableDot))
                {
                    throw new Exception($"数据库接口[{baseInterfaceTestItem.TestItemName}]配置错误，栏位[table_dot]值不能为空，请检查！");
                }
                var tableDotSql = $"SELECT * FROM {baseInterfaceTestItem.TableDot} WHERE IntestID<>0 ORDER BY IntestID,id";

                viewModel.Model.UnfinishOriginalData = OleDbHelper.DataTable(tableDotSql, tempTestDetailDbPath);

            }

        }
    }
}
