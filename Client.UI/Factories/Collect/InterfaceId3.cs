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
    public class InterfaceId3 : ICollectFactory
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

            var tempDeadline = testDataRow["LingQi"].ToString();

            if (tempDeadline.Contains("天"))
            {
                test.Deadline = tempDeadline.Split('天')[0];
            }
            test.TestTime = Convert.ToDateTime(testDataRow["ExpDate"] ?? DateTime.MinValue);

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
                    foreach (DataRow dr in viewModel.Model.UnfinishTestDetailData.Rows)
                    {
                        //检测明细数据
                        var testDetail = new ExecuteTestDetailInfo();

                        testDetail.TestId = testId;
                        testDetail.ExperimentNo = Convert.ToInt32(dr["ShunXu"]);
                        testDetail.PlayTime = Convert.ToDateTime(dr["ExpDate"] ?? DateTime.MinValue);

                        var tempTestDetailDeadline = (dr["LingQi"]??"").ToString();
                        if (tempTestDetailDeadline.Contains("天"))
                        {
                            tempTestDetailDeadline = tempTestDetailDeadline.Split('天')[0];
                        }

                        var tempTestDetailMaxDot = (dr["OneKn"] ?? "").ToString();
                        if (tempTestDetailMaxDot.Contains("*"))
                        {
                            testDetail.MaxDot = tempTestDetailMaxDot.Split('*')[1];
                        }
                        else
                        {
                            testDetail.MaxDot = (dr["OneKn"] ?? "").ToString();
                        }

                        testDetail.SampleWidth = (dr["AVWid"] ?? "0").ToString();
                        testDetail.SampleThick = (dr["AvHei"] ?? "0").ToString();
                        testDetail.SampleDia = (dr["AvLen"] ?? "0").ToString();
                        testDetail.Area = (dr["SArea"] ?? "0").ToString();

                        rowCount = viewModel.Model.TestData?.Count(w => w.OrgNo == test.OrgNo &&
                                                             w.TestItemNo == test.TestItemNo &&
                                                             w.TestNo == test.TestNo &&
                                                             w.SampleNo == test.SampleNo &&
                                                             w.Deadline == tempTestDetailDeadline) ?? 0;
                        if (rowCount > 0)
                        {
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

            viewModel.Model.UnfinishTestData = OdbcHelper.DataTable(tableMasterSql, path);
            viewModel.Model.UnfinishTestDetailData = OdbcHelper.DataTable(tableDetailSql, path);

        }
    }
}
