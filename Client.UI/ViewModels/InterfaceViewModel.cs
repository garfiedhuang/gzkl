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
using GZKL.Client.UI.Views.CollectMgt.Interface;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace GZKL.Client.UI.ViewsModels
{
    public class InterfaceViewModel : ViewModelBase
    {
        #region Construct and property

        /// <summary>
        /// 构造函数
        /// </summary>
        public InterfaceViewModel()
        {
            //SelectInterfaceDbCommand = new RelayCommand(this.SelectInterfaceDb);
            //SetInterfaceDbCommand = new RelayCommand(this.SetInterface);
            //AddTestItemCommand = new RelayCommand(this.AddTestItem);
            //DeleteTestItemCommand = new RelayCommand(this.DeleteTestItem);

            Model = new InterfaceModel()
            {
                InterfaceInfos = new List<InterfaceInfo>(),
                SystemTestItemInfos = new List<SystemTestItemInfo>(),
                InterfaceTestItemInfos = new List<InterfaceTestItemInfo>(),
                InterfaceTestItemRelationInfos = new List<InterfaceTestItemRelationInfo>()
            };

            Query();
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

        #endregion

        #region Command

        ///// <summary>
        ///// 选择接口数据库
        ///// </summary>
        //public RelayCommand SelectInterfaceDbCommand { get; set; }

        ///// <summary>
        ///// 设置当前接口为本机接口
        ///// </summary>
        //public RelayCommand SetInterfaceDbCommand { get; set; }

        ///// <summary>
        ///// 新增测试项目
        ///// </summary>
        //public RelayCommand AddTestItemCommand { get; set; }

        ///// <summary>
        ///// 删除测试项目
        ///// </summary>
        //public RelayCommand DeleteTestItemCommand { get; set; }


        #endregion

        #region Command implement

        /// <summary>
        /// 查询
        /// </summary>
        public void Query()
        {
            try
            {
                var sql1 = @"SELECT * FROM [dbo].[base_interface] WHERE [is_deleted]=0";

                var sql2 = @"SELECT * FROM [dbo].[base_interface_test_item] WHERE [is_deleted]=0";

                var sql3 = @"SELECT * FROM [dbo].[base_test_item] WHERE [is_deleted]=0";

                var sql4 = @"SELECT * FROM [dbo].[base_interface_relation] WHERE [is_deleted]=0";

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
                using (var data = SQLHelper.GetDataTable(sql4))
                {
                    if (model.InterfaceTestItemRelationInfos?.Count > 0)
                    {
                        model.InterfaceTestItemRelationInfos.Clear();
                    }

                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            model.InterfaceTestItemRelationInfos.Add(new InterfaceTestItemRelationInfo()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                InterfaceTestItemNo = dataRow["test_item_no"].ToString(),
                                InterfaceTestItemName = dataRow["test_item_name"].ToString(),
                                SystemTestItemNo = dataRow["test_item_no"].ToString(),
                                SystemTestItemName = dataRow["test_item_name"].ToString()
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
        /// 选择接口数据库
        /// </summary>
        /// <param name="model"></param>
        public void SelectInterfaceDb(InterfaceInfo model)
        {
            var fileName = string.Empty;

            try
            {
                if (model==null)
                {
                    MessageBox.Show("请选择接口记录", "提示信息");
                    return;
                }

                CommonOpenFileDialog dialog = new CommonOpenFileDialog("请选择一个文件");
                dialog.Filters.Add(new CommonFileDialogFilter("Access Files","*.mdb"));
                dialog.IsFolderPicker = false;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    fileName = dialog.FileName;
                }

                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show("请选择数据库文件", "提示信息");
                    return;
                }

                if (model.Id != 4 && !fileName.Contains(model.AccessDbName))
                {
                    MessageBox.Show("选择的数据库文件不正确，请重新选择", "提示信息");
                    return;
                }
                else if (model.Id == 4 && !fileName.Contains("Tests"))
                {
                    MessageBox.Show("选择的数据库文件不正确，请重新选择", "提示信息");
                    return;
                }

                var sql = @"BEGIN
UPDATE [dbo].[base_interface] SET [is_enabled]=0 WHERE 1=1;
UPDATE [dbo].[base_interface] SET [is_enabled]=1 WHERE [id]=@id;
END";
                var parameters = new SqlParameter[1] { new SqlParameter("@id", model.Id) };

                var result = SQLHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    MessageBox.Show("设置成功", "提示信息");
                }
                else
                {
                    MessageBox.Show("设置失败", "提示信息");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        /// <summary>
        /// 设置当前接口为本机接口
        /// </summary>
        /// <param name="model"></param>
        public void SetInterface(InterfaceInfo model)
        {
            try
            {
                var sql = @"BEGIN
UPDATE [dbo].[base_interface] SET [is_enabled]=0 WHERE 1=1;
UPDATE [dbo].[base_interface] SET [is_enabled]=1 WHERE [id]=@id;
END";
                var parameters = new SqlParameter[1] { new SqlParameter("@id", model.Id) };

                var result = SQLHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    MessageBox.Show("设置成功", "提示信息");
                }
                else
                {
                    MessageBox.Show("设置失败", "提示信息");
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
        public void AddTestItem(InterfaceTestItemRelationInfo model)
        {
            try
            {
                var sql = @"SELECT COUNT(1) FROM [dbo].[base_interface_relation] WHERE test_item_id=@testItemId AND test_item_no=@testItemNo";
                var parameters = new SqlParameter[] { 
                    new SqlParameter("@testItemId", model.InterfaceId),
                    new SqlParameter("@testItemNo", model.SystemTestItemNo)};

                var result =Convert.ToInt32(SQLHelper.ExecuteScalar(sql, parameters));

                if (result > 0)
                {
                    MessageBox.Show($"记录{model.InterfaceId}|{model.SystemTestItemNo}已存在，请勿重复添加", "提示信息");
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
                    new SqlParameter("@interfaceId", model.InterfaceId),
                    new SqlParameter("@testItemId", model.InterfaceId),
                    new SqlParameter("@testItemNo", model.SystemTestItemNo),
                    new SqlParameter("@testItemName", model.SystemTestItemName),
                    new SqlParameter("@is_enabled", 1),
                    new SqlParameter("@create_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SqlParameter("@user_id", SessionInfo.Instance.Session.Id)
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
