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
            SelectAutoCollectDbCommand = new RelayCommand(this.SelectAutoCollectDb);
            SetAutoCollectDbCommand = new RelayCommand(this.SetAutoCollectDb);
            AddTestItemCommand = new RelayCommand(this.AddTestItem);
            DeleteTestItemCommand = new RelayCommand(this.DeleteTestItem);

            AutoCollectModels = new List<AutoCollectModel>();
            GridModelList = new ObservableCollection<AutoCollectModel>();

        }

        private void DeleteTestItem()
        {
            throw new NotImplementedException();
        }

        private void AddTestItem()
        {
            throw new NotImplementedException();
        }

        private void SetAutoCollectDb()
        {
            throw new NotImplementedException();
        }

        private void SelectAutoCollectDb()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询之后的结果数据，用于分页显示
        /// </summary>
        private static List<AutoCollectModel> AutoCollectModels { get; set; }

        /// <summary>
        /// 网格数据集合
        /// </summary>
        private ObservableCollection<AutoCollectModel> gridModelList;
        public ObservableCollection<AutoCollectModel> GridModelList
        {
            get { return gridModelList; }
            set { gridModelList = value; RaisePropertyChanged(); }
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

        /// <summary>
        /// 选择接口数据库
        /// </summary>
        public RelayCommand SelectAutoCollectDbCommand { get; set; }

        /// <summary>
        /// 设置当前接口为本机数据库
        /// </summary>
        public RelayCommand SetAutoCollectDbCommand { get; set; }

        /// <summary>
        /// 新增测试项目
        /// </summary>
        public RelayCommand AddTestItemCommand { get; set; }

        /// <summary>
        /// 删除测试项目
        /// </summary>
        public RelayCommand DeleteTestItemCommand { get; set; }


        #endregion

        #region Command implement

        /// <summary>
        /// 查询
        /// </summary>
        public void Query()
        {
            try
            {
                /*
                var sql = new StringBuilder(@"SELECT row_number()over(order by update_dt desc )as row_num
                ,[id],[AutoCollect_name],[access_db_path],[access_db_name],[remark],[is_enabled],[is_deleted],[create_dt]
                ,[create_user_id],[update_dt],[update_user_id]
                FROM [dbo].[base_AutoCollect] WHERE [is_deleted]=0");

                SqlParameter[] parameters = null;

                if (!string.IsNullOrEmpty(Search.Trim()))
                {
                    sql.Append($" AND ([AutoCollect_name] LIKE @search or [access_db_name] LIKE @search)");
                    parameters = new SqlParameter[1] { new SqlParameter("@search", $"%{Search}%") };
                }

                sql.Append($" ORDER BY [update_dt] DESC");

                AutoCollectModels.Clear();//清空前端分页数据

                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            AutoCollectModels.Add(new AutoCollectModel()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                RowNum = Convert.ToInt64(dataRow["row_num"]),
                                AutoCollectName = dataRow["AutoCollect_name"].ToString(),
                                AccessDbPath = dataRow["access_db_path"].ToString(),
                                AccessDbName = dataRow["access_db_name"].ToString(),
                                Remark = dataRow["remark"].ToString(),
                                IsEnabled = Convert.ToInt32(dataRow["is_enabled"]),
                                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                                UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                            });
                        }
                    }
                }

                //当前页数
                PageIndex = AutoCollectModels.Count > 0 ? 1 : 0;
                MaxPageCount = 0;

                //最大页数
                MaxPageCount = PageIndex > 0 ? (int)Math.Ceiling((decimal)AutoCollectModels.Count / DataCountPerPage) : 0;

                //数据分页
                Paging(PageIndex);
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            this.Search = string.Empty;
            this.Query();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        public void Edit(int id)
        {
            try
            {
                /*
                var selected = GridModelList.Where(w => w.IsSelected == true).ToList();

                if (selected.Count != 1)
                {
                    MessageBox.Show($"请选择一条记录进行编辑", "提示信息");
                    return;
                }

                id = (int)selected.First().Id;

                var sql = new StringBuilder(@"SELECT [id],[AutoCollect_name],[access_db_path],[access_db_name],[remark]
                ,[is_enabled],[is_deleted],[create_dt],[create_user_id],[update_dt],[update_user_id]
                FROM [dbo].[base_AutoCollect] WHERE [is_deleted]=0 AND [id]=@id");

                var parameters = new SqlParameter[1] { new SqlParameter("@id", id) };
                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data == null || data.Rows.Count == 0)
                    {
                        MessageBox.Show($"数据库不存在 主键ID={id} 的记录", "提示信息");
                        return;
                    }

                    var dataRow = data.Rows[0];
                    var model = new AutoCollectModel()
                    {
                        Id = Convert.ToInt64(dataRow["id"]),
                        AutoCollectName = dataRow["AutoCollect_name"].ToString(),
                        AccessDbPath = dataRow["access_db_path"].ToString(),
                        AccessDbName = dataRow["access_db_name"].ToString(),
                        Remark = dataRow["remark"].ToString(),
                        IsEnabled = Convert.ToInt32(dataRow["is_enabled"]),
                        CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                        UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                    };

                    if (model != null)
                    {
                        Edit view = new Edit(model);
                        var r = view.ShowDialog();
                        if (r.Value)
                        {
                            sql.Clear();
                            sql.Append(@"UPDATE [dbo].[base_AutoCollect]
   SET [AutoCollect_name] = @AutoCollectName
      ,[access_db_path] = @access_db_path
      ,[access_db_name] = @access_db_name
      ,[remark] = @remark
      ,[is_enabled] = @is_enabled
      ,[update_dt] = @update_dt
      ,[update_user_id] = @user_id
 WHERE [id]=@id");
                            parameters = new SqlParameter[] {
                            new SqlParameter("@AutoCollect_name", model.AutoCollectName),
                            new SqlParameter("@access_db_path", model.AccessDbPath),
                            new SqlParameter("@access_db_name", model.AccessDbName),
                            new SqlParameter("@remark", model.Remark),
                            new SqlParameter("@is_enabled", model.IsEnabled),
                            new SqlParameter("@update_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                            new SqlParameter("@user_id", SessionInfo.Instance.Session.Id),
                            new SqlParameter("@id", id)
                        };

                            var result = SQLHelper.ExecuteNonQuery(sql.ToString(), parameters);

                            this.Query();
                        }
                    }
                }
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            try
            {
                /*
                var selected = GridModelList.Where(w => w.IsSelected == true).ToList();

                if (selected.Count == 0)
                {
                    MessageBox.Show($"请至少选择一条记录进行删除", "提示信息");
                    return;
                }

                if (selected != null)
                {
                    var r = MessageBox.Show($"确定要删除【{string.Join(",", selected.Select(s => $"{s.AutoCollectName}|{s.AccessDbName}"))}】吗？", "提示", MessageBoxButton.YesNo);
                    if (r == MessageBoxResult.Yes)
                    {
                        foreach (var dr in selected)
                        {
                            //var sql = new StringBuilder(@"DELETE FROM [dbo].[base_AutoCollect] WHERE [id] IN(@id)");
                            var sql = new StringBuilder(@"UPDATE [dbo].[base_AutoCollect] SET [is_deleted]=1 WHERE [id]=@id");

                            var parameters = new SqlParameter[1] { new SqlParameter("@id", dr.Id) };
                            var result = SQLHelper.ExecuteNonQuery(sql.ToString(), parameters);
                        }

                        this.Query();
                    }
                }
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        public void Add()
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

        /// <summary>
        /// 页面更新事件
        /// </summary>
        public void PageUpdated(FunctionEventArgs<int> e)
        {
            Paging(e.Info);
        }

        /// <summary>
        /// 选择
        /// </summary>
        public void Select(AutoCollectModel model)
        {
            try
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog("请选择一个数据库文件");
                dialog.IsFolderPicker = false; //选择文件还是文件夹（true:选择文件夹，false:选择文件）
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    model.AccessDbPath = dialog.FileName;
                    model.AccessDbName = dialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        #endregion

        #region Privates

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageIndex"></param>
        private void Paging(int pageIndex)
        {

            //GridModelList.Clear();//清空依赖属性

            //var pagedData = AutoCollectModels.Skip((pageIndex - 1) * DataCountPerPage).Take(DataCountPerPage).ToList();

            //if (pagedData.Count > 0)
            //{
            //    pagedData.ForEach(item =>
            //    {
            //        GridModelList.Add(item);
            //    });
            //}
        }

        #endregion
    }
}
