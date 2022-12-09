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
    public class InterfaceId2 : ICollectFactory
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

            test.Deadline = (testDataRow["Age_Int"] ?? "0").ToString();
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
                    testDetail.ExperimentNo = Convert.ToInt32(dr["IntestID"]);
                    testDetail.TestTime = Convert.ToDateTime(dr["testdate"] ?? DateTime.MinValue);

                    if (!dr.IsNull("最大力(Fm)"))
                    {
                        testDetail.MaxDot = (dr["最大力(Fm)"] ?? "0").ToString();
                    }
                    else if (!dr.IsNull("最大力"))
                    {
                        testDetail.MaxDot = (dr["最大力"] ?? "0").ToString();
                    }
                    else if (!dr.IsNull("试件破坏载荷(F)"))
                    {
                        testDetail.MaxDot = (dr["试件破坏载荷(F)"] ?? "0").ToString();
                    }
                    else if (!dr.IsNull("立方体破坏压力(Nu)"))
                    {
                        testDetail.MaxDot = (dr["立方体破坏压力(Nu)"] ?? "0").ToString();
                    }
                    else if (!dr.IsNull("最大弯曲力(Fbb)"))
                    {
                        testDetail.MaxDot = (dr["最大弯曲力(Fbb)"] ?? "0").ToString();
                    }
                    else if (!dr.IsNull("最大实际压缩力(Fmc)"))
                    {
                        testDetail.MaxDot = (dr["最大实际压缩力(Fmc)"] ?? "0").ToString();
                    }
                    else if (!dr.IsNull("破坏时的最大载荷(Fc)"))
                    {
                        testDetail.MaxDot = (dr["破坏时的最大载荷(Fc)"] ?? "0").ToString();
                    }


                    if (!dr.IsNull("断后标距(Lu)"))
                    {
                        testDetail.GaugeLength = (dr["断后标距(Lu)"] ?? "0").ToString();
                    }


                    if (!dr.IsNull("上屈服力"))
                    {
                        var tempUpYieldDot = dr["上屈服力"].ToString().Trim();

                        if (!string.IsNullOrEmpty(tempUpYieldDot))
                        {
                            testDetail.UpYieldDot = tempUpYieldDot;
                        }
                    }
                    else if (!dr.IsNull("上屈服压缩力(FeHc)"))
                    {
                        var tempUpYieldDot = dr["上屈服压缩力(FeHc)"].ToString().Trim();

                        if (!string.IsNullOrEmpty(tempUpYieldDot))
                        {
                            testDetail.UpYieldDot = tempUpYieldDot;
                        }
                    }


                    if (!dr.IsNull("下屈服力"))
                    {
                        var tempDownYieldDot = dr["下屈服力"].ToString().Trim();

                        if (!string.IsNullOrEmpty(tempDownYieldDot))
                        {
                            testDetail.DownYieldDot = tempDownYieldDot;
                        }
                    }
                    else if (!dr.IsNull("下屈服压缩力(FeLc)"))
                    {
                        var tempDownYieldDot = dr["下屈服压缩力(FeLc)"].ToString().Trim();

                        if (!string.IsNullOrEmpty(tempDownYieldDot))
                        {
                            testDetail.DownYieldDot = tempDownYieldDot;
                        }
                    }

                    //判断当前明细表是否存在检测记录
                    rowCount = viewModel.Model.TestDetailData?.Count(w => w.TestId == testId &&
                                                                          w.ExperimentNo == testDetail.ExperimentNo) ?? 0;
                    if (rowCount == 0)
                    {
                        base.AddTestDetail(test.SampleNo, testDetail);
                    }

                    //检测点表数据
                    if (viewModel.Model.UnfinishOriginalData?.Rows?.Count > 0)
                    {
                        var originalData = new ExecuteOriginalDataInfo();

                        var originDataRow = viewModel.Model.UnfinishOriginalData.Rows[0];

                        originalData.ExperimentNo = Convert.ToInt32(originDataRow["IntestID"] ?? "0");
                        originalData.TestTime = Convert.ToDateTime(originDataRow["TimeValue"] ?? DateTime.MinValue);
                        originalData.LoadValue = (originDataRow["LoadValue"] ?? "0").ToString();
                        originalData.PositionValue = (originDataRow["PosiValue"] ?? "0").ToString();
                        originalData.ExtendValue = (originDataRow["ExtnValue"] ?? "0").ToString();
                        originalData.BigDeformValue = (originDataRow["LoadValue"] ?? "0").ToString();

                        rowCount = viewModel.Model.OriginalData?.Count(w => w.TestId == testId &&
                                                                          w.ExperimentNo == originalData.ExperimentNo &&
                                                                          w.TestTime == originalData.TestTime) ?? 0;

                        if (rowCount == 0)
                        {
                            base.AddOriginalData(test.SampleNo, originalData);
                        }
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
