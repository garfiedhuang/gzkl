using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.Views.CollectMgt.Interface;
using MessageBox = HandyControl.Controls.MessageBox;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace GZKL.Client.UI.ViewsModels
{
    public class InterfaceBaseViewModel : BaseSearchViewModel<InterfaceBaseModel>
    {
        #region Construct and property

        /// <summary>
        /// 构造函数
        /// </summary>
        public InterfaceBaseViewModel()
        {

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
                var sql = new StringBuilder(@"SELECT row_number()over(order by update_dt desc )as row_num,* FROM [dbo].[base_interface] WHERE [is_deleted]=0");

                SqlParameter[] parameters = null;

                if (!string.IsNullOrEmpty(Search.Trim()))
                {
                    sql.Append($" AND ([interface_name] LIKE @search or [access_db_name] LIKE @search)");
                    parameters = new SqlParameter[1] { new SqlParameter("@search", $"%{Search}%") };
                }

                sql.Append($" ORDER BY [update_dt] DESC");

                TModels.Clear();//清空前端分页数据

                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            TModels.Add(new InterfaceBaseModel()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                RowNum = Convert.ToInt64(dataRow["row_num"]),
                                InterfaceName = dataRow["interface_name"].ToString(),
                                AccessDbPath = dataRow["access_db_path"].ToString(),
                                AccessDbName = dataRow["access_db_name"].ToString(),
                                Uid = dataRow["uid"].ToString(),
                                Pwd = dataRow["pwd"].ToString(),
                                Remark = dataRow["remark"].ToString(),
                                IsEnabled = Convert.ToInt32(dataRow["is_enabled"]),
                                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                                UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                            });
                        }
                    }
                }

                //数据分页
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
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        public override void Edit(long id)
        {
            try
            {
                var sql = new StringBuilder(@"SELECT * FROM [dbo].[base_interface] WHERE [is_deleted]=0 AND [id]=@id");

                var parameters = new SqlParameter[1] { new SqlParameter("@id", id) };
                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data == null || data.Rows.Count == 0)
                    {
                        MessageBox.Show($"数据库不存在 主键ID={id} 的记录", "提示信息");
                        return;
                    }

                    var dataRow = data.Rows[0];
                    var model = new InterfaceBaseModel()
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
                        UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                    };

                    if (model != null)
                    {
                        BaseEdit view = new BaseEdit(model);
                        var r = view.ShowDialog();
                        if (r.Value)
                        {
                            sql.Clear();
                            sql.Append(@"UPDATE [dbo].[base_interface]
   SET [interface_name] = @interface_name
      ,[access_db_path] = @access_db_path
      ,[access_db_name] = @access_db_name
      ,[uid] = @uid
      ,[pwd] = @pwd
      ,[remark] = @remark
      ,[is_enabled] = @is_enabled
      ,[update_dt] = @update_dt
      ,[update_user_id] = @user_id
 WHERE [id]=@id");
                            parameters = new SqlParameter[] {
                            new SqlParameter("@interface_name", model.InterfaceName),
                            new SqlParameter("@access_db_path", model.AccessDbPath),
                            new SqlParameter("@access_db_name", model.AccessDbName),
                            new SqlParameter("@uid", model.Uid),
                            new SqlParameter("@pwd", model.Pwd),
                            new SqlParameter("@remark", model.Remark),
                            new SqlParameter("@is_enabled", model.IsEnabled),
                            new SqlParameter("@update_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                            new SqlParameter("@user_id", SessionInfo.Instance.UserInfo.Id),
                            new SqlParameter("@id", id)
                        };

                            var result = SQLHelper.ExecuteNonQuery(sql.ToString(), parameters);

                            this.Query();
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
        /// 删除
        /// </summary>
        /// <param name="selected"></param>
        public void Delete(List<InterfaceBaseModel> selected)
        {
            try
            {
                var r = MessageBox.Show($"确定要删除【{string.Join(",", selected.Select(s => $"{s.InterfaceName}"))}】吗？", "提示", MessageBoxButton.YesNo);
                if (r == MessageBoxResult.Yes)
                {
                    foreach (var dr in selected)
                    {
                        //var sql = new StringBuilder(@"DELETE FROM [dbo].[base_InterfaceBase] WHERE [id] IN(@id)");
                        var sql = new StringBuilder(@"UPDATE [dbo].[base_InterfaceBase] SET [is_deleted]=1 WHERE [id]=@id");

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
                InterfaceBaseModel model = new InterfaceBaseModel();
                BaseEdit view = new BaseEdit(model);
                var r = view.ShowDialog();
                if (r.Value)
                {
                    var sql = @"INSERT INTO [dbo].[base_interface]
           ([interface_name]
           ,[access_db_path]
           ,[access_db_name]
           ,[uid]
           ,[pwd]
           ,[remark]
           ,[is_enabled]
           ,[is_deleted]
           ,[create_dt]
           ,[create_user_id]
           ,[update_dt]
           ,[update_user_id])
     VALUES
           (@interface_name
           ,@access_db_path
           ,@access_db_name
           ,@uid
           ,@pwd
           ,@remark
           ,@is_enabled
           ,0
           ,@create_dt
           ,@user_id
           ,@create_dt
           ,@user_id)";

                    var parameters = new SqlParameter[] {
                            new SqlParameter("@interface_name", model.InterfaceName),
                            new SqlParameter("@access_db_path", model.AccessDbPath),
                            new SqlParameter("@access_db_name", model.AccessDbName),
                            new SqlParameter("@uid", model.Uid),
                            new SqlParameter("@pwd", model.Pwd),
                            new SqlParameter("@remark", model.Remark),
                            new SqlParameter("@is_enabled", model.IsEnabled),
                            new SqlParameter("@create_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                            new SqlParameter("@user_id", SessionInfo.Instance.UserInfo.Id)
                };

                    var result = SQLHelper.ExecuteNonQuery(sql, parameters);

                    this.Query();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        /// <summary>
        /// 设置当前接口
        /// </summary>
        /// <param name="model"></param>
        public void SetCurrentInterface(InterfaceBaseModel model)
        {
            try
            {
                var sql = @"BEGIN
UPDATE [dbo].[base_interface] SET [is_enabled]=0 WHERE 1=1;
UPDATE [dbo].[base_interface] SET [is_enabled]=1,[update_dt] = @update_dt,[update_user_id] = @user_id WHERE [id]=@id;
END";
                var parameters = new SqlParameter[3] {
                    new SqlParameter("@update_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SqlParameter("@user_id", SessionInfo.Instance.UserInfo.Id),
                    new SqlParameter("@id", model.Id) };

                var result = SQLHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    this.Query();
                    MessageBox.Show("本机接口设置成功", "提示信息");
                }
                else
                {
                    MessageBox.Show("本机接口设置失败", "提示信息");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }

        }

        /// <summary>
        /// 数据库接口数据库
        /// </summary>
        /// <param name="model"></param>
        public void SelectInterfaceDatabase(InterfaceBaseModel model)
        {
            try
            {
                var fileName = string.Empty;

                CommonOpenFileDialog dialog = new CommonOpenFileDialog("请选择一个文件");
                dialog.Filters.Add(new CommonFileDialogFilter("Access Files", "*.mdb"));
                dialog.IsFolderPicker = false;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    fileName = dialog.FileName;
                }
                else
                {
                    return;
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

                if (model.Id == 4)
                {
                    //adsBase_Interface.FieldByName('FilePath').AsString:=copy(RzOpenDialog1.FileName,1, Pos('\Tests\',RzOpenDialog1.FileName)+6)
                    model.AccessDbPath = fileName.Substring(0, fileName.IndexOf("\\Tests\\") + 7);
                }
                else
                {
                    model.AccessDbPath = fileName;
                }

                var sql = @"UPDATE [dbo].[base_interface] SET [access_db_path]=@dbPath,[update_dt] = @update_dt,[update_user_id] = @user_id WHERE [id]=@id";
                var parameters = new SqlParameter[4] {
                    new SqlParameter("@dbPath", model.AccessDbPath),
                    new SqlParameter("@update_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SqlParameter("@user_id", SessionInfo.Instance.UserInfo.Id),
                    new SqlParameter("@id", model.Id) };

                var result = SQLHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    this.Query();
                    MessageBox.Show("接口数据库设置成功", "提示信息");
                }
                else
                {
                    MessageBox.Show("接口数据库设置失败", "提示信息");
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
