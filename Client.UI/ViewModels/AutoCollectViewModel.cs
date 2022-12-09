using GalaSoft.MvvmLight;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.Common;
using MessageBox = HandyControl.Controls.MessageBox;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GZKL.Client.UI.ViewsModels
{
    public class AutoCollectViewModel : ViewModelBase
    {
        #region Construct and property

        /// <summary>
        /// 构造函数
        /// </summary>
        public AutoCollectViewModel()
        {

#if DEBUG
            /*
                        DsnHelper.CreateDSN("AutoAcsDB", "AutoAcs", "F:\\gzkl\\gzkl-source\\db\\Press2.mdb");
                        var dt = OdbcDBHelper.DataTable("select * from base_compa", "");
                        //var dt = AccessDBHelper.DataTable("select * from base_compa", "");
            */
#endif

            SelectorData = new List<SelectorModel>();

            OrgData = new List<OrgInfo>();
            InterfaceData = new List<InterfaceInfo>();
            TestTypeData = new List<TestTypeInfo>();
            SystemTestItemData = new List<SystemTestItemInfo>();
            InterfaceTestItemData = new List<InterfaceTestItemInfo>();

            Model = new AutoCollectModel();

            this.InitData();
            this.SetInterface();
        }

        private List<SelectorModel> selectorData;
        /// <summary>
        /// 选择器数据源
        /// </summary>
        public List<SelectorModel> SelectorData
        {
            get { return selectorData; }
            set { selectorData = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 机构下拉数据源
        /// </summary>
        public List<OrgInfo> OrgData { get; set; }

        /// <summary>
        /// 接口下拉数据源
        /// </summary>
        public List<InterfaceInfo> InterfaceData { get; set; }

        /// <summary>
        /// 检测类型下拉数据源
        /// </summary>
        public List<TestTypeInfo> TestTypeData { get; set; }

        /// <summary>
        /// 系统检测项下拉数据源
        /// </summary>
        public List<SystemTestItemInfo> SystemTestItemData { get; set; }

        /// <summary>
        /// 接口检测项下拉数据源
        /// </summary>
        public List<InterfaceTestItemInfo> InterfaceTestItemData { get; set; }

        /// <summary>
        /// 网格数据集合
        /// </summary>
        private AutoCollectModel model;
        public AutoCollectModel Model
        {
            get { return model; }
            set { model = value; RaisePropertyChanged(); }
        }

        #endregion

        #region Command


        #endregion

        #region Command implement

        /// <summary>
        /// 查询
        /// </summary>
        public void Query()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        public void Save()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        #endregion

        #region Privates

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitData()
        {
            try
            {
                var sql0 = @"SELECT * FROM [dbo].[base_org] WHERE [is_deleted]=0";

                var sql1 = @"SELECT * FROM [dbo].[base_interface] WHERE [is_deleted]=0";

                var sql2 = @"SELECT * FROM [dbo].[base_interface_test_item] WHERE [is_deleted]=0";

                var sql3 = @"SELECT * FROM [dbo].[base_test_item] WHERE [is_deleted]=0";

                var sql4 = @"SELECT * FROM [dbo].[base_test_type] WHERE [is_deleted]=0";

                //机构信息
                using (var data = SQLHelper.GetDataTable(sql0))
                {
                    if (OrgData?.Count > 0)
                    {
                        OrgData.Clear();
                    }

                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            OrgData.Add(new OrgInfo()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                OrgNo = dataRow["org_no"].ToString(),
                                OrgName = dataRow["org_name"].ToString(),
                                OrgLevel = dataRow["org_level"].ToString(),
                                Remark = dataRow["remark"].ToString()
                            });
                        }
                    }
                }

                //接口信息
                using (var data = SQLHelper.GetDataTable(sql1))
                {
                    if (InterfaceData?.Count > 0)
                    {
                        InterfaceData.Clear();
                    }

                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            InterfaceData.Add(new InterfaceInfo()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                InterfaceName = dataRow["interface_name"].ToString(),
                                AccessDbPath = dataRow["access_db_path"].ToString(),
                                AccessDbName = dataRow["access_db_name"].ToString(),
                                Uid = dataRow["uid"].ToString(),
                                Pwd = dataRow["pwd"].ToString(),
                                Remark = dataRow["remark"].ToString(),
                                IsEnabled = Convert.ToInt32(dataRow["is_enabled"]),
                                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                                UpdateDt = Convert.ToDateTime(dataRow["update_dt"])
                            });
                        }
                    }
                }

                //接口对应检测项目
                using (var data = SQLHelper.GetDataTable(sql2))
                {
                    if (InterfaceTestItemData?.Count > 0)
                    {
                        InterfaceTestItemData.Clear();
                    }

                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            InterfaceTestItemData.Add(new InterfaceTestItemInfo()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                InterfaceId = Convert.ToInt64(dataRow["interface_id"]),
                                TestItemName = dataRow["test_item_name"].ToString(),
                                TableMaster = dataRow["table_master"].ToString(),
                                TableDetail = dataRow["table_detail"].ToString(),
                                TableDot = dataRow["table_dot"].ToString(),
                                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                                UpdateDt = Convert.ToDateTime(dataRow["update_dt"])
                            });
                        }
                    }
                }

                //系统对应检测项目
                using (var data = SQLHelper.GetDataTable(sql3))
                {
                    if (SystemTestItemData?.Count > 0)
                    {
                        SystemTestItemData.Clear();
                    }

                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            SystemTestItemData.Add(new SystemTestItemInfo()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                TestItemNo = dataRow["test_item_no"].ToString(),
                                TestItemName = dataRow["test_item_name"].ToString(),
                                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                                UpdateDt = Convert.ToDateTime(dataRow["update_dt"])
                            });
                        }
                    }
                }

                //检测类型
                using (var data = SQLHelper.GetDataTable(sql4))
                {
                    if (TestTypeData?.Count > 0)
                    {
                        TestTypeData.Clear();
                    }

                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            TestTypeData.Add(new TestTypeInfo()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                Category = dataRow["category"].ToString(),
                                TestTypeNo = dataRow["test_type_no"].ToString(),
                                TestTypeName = dataRow["test_type_name"].ToString(),
                                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                                UpdateDt = Convert.ToDateTime(dataRow["update_dt"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        /// <summary>
        /// 设置接口
        /// </summary>
        public void SetInterface()
        {
            if (InterfaceData?.Count > 0)
            {
                Model.InterfaceId = InterfaceData.FirstOrDefault(w => w.IsEnabled == 1).Id;
                Model.InterfaceName = InterfaceData.FirstOrDefault(w => w.IsEnabled == 1).InterfaceName;
            }
        }

        /// <summary>
        /// 获取系统检测项数据
        /// </summary>
        /// <param name="filter"></param>
        public void GetSystemTestItemData(bool filter = true)
        {
            try
            {
                var sql = string.Empty;
                var parameters = new SqlParameter[] { };

                if (filter)
                {
                    sql = @"SELECT bti.* FROM [dbo].[base_test_item] bti INNER JOIN [dbo].[base_test_type_item] btti ON bti.test_item_no=btti.test_item_no 
	  WHERE bti.is_deleted=0 AND btti.is_deleted=0 AND btti.test_type_no=@testTypeNo";

                    parameters = new SqlParameter[1] { new SqlParameter("@testTypeNo", Model.QueryTestNo) };
                }
                else
                {
                    sql = @"SELECT * FROM [dbo].[base_test_item] WHERE [is_deleted]=0";
                    parameters = null;
                }

                //系统对应检测项目
                using (var data = SQLHelper.GetDataTable(sql, parameters))
                {
                    if (SystemTestItemData?.Count > 0)
                    {
                        SystemTestItemData.Clear();
                    }

                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            SystemTestItemData.Add(new SystemTestItemInfo()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                TestItemNo = dataRow["test_item_no"].ToString(),
                                TestItemName = dataRow["test_item_name"].ToString(),
                                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                                UpdateDt = Convert.ToDateTime(dataRow["update_dt"])
                            });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        /// <summary>
        /// 获取接口检测项数据
        /// </summary>
        public void GetInterfaceTestItemData(bool filter = true)
        {
            try
            {
                var sql = string.Empty;
                var parameters = new SqlParameter[] { };

                if (filter)
                {
                    sql = @"SELECT biti.* FROM [dbo].[base_interface_test_item] biti INNER JOIN [dbo].[base_interface_relation] bir ON biti.id=bir.test_item_id
	  WHERE biti.is_deleted=0 AND bir.is_deleted=0 AND bir.interface_id=@interfaceId AND bir.test_item_no =@testItemNo";

                    parameters = new SqlParameter[2] { new SqlParameter("@interfaceId", Model.InterfaceId), new SqlParameter("@testItemNo", Model.SystemTestItemNo) };
                }
                else
                {
                    sql = @"SELECT * FROM [dbo].[base_interface_test_item] WHERE [is_deleted]=0 AND [interface_id]=@interfaceId";
                    parameters = new SqlParameter[1] { new SqlParameter("@interfaceId", Model.InterfaceId) };
                }


                //系统对应检测项目
                using (var data = SQLHelper.GetDataTable(sql, parameters))
                {
                    if (InterfaceTestItemData?.Count > 0)
                    {
                        InterfaceTestItemData.Clear();
                    }

                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            InterfaceTestItemData.Add(new InterfaceTestItemInfo()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                //TestItemNo = dataRow["test_item_no"].ToString(),
                                TestItemName = dataRow["test_item_name"].ToString(),
                                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                                UpdateDt = Convert.ToDateTime(dataRow["update_dt"])
                            });
                        }
                    }
                }

                //如果过滤查询不到数据，仍需要返回基础表数据
                if (filter && InterfaceTestItemData?.Count == 0)
                {
                    GetInterfaceTestItemData(false);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        /// <summary>
        /// 参数校验
        /// </summary>
        /// <returns></returns>
        public int CheckValue()
        {
            var registerInfo = SessionInfo.Instance.RegisterInfo;

            if (string.IsNullOrEmpty(registerInfo?.RegCode))
            {
                MessageBox.Show("软件未注册，请与开发商联系！", "操作提示");
                return -1;
            }
            else if (registerInfo.RegTime.AddYears(2) <= DateTime.Now)
            {
                MessageBox.Show("注册码已过期，请与开发商联系，重新注册！", "操作提示");
                return -1;
            }

            if (string.IsNullOrEmpty(Model.OrgNo))
            {
                MessageBox.Show("请选择检测机构！", "操作提示");
                return -1;
            }

            if (string.IsNullOrEmpty(Model.SystemTestItemNo))
            {
                MessageBox.Show("请选择检测项目！", "操作提示");
                return -1;
            }

            if (string.IsNullOrEmpty(Model.TestTypeNo))
            {
                MessageBox.Show("请选择检测类型！", "操作提示");
                return -1;
            }

            if (string.IsNullOrEmpty(Model.QueryTestNo))
            {
                MessageBox.Show("请输入检测编号！", "操作提示");
                return -1;
            }

            if (string.IsNullOrEmpty(Model.QuerySampleNo))
            {
                MessageBox.Show("请输入样品编号！", "操作提示");
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        public void QueryData()
        {
            var orgNo = model.OrgNo;                 //机构代码
            var testItemNo = model.SystemTestItemNo; //检测项编号
            var testNo = model.QueryTestNo;          //检测编号

            var sql = @"SELECT * FROM [dbo].[biz_execute_test] WHERE [is_deleted]=0 AND [org_no]=@orgNo AND [test_item_no]=@testItemNo AND [test_no]=@testNo";
            var parameters = new SqlParameter[3] {
                    new SqlParameter("@orgNo", orgNo),
                    new SqlParameter("@testItemNo", testItemNo),
                    new SqlParameter("@testNo", testNo)
            };

            //检测主表信息
            using (var data = SQLHelper.GetDataTable(sql,parameters))
            {
                if (Model.TestData?.Count > 0)
                {
                    Model.TestData.Clear();
                }

                if (data != null && data.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in data.Rows)
                    {
                        Model.TestData.Add(new ExecuteTestInfo()
                        {
                            Id = Convert.ToInt64(dataRow["id"]),
                            OrgNo = dataRow["org_no"].ToString(),
                            TestNo = dataRow["test_no"].ToString(),
                            SampleNo = dataRow["sample_no"].ToString(),
                            TestTypeNo = dataRow["test_type_no"].ToString(),
                            TestItemNo = dataRow["test_item_no"].ToString(),
                            Deadline = dataRow["deadline"].ToString()
                        });
                    }
                }
            }

            var sql2 = @"SELECT * FROM [dbo].[biz_execute_test_detail] WHERE [is_deleted]=0 AND [test_id]=@testId";
            var sql3 = @"SELECT * FROM [dbo].[biz_execute_original_data] WHERE [is_deleted]=0 AND [test_id]=@testId";

            var testId = 0L;

            if (Model.TestData?.Count > 0)
            {
                testId = Model.TestData.FirstOrDefault().Id;
            }
            else
            {
                testId = -1;
            }
            parameters = new SqlParameter[1] { new SqlParameter("@testId", testId) };

            //检测明细表信息
            using (var data = SQLHelper.GetDataTable(sql2, parameters))
            {
                if (Model.TestDetailData?.Count > 0)
                {
                    Model.TestDetailData.Clear();
                }

                if (data != null && data.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in data.Rows)
                    {
                        Model.TestDetailData.Add(new ExecuteTestDetailInfo()
                        {
                            Id = Convert.ToInt64(dataRow["id"]),
                            TestId = Convert.ToInt64(dataRow["test_id"]),
                            ExperimentNo = Convert.ToInt32(dataRow["experiment_no"]),
                            PlayTime = Convert.ToDateTime(dataRow["play_time"]),
                            Area = dataRow["area"].ToString(),
                            UpYieldDot = dataRow["up_yield_dot"].ToString(),
                            DownYieldDot = dataRow["down_yield_dot"].ToString(),
                            MaxDot = dataRow["max_dot"].ToString()
                        });
                    }
                }
            }

            //原始数据信息
            using (var data = SQLHelper.GetDataTable(sql3, parameters))
            {
                if (Model.OriginalData?.Count > 0)
                {
                    Model.OriginalData.Clear();
                }

                if (data != null && data.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in data.Rows)
                    {
                        Model.OriginalData.Add(new ExecuteOriginalDataInfo()
                        {
                            Id = Convert.ToInt64(dataRow["id"]),
                            TestId = Convert.ToInt64(dataRow["test_id"]),
                            ExperimentNo = Convert.ToInt32(dataRow["experiment_no"]),
                            PlayTime = Convert.ToDateTime(dataRow["play_time"]),
                            LoadValue = dataRow["load_value"].ToString(),
                            PositionValue = dataRow["position_value"].ToString(),
                            ExtendValue = dataRow["extend_value"].ToString(),
                            BigDeformValue = dataRow["big_deform_value"].ToString(),
                            DeformSwitch = dataRow["deform_switch"].ToString(),
                            CtrlStep = dataRow["ctrl_step"].ToString(),
                            ExtendDevice1 = dataRow["extend_device1"].ToString(),
                            ExtendDevice2 = dataRow["extend_device2"].ToString(),
                            ExtendDevice3 = dataRow["extend_device3"].ToString(),
                            ExtendDevice4 = dataRow["extend_device4"].ToString(),
                            ExtendDevice5 = dataRow["extend_device5"].ToString(),
                            ExtendDevice6 = dataRow["extend_device6"].ToString(),
                            PosiSpeed = dataRow["posi_speed"].ToString(),
                            StressSpeed = dataRow["stree_speed"].ToString()
                        });
                    }
                }
            }
        }

        #endregion
    }
}
