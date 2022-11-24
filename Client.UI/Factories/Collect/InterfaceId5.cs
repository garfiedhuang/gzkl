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
    public class InterfaceId5 : ICollectFactory
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


            var testTemperature = testDataRow["试验温度"].ToString();

            testTemperature = testTemperature.Replace("龄期","").Replace("d","").Replace("D","");

            try
            {
                test.Deadline = Convert.ToInt32(testTemperature).ToString();
            }
            catch { }

            if (!testDataRow.IsNull("试验日期"))
            {
                test.TestTime = Convert.ToDateTime(testDataRow["试验日期"] ?? DateTime.MinValue);
            }

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
                if (testId > 0)
                {
                    //检测明细数据
                    var testDetail = new ExecuteTestDetailInfo();

                    testDetail.TestId = testId;

                    var tempNum = testDataRow["num"].ToString();
                    if (!string.IsNullOrEmpty(tempNum) && tempNum.Length >= 7)
                    {
                        //ExperimentNo:= strtoint(copy(num, length(num)-3,4));
                        testDetail.ExperimentNo =Convert.ToInt32(tempNum.Substring(tempNum.Length-2,4));
                    }


                    testDetail.PlayTime = test.TestTime;

                    if (!testDataRow.IsNull("最大力"))
                    {
                        testDetail.MaxDot = (testDataRow["最大力"] ?? "").ToString().Trim();
                        if (string.IsNullOrEmpty(testDetail.MaxDot))
                        {
                            testDetail.MaxDot = "0";
                        }
                    }
                    if (!testDataRow.IsNull("断后标距"))
                    {
                        testDetail.GaugeLength = (testDataRow["断后标距"] ?? "").ToString().Trim();
                        if (string.IsNullOrEmpty(testDetail.GaugeLength))
                        {
                            testDetail.GaugeLength = "0";
                        }
                    }
                    if (!testDataRow.IsNull("上屈服力"))
                    {
                        testDetail.UpYieldDot = (testDataRow["上屈服力"] ?? "").ToString().Trim();
                        if (string.IsNullOrEmpty(testDetail.UpYieldDot))
                        {
                            testDetail.UpYieldDot = "0";
                        }
                    }
                    if (!testDataRow.IsNull("下屈服力"))
                    {
                        testDetail.DownYieldDot = (testDataRow["下屈服力"] ?? "").ToString().Trim();
                        if (string.IsNullOrEmpty(testDetail.DownYieldDot))
                        {
                            testDetail.DownYieldDot = "0";
                        }
                    }
                    if (!testDataRow.IsNull("面积"))
                    {
                        testDetail.Area = (testDataRow["面积"] ?? "").ToString().Trim();
                        if (string.IsNullOrEmpty(testDetail.Area))
                        {
                            testDetail.Area = "0";
                        }
                    }

                    if (!testDataRow.IsNull("尺寸1"))
                    {
                        testDetail.SampleDia = (testDataRow["尺寸1"] ?? "").ToString().Trim();
                        if (string.IsNullOrEmpty(testDetail.SampleDia))
                        {
                            testDetail.SampleDia = "0";
                        }
                    }
                    if (!testDataRow.IsNull("尺寸2"))
                    {
                        testDetail.SampleWidth = (testDataRow["尺寸2"] ?? "").ToString().Trim();
                        if (string.IsNullOrEmpty(testDetail.SampleWidth))
                        {
                            testDetail.SampleWidth = "0";
                        }
                    }
                    if (!testDataRow.IsNull("尺寸3"))
                    {
                        testDetail.SampleThick = (testDataRow["尺寸3"] ?? "").ToString().Trim();
                        if (string.IsNullOrEmpty(testDetail.SampleThick))
                        {
                            testDetail.SampleThick = "0";
                        }
                    }

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
