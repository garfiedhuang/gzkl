using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.ViewsModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Client.UI.Factories.Collect
{
    public class InterfaceId4 : ICollectFactory
    {
        public CollectDataEnum DataEnum => throw new NotImplementedException();

        public override void ImportData(AutoCollectViewModel viewModel)
        {
            var testDataRow = viewModel.Model.UnfinishTestData?.Rows[0];

            if (testDataRow == null)
            {
                throw new Exception("不存在检测主数据");
            }

            //检测主数据
            var test = new ExecuteTestInfo();

            var groupId = testDataRow["GroupId"].ToString();

            if (!string.IsNullOrEmpty(groupId))
            {
                test.Deadline = GetDeadline(groupId);
            }
            test.TestTime = Convert.ToDateTime(testDataRow["testdate"] ?? DateTime.MinValue);

            test.OrgNo = viewModel.Model.OrgNo;
            test.TestItemNo = viewModel.Model.SystemTestItemNo;
            test.TestNo = viewModel.Model.QueryTestNo;
            test.SampleNo = viewModel.Model.QuerySampleNo;

            //判断当前主表是否存在检测记录
            var rowCount = viewModel.Model.TestData?.Count(w => w.OrgNo == test.OrgNo &&
                                                             w.TestItemNo == test.TestItemNo &&
                                                             w.TestNo == test.TestNo &&
                                                             w.SampleNo == test.SampleNo &&
                                                             w.Deadline == test.Deadline) ?? 0;
            if (rowCount == 0)
            {
                var testId = base.AddTest(test);
                if (testId > 0 && viewModel.Model.UnfinishTestDetailData?.Rows?.Count > 0)
                {
                    //检测明细数据
                    var testDetail = new ExecuteTestDetailInfo();
                    var dr = viewModel.Model.UnfinishTestDetailData.Rows[0];

                    testDetail.TestId = testId;
                    testDetail.ExperimentNo = Convert.ToInt32(dr["testcode"]);
                    testDetail.TestTime = Convert.ToDateTime(dr["TestDate"] ?? DateTime.MinValue);

                    //获取测试参数
                    var tempTestId = dr["testid"].ToString();
                    var dtTestParams = GetTestParam(tempTestId);

                    var drMaxDot = dtTestParams?.AsEnumerable().FirstOrDefault(w => w.Field<string>("paramName") == "最大力");
                    if (drMaxDot != null)
                    {
                        testDetail.LoadUnitName = drMaxDot["UnitSign"].ToString();
                        if (testDetail.LoadUnitName == "kN")
                        {
                            testDetail.MaxDot = (Convert.ToDouble(drMaxDot["ParamValue"] ?? "0") * 0.001).ToString();
                        }
                        else
                        {
                            testDetail.MaxDot = (drMaxDot["ParamValue"] ?? "0").ToString();
                        }
                    }


                    var drUpYieldDot = dtTestParams?.AsEnumerable().FirstOrDefault(w => w.Field<string>("paramName") == "上屈服");
                    if (drUpYieldDot != null)
                    {
                        if (testDetail.LoadUnitName == "kN")
                        {
                            testDetail.UpYieldDot = (Convert.ToDouble(drUpYieldDot["ParamValue"] ?? "0") * 0.001).ToString();
                        }
                        else
                        {
                            testDetail.UpYieldDot = (drUpYieldDot["ParamValue"] ?? "0").ToString();
                        }
                    }

                    var drDownYieldDot = dtTestParams?.AsEnumerable().FirstOrDefault(w => w.Field<string>("paramName") == "下屈服");
                    if (drDownYieldDot != null)
                    {
                        if (testDetail.LoadUnitName == "kN")
                        {
                            testDetail.DownYieldDot = (Convert.ToDouble(drDownYieldDot["ParamValue"] ?? "0") * 0.001).ToString();
                        }
                        else
                        {
                            testDetail.DownYieldDot = (drDownYieldDot["ParamValue"] ?? "0").ToString();
                        }
                    }


                    var drArea = dtTestParams?.AsEnumerable().FirstOrDefault(w => w.Field<string>("paramName") == "原始截面积");
                    if (drArea != null)
                    {
                        try
                        {
                            testDetail.Area = Convert.ToDouble(drArea["ParamValue"] ?? "0").ToString();
                        }
                        catch { }
                    }

                    var drGaugeLength = dtTestParams?.AsEnumerable().FirstOrDefault(w => w.Field<string>("paramName") == "断后标距");
                    if (drGaugeLength != null)
                    {
                        try
                        {
                            testDetail.GaugeLength = Convert.ToDouble(drGaugeLength["ParamValue"] ?? "0").ToString();
                        }
                        catch { }
                    }

                    var drSampleDia = dtTestParams?.AsEnumerable().FirstOrDefault(w => w.Field<string>("paramName") == "圆形直径");
                    if (drSampleDia != null)
                    {
                        try
                        {
                            testDetail.SampleDia = Convert.ToDouble(drSampleDia["ParamValue"] ?? "0").ToString();
                        }
                        catch { }
                    }

                    drSampleDia = dtTestParams?.AsEnumerable().FirstOrDefault(w => w.Field<string>("paramName") == "圆柱体直径");
                    if (drSampleDia != null)
                    {
                        try
                        {
                            testDetail.SampleDia = Convert.ToDouble(drSampleDia["ParamValue"] ?? "0").ToString();
                        }
                        catch { }
                    }

                    var drSampleThick = dtTestParams?.AsEnumerable().FirstOrDefault(w => w.Field<string>("paramName") == "圆柱体高度");
                    if (drSampleThick != null)
                    {
                        try
                        {
                            testDetail.SampleThick = Convert.ToDouble(drSampleThick["ParamValue"] ?? "0").ToString();
                        }
                        catch { }
                    }

                    var drSampleWidth = dtTestParams?.AsEnumerable().FirstOrDefault(w => w.Field<string>("paramName") == "立方体边长");
                    if (drSampleWidth != null)
                    {
                        try
                        {
                            testDetail.SampleWidth = Convert.ToDouble(drSampleWidth["ParamValue"] ?? "0").ToString();
                        }
                        catch { }
                    }

                    //testDetail.DeformUnitName = "mm";//旧系统未使用的变量

                    //判断当前明细表是否存在检测记录
                    rowCount = viewModel.Model.TestDetailData?.Count(w => w.TestId == testId &&
                                                                          w.ExperimentNo == testDetail.ExperimentNo) ?? 0;
                    if (rowCount == 0)
                    {
                        base.AddTestDetail(test.SampleNo, testDetail);
                    }

                }
            }
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

        /// <summary>
        /// 获取龄期
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        private string GetDeadline(string groupId)
        {
            var sql = @"SELECT * FROM GroupParam WHERE GroupID=@groupId";
            var parameters = new SqlParameter[1] { new SqlParameter("@groupId",groupId) };
            var deadline = string.Empty;

            using (var dt = SQLHelper.GetDataTable(sql, parameters))
            {
                if (dt?.Rows?.Count > 0)
                {
                    var dr = dt.AsEnumerable().FirstOrDefault(w => w.Field<string>("ParamName") == "试验龄期");
                    if (dr == null)
                    {
                        deadline = "0";
                    }
                    else if (dr != null && !dr.IsNull("ParamValue"))
                    {
                        deadline = dr["ParamValue"].ToString();
                    }
                    else
                    {
                        deadline = "28";
                    }
                }
                else
                {
                    deadline = "0";
                }
            }

            return deadline;
        }

        /// <summary>
        /// 获取测试参数
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        private DataTable GetTestParam(string testId)
        {
            var sql = @"SELECT * FROM testParam WHERE isactive=True AND ParamType IN(2,3) AND testid=@testId";
            var parameters = new SqlParameter[1] { new SqlParameter("@testId", testId) };

           return SQLHelper.GetDataTable(sql, parameters);
        }
    }
}
