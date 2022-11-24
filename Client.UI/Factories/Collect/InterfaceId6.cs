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
    public class InterfaceId6 : ICollectFactory
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

            var tempTestNo = testDataRow["TestNo"].ToString();

            var drDeadline = viewModel.Model.UnfinishTestDetailData?.AsEnumerable()?.FirstOrDefault(w=>w.Field<string>("TestNo")== tempTestNo&&w.Field<string>("Name")=="龄期");

            if (drDeadline != null)
            {
                test.Deadline = drDeadline["TheValue"].ToString().Trim();
                if (string.IsNullOrEmpty(test.Deadline))
                {
                    test.Deadline = "0";
                }
            }
            else
            {
                test.Deadline = "0";
            }
            test.TestTime = Convert.ToDateTime(testDataRow["PlayTime"] ?? DateTime.MinValue);

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

                    testDetail.TestId = testId;
                    testDetail.ExperimentNo = Convert.ToInt32(tempTestNo);
                    testDetail.PlayTime = test.TestTime;

                    //获取测试参数
                    var tempArea = testDataRow["Area"].ToString();

                    if (tempArea == "0")
                    {
                        var drArea = viewModel.Model.UnfinishTestDetailData.AsEnumerable().FirstOrDefault(w => w.Field<string>("TestNo") == tempTestNo&& w.Field<string>("Name") == "受压部分面积A");
                        if (drArea != null)
                        {
                            var theValue = drArea["TheValue"].ToString().Trim();
                            if(theValue!="") testDetail.Area=theValue;
                        }
                    }

                    testDetail.SampleDia = testDataRow["SampleDia"].ToString();
                    testDetail.SampleWidth = testDataRow["SampleDia"].ToString();
                    testDetail.SampleThick = testDataRow["SampleThick"].ToString();


                    if (testDetail.SampleDia == "0")
                    {
                        var drSampleDia = viewModel.Model.UnfinishTestDetailData.AsEnumerable().FirstOrDefault(w => w.Field<string>("TestNo") == tempTestNo && w.Field<string>("Name") == "正方形截面边长b");
                        if (drSampleDia != null)
                        {
                            var theValue = drSampleDia["TheValue"].ToString().Trim();
                            if (theValue != "") testDetail.SampleDia = theValue;
                        }
                        else
                        {
                            drSampleDia = viewModel.Model.UnfinishTestDetailData.AsEnumerable().FirstOrDefault(w => w.Field<string>("TestNo") == tempTestNo && w.Field<string>("Name") == "试样直径d");
                            if (drSampleDia != null)
                            {
                                var theValue = drSampleDia["TheValue"].ToString().Trim();
                                if (theValue != "") testDetail.SampleDia = theValue;
                            }
                        }
                    }


                    var drMaxDot = viewModel.Model.UnfinishTestDetailData.AsEnumerable().FirstOrDefault(w => w.Field<string>("TestNo") == tempTestNo && w.Field<string>("Name") == "破坏时的最大荷载Fc");
                    if (drMaxDot != null)
                    {
                        var theValue = drMaxDot["TheValue"].ToString().Trim();
                        if (theValue != "")
                        {
                            var unit = drMaxDot["unit"].ToString().Trim();
                            if (unit == "N" || unit == "")
                            {
                                testDetail.MaxDot = (Convert.ToDouble(theValue) * 0.001).ToString();
                            }
                            else
                            {
                                testDetail.MaxDot = theValue;
                            }
                        }
                    }

                    drMaxDot = viewModel.Model.UnfinishTestDetailData.AsEnumerable().FirstOrDefault(w => w.Field<string>("TestNo") == tempTestNo && w.Field<string>("Name") == "破坏荷载（F）");
                    if (drMaxDot != null)
                    {
                        var theValue = drMaxDot["TheValue"].ToString().Trim();
                        if (theValue != "")
                        {
                            var unit = drMaxDot["unit"].ToString().Trim();
                            if (unit == "N" || unit == "")
                            {
                                testDetail.MaxDot = (Convert.ToDouble(theValue) * 0.001).ToString();
                            }
                            else
                            {
                                testDetail.MaxDot = theValue;
                            }
                        }
                    }

                    drMaxDot = viewModel.Model.UnfinishTestDetailData.AsEnumerable().FirstOrDefault(w => w.Field<string>("TestNo") == tempTestNo && w.Field<string>("Name") == "最大力（Fm）");
                    if (drMaxDot != null)
                    {
                        var theValue = drMaxDot["TheValue"].ToString().Trim();
                        if (theValue != "")
                        {
                            var unit = drMaxDot["unit"].ToString().Trim();
                            if (unit == "N" || unit == "")
                            {
                                testDetail.MaxDot = (Convert.ToDouble(theValue) * 0.001).ToString();
                            }
                            else
                            {
                                testDetail.MaxDot = theValue;
                            }
                        }
                    }

                    drMaxDot = viewModel.Model.UnfinishTestDetailData.AsEnumerable().FirstOrDefault(w => w.Field<string>("TestNo") == tempTestNo && w.Field<string>("Name") == "最大力");
                    if (drMaxDot != null)
                    {
                        var theValue = drMaxDot["TheValue"].ToString().Trim();
                        if (theValue != "")
                        {
                            var unit = drMaxDot["unit"].ToString().Trim();
                            if (unit == "N" || unit == "")
                            {
                                testDetail.MaxDot = (Convert.ToDouble(theValue) * 0.001).ToString();
                            }
                            else
                            {
                                testDetail.MaxDot = theValue;
                            }
                        }
                    }


                    var drGaugeLength = viewModel.Model.UnfinishTestDetailData.AsEnumerable().FirstOrDefault(w => w.Field<string>("TestNo") == tempTestNo && w.Field<string>("Name") == "断后标距（Lu）");
                    if (drGaugeLength != null)
                    {
                        var theValue = drGaugeLength["TheValue"].ToString().Trim();
                        if (theValue != "")
                        {
                            testDetail.GaugeLength = theValue;
                        }
                    }

                    var drUpYieldDot = viewModel.Model.UnfinishTestDetailData.AsEnumerable().FirstOrDefault(w => w.Field<string>("TestNo") == tempTestNo && w.Field<string>("Name") == "上屈服力（FeH）");
                    if (drUpYieldDot != null)
                    {
                        var theValue = drUpYieldDot["TheValue"].ToString().Trim();
                        if (theValue != "")
                        {
                            var unit = drUpYieldDot["unit"].ToString().Trim();
                            if (unit == "N" || unit == "")
                            {
                                testDetail.UpYieldDot = (Convert.ToDouble(theValue) * 0.001).ToString();
                            }
                            else
                            {
                                testDetail.UpYieldDot = theValue;
                            }
                        }
                    }

                    var drDownYieldDot = viewModel.Model.UnfinishTestDetailData.AsEnumerable().FirstOrDefault(w => w.Field<string>("TestNo") == tempTestNo && w.Field<string>("Name") == "屈服力（FeL）");
                    if (drDownYieldDot != null)
                    {
                        var theValue = drDownYieldDot["TheValue"].ToString().Trim();
                        if (theValue != "")
                        {
                            var unit = drDownYieldDot["unit"].ToString().Trim();
                            if (unit == "N" || unit == "")
                            {
                                testDetail.DownYieldDot = (Convert.ToDouble(theValue) * 0.001).ToString();
                            }
                            else
                            {
                                testDetail.DownYieldDot = theValue;
                            }
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
            var tableMasterSql = $"SELECT m.* FROM TestNo m INNER JOIN ParamFactValue d ON m.testno=d.testNO WHERE d.TheValue='{viewModel.Model.QuerySampleNo}'";
            var tableDetailSql = $"SELECT m.* FROM ParamFactValue m INNER JOIN ParamFactValue d ON m.testno=d.testNO WHERE d.TheValue='{viewModel.Model.QuerySampleNo}'";

            var path = $"{baseInterface.AccessDbPath}";

            if (string.IsNullOrEmpty(path) || !System.IO.File.Exists(path))
            {
                throw new Exception($"数据库文件不存在，请检查！{path}");
            }

            viewModel.Model.UnfinishTestData = OdbcHelper.DataTable(tableMasterSql, path);
            viewModel.Model.UnfinishTestDetailData = OdbcHelper.DataTable(tableDetailSql, path);

        }
    }
}
