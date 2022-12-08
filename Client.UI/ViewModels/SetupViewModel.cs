using GalaSoft.MvvmLight;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.Common;
using MessageBox = HandyControl.Controls.MessageBox;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using ADODB;
using GZKL.Client.UI.Views.CollectMgt.Interface;

namespace GZKL.Client.UI.ViewsModels
{
    public class SetupViewModel : BaseSearchViewModel<InterfaceTestItemRelationInfo>
    {
        #region Construct and property

        /// <summary>
        /// 构造函数
        /// </summary>
        public SetupViewModel()
        {
            Model = new InterfaceModel()
            {
                InterfaceInfos = new ObservableCollection<InterfaceInfo>(),
                SystemTestItemInfos = new ObservableCollection<SystemTestItemInfo>(),
                InterfaceTestItemInfos = new ObservableCollection<InterfaceTestItemInfo>(),
                InterfaceTestItemRelationInfos = new ObservableCollection<InterfaceTestItemRelationInfo>()
            };
        }

        /// <summary>
        /// 网格数据集合
        /// </summary>
        private InterfaceModel model;
        public InterfaceModel Model
        {
            get { return model; }
            set { model = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        private string search = string.Empty;

        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Command


        #endregion

        #region Command implement

        /// <summary>
        /// 查询
        /// </summary>
        public override void Query()
        {
            try
            {
                var sql1 = @"SELECT * FROM [dbo].[base_interface] WHERE [is_deleted]=0";

                var sql2 = @"SELECT * FROM [dbo].[base_interface_test_item] WHERE [is_deleted]=0";

                var sql3 = @"SELECT * FROM [dbo].[base_test_item] WHERE [is_deleted]=0";

                var sql4 = @"SELECT row_number()over(order by bir.update_dt desc )as row_num,bir.id,bir.interface_id,bi.interface_name,bir.test_item_id AS interface_test_item_id,
biti.test_item_name AS interface_test_item_name,bir.test_item_no AS system_test_item_no,bir.test_item_name AS system_test_item_name,bir.create_dt,bir.update_dt
FROM [dbo].[base_interface_relation] bir INNER JOIN [dbo].[base_interface] bi ON bir.interface_id=bi.id
INNER JOIN [dbo].[base_interface_test_item] biti ON bir.test_item_id=biti.id
INNER JOIN [dbo].[base_test_item] bti ON bir.test_item_no=bti.test_item_no WHERE bir.[is_deleted]=0
AND bi.[is_deleted]=0 AND biti.[is_deleted]=0 AND bti.[is_deleted]=0";

                //接口信息
                using (var data = SQLHelper.GetDataTable(sql1))
                {
                    if (model.InterfaceInfos?.Count > 0)
                    {
                        model.InterfaceInfos.Clear();
                    }

                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            model.InterfaceInfos.Add(new InterfaceInfo()
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
                    if (model.InterfaceTestItemInfos?.Count > 0)
                    {
                        model.InterfaceTestItemInfos.Clear();
                    }

                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            model.InterfaceTestItemInfos.Add(new InterfaceTestItemInfo()
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
                    if (model.SystemTestItemInfos?.Count > 0)
                    {
                        model.SystemTestItemInfos.Clear();
                    }

                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            model.SystemTestItemInfos.Add(new SystemTestItemInfo()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                TestItemNo = dataRow["test_item_no"].ToString(),
                                TestItemName = dataRow["test_item_name"].ToString()
                            });
                        }
                    }
                }

                //接口与检测项关系
                SqlParameter[] sqlParameters = null;

                if (!string.IsNullOrEmpty(Search))
                {
                    sql4 +=$" AND (bi.interface_name LIKE @search OR biti.test_item_name LIKE @search OR bir.test_item_no LIKE @search OR bir.test_item_name LIKE @search)";
                    sqlParameters = new SqlParameter[1] { new SqlParameter("@search", $"%{Search}%") };
                }

                using (var data = SQLHelper.GetDataTable(sql4, sqlParameters))
                {
                  
                    TModels.Clear();

                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            TModels.Add(new InterfaceTestItemRelationInfo()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                RowNum = Convert.ToInt64(dataRow["row_num"]),
                                InterfaceId =Convert.ToInt64(dataRow["interface_id"]),
                                InterfaceName= dataRow["interface_name"].ToString(),
                                InterfaceTestItemId = Convert.ToInt64(dataRow["interface_test_item_id"]),
                                InterfaceTestItemName = dataRow["interface_test_item_name"].ToString(),
                                SystemTestItemNo = dataRow["system_test_item_no"].ToString(),
                                SystemTestItemName = dataRow["system_test_item_name"].ToString(),
                                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                                UpdateDt = Convert.ToDateTime(dataRow["update_dt"])
                            });
                        }
                    }
                }

                Paging(-1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        public override void Reset()
        {
            this.Search = string.Empty;
            this.Query();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="selected"></param>
        public void Delete(List<InterfaceTestItemRelationInfo> selected)
        {
            try
            {
                var r = MessageBox.Show($"确定要删除【{string.Join(",", selected.Select(s => $"{s.InterfaceName}"))}】吗？", "提示", MessageBoxButton.YesNo);
                if (r == MessageBoxResult.Yes)
                {
                    foreach (var dr in selected)
                    {
                        //var sql = new StringBuilder(@"DELETE FROM [dbo].[base_InterfaceBase] WHERE [id] IN(@id)");
                        var sql = @"UPDATE [dbo].[base_interface_relation] SET [is_deleted]=1 WHERE [id]=@id";

                        var parameters = new SqlParameter[1] { new SqlParameter("@id", dr.Id) };
                        var result = SQLHelper.ExecuteNonQuery(sql.ToString(), parameters);
                    }

                    this.Query();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        public override void Add()
        {
            try
            {
                InterfaceTestItemRelationInfo model = new InterfaceTestItemRelationInfo();
                SetupEdit view = new SetupEdit(model, Model.InterfaceInfos.ToList(), Model.InterfaceTestItemInfos.ToList(), Model.SystemTestItemInfos.ToList());
                var r = view.ShowDialog();
                if (r.Value)
                {

                    this.Query();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }


        /// <summary>
        /// 新增测试项目
        /// </summary>
        /// <param name="model"></param>
        public void AddTestItem(InterfaceInfo interfaceInfo, InterfaceTestItemInfo interfaceTestItemInfo, SystemTestItemInfo systemTestItemInfo)
        {
            try
            {
                var sql = @"SELECT COUNT(1) FROM [dbo].[base_interface_relation] WHERE test_item_id=@interfaceTestItemId AND test_item_no=@systemTestItemNo";
                var parameters = new SqlParameter[] { 
                    new SqlParameter("@interfaceTestItemId", interfaceTestItemInfo.Id),
                    new SqlParameter("@systemTestItemNo", systemTestItemInfo.TestItemNo)};

                var result =Convert.ToInt32(SQLHelper.ExecuteScalar(sql, parameters));

                if (result > 0)
                {
                    MessageBox.Show($"记录{interfaceTestItemInfo.InterfaceId}|{systemTestItemInfo.TestItemNo}已存在，请勿重复添加", "提示信息");
                    return;
                }

                sql = @"INSERT INTO [dbo].[base_interface_relation]
                                   ([interface_id]
                                   ,[test_item_id]
                                   ,[test_item_no]
                                   ,[test_item_name]
                                   ,[is_enabled]
                                   ,[is_deleted]
                                   ,[create_dt]
                                   ,[create_user_id]
                                   ,[update_dt]
                                   ,[update_user_id])
                             VALUES
                                   (@interfaceId
                                   ,@testItemId
                                   ,@testItemNo
                                   ,@testItemName
                                   ,@is_enabled
                                   ,0
                                   ,@create_dt
                                   ,@user_id
                                   ,@create_dt
                                   ,@user_id)";

                parameters = new SqlParameter[] {
                    new SqlParameter("@interfaceId", interfaceInfo.Id),
                    new SqlParameter("@testItemId", interfaceTestItemInfo.Id),
                    new SqlParameter("@testItemNo", systemTestItemInfo.TestItemNo),
                    new SqlParameter("@testItemName", systemTestItemInfo.TestItemName),
                    new SqlParameter("@is_enabled", 1),
                    new SqlParameter("@create_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SqlParameter("@user_id", SessionInfo.Instance.UserInfo.Id)
                };

                result = SQLHelper.ExecuteNonQuery(sql, parameters);

                this.Query();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        /// <summary>
        /// 删除测试项目
        /// </summary>
        /// <param name="model"></param>
        public void DeleteTestItem(InterfaceTestItemRelationInfo model)
        {
            try
            {
                var sql = @"DELETE FROM base_interface_relation WHERE id=@id";
                var parameters = new SqlParameter[1] { new SqlParameter("@id", model.Id) };

                var result = SQLHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    this.Query();
                    MessageBox.Show("删除成功", "提示信息");
                }
                else
                {
                    MessageBox.Show("删除失败", "提示信息");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        #endregion


        #region Privates


        #endregion
    }
}
