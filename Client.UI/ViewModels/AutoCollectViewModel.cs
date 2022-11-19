using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GZKL.Client.UI.Models;
using System.Windows;
using HandyControl.Data;
using System.Data.SqlClient;
using GZKL.Client.UI.Common;
using System.Data;
using System.Windows.Controls;
using MessageBox = HandyControl.Controls.MessageBox;
using GZKL.Client.UI.Views.CollectMgt.AutoCollect;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.WindowsAPICodePack.Dialogs;

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
            OrgData = new List<OrgInfo>();
            InterfaceData = new List<InterfaceInfo>();
            TestTypeData = new List<TestTypeInfo>();
            SystemTestItemData = new List<SystemTestItemInfo>();
            InterfaceTestItemData = new List<InterfaceTestItemInfo>();

            Model = new AutoCollectModel();

            this.InitData();

            this.SetInterface();
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
                /*
                AutoCollectModel model = new AutoCollectModel();
                Edit view = new Edit(model);
                var r = view.ShowDialog();
                if (r.Value)
                {
                    var sql = @"INSERT INTO [dbo].[base_AutoCollect]
           ([AutoCollect_name]
           ,[access_db_path]
           ,[access_db_name]
           ,[remark]
           ,[is_enabled]
           ,[is_deleted]
           ,[create_dt]
           ,[create_user_id]
           ,[update_dt]
           ,[update_user_id])
     VALUES
           (@AutoCollect_name
           ,@access_db_path
           ,@access_db_name
           ,@remark
           ,@is_enabled
           ,0
           ,@create_dt
           ,@user_id
           ,@create_dt
           ,@user_id)";

                    var parameters = new SqlParameter[] {
                    new SqlParameter("@AutoCollect_name", model.AutoCollectName),
                    new SqlParameter("@access_db_path", model.AccessDbPath),
                    new SqlParameter("@access_db_name", model.AccessDbName),
                    new SqlParameter("@remark", model.Remark),
                    new SqlParameter("@is_enabled", model.IsEnabled),
                    new SqlParameter("@create_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SqlParameter("@user_id", SessionInfo.Instance.Session.Id)
                };

                    var result = SQLHelper.ExecuteNonQuery(sql, parameters);

                    this.Query();
                }

                */
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
        private void InitData()
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
                                TableDot = dataRow["table_dot"].ToString()
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
                                TestItemName = dataRow["test_item_name"].ToString()
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
                                TestTypeName = dataRow["test_type_name"].ToString()
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
        private void SetInterface()
        {
            if (InterfaceData?.Count > 0)
            {
                Model.InterfaceName = InterfaceData.FirstOrDefault(w => w.IsEnabled == 1)?.InterfaceName;
            }
        }

        /// <summary>
        /// 参数校验
        /// </summary>
        /// <returns></returns>
        private int CheckValue()
        {

            return 0;
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="orgNo">机构代码</param>
        /// <param name="testItemNo">检测项编号</param>
        /// <param name="testNo">检测编号</param>
        private void QueryData(string orgNo,string testItemNo,string testNo)
        {
            var sql = @"SELECT * FROM [dbo].[biz_execute_test] WHERE [is_deleted]=0 AND [org_no]=@orgNo AND [test_item_no]=@testItemNo AND [test_no]=@testNo";
            var parameters =new SqlParameter[3] {
                    new SqlParameter("@orgNo", orgNo),
                    new SqlParameter("@testItemNo", testItemNo),
                    new SqlParameter("@testNo", testNo)
            };

            
        
        }

        #endregion
    }
}
