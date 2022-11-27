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
using GZKL.Client.UI.Views.SystemMgt.Menu;
using GalaSoft.MvvmLight.Messaging;

namespace GZKL.Client.UI.ViewsModels
{

    public class MenuViewModel : ViewModelBase
    {
        #region Construct and property

        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuViewModel()
        {
            QueryCommand = new RelayCommand(this.Query);
            EditCommand = new RelayCommand<int>(this.Edit);
            DeleteCommand = new RelayCommand<int>(this.Delete);
            AddCommand = new RelayCommand(this.Add);

            this.GridModelList = new ObservableCollection<MenuModel>();

            this.TreeViewList = GetDataList();
        }

        /// <summary>
        /// 菜单树集合
        /// </summary>
        private ObservableCollection<MenuDataModel> treeViewList;
        public ObservableCollection<MenuDataModel> TreeViewList
        {
            get => treeViewList;
            set => Set(ref treeViewList, value);
        }

        /// <summary>
        /// 网格数据集合
        /// </summary>
        private ObservableCollection<MenuModel> gridModelList;
        public ObservableCollection<MenuModel> GridModelList
        {
            get { return gridModelList; }
            set { gridModelList = value; RaisePropertyChanged(); }
        }

        #endregion

        #region Command

        /// <summary>
        /// 查询命令
        /// </summary>
        public RelayCommand QueryCommand { get; set; }

        /// <summary>
        /// 编辑
        /// </summary>
        public RelayCommand<int> EditCommand { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public RelayCommand<int> DeleteCommand { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        public RelayCommand AddCommand { get; set; }


        #endregion

        #region Command implement

        /// <summary>
        /// 查询
        /// </summary>
        public void Query()
        {
            try
            {
                //var sql = new StringBuilder(@"SELECT row_number()over(order by update_dt desc )as row_num
                //,[id],[category],[value],[text],[remark],[is_enabled],[is_deleted],[create_dt]
                //,[create_user_id],[update_dt],[update_user_id]
                //FROM [dbo].[sys_Menu] WHERE [is_deleted]=0");

                //SqlParameter[] parameters = null;

                //if (!string.IsNullOrEmpty(Search.Trim()))
                //{
                //    sql.Append($" AND ([category] LIKE @search or [value] LIKE @search or [text] LIKE @search)");
                //    parameters = new SqlParameter[1] { new SqlParameter("@search", $"%{Search}%") };
                //}

                ////sql.Append($" ORDER BY [category] DESC");

                //MenuModels.Clear();//清空前端分页数据

                //using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                //{
                //    if (data != null && data.Rows.Count > 0)
                //    {
                //        foreach (DataRow dataRow in data.Rows)
                //        {
                //            MenuModels.Add(new MenuModel()
                //            {
                //                Id = Convert.ToInt64(dataRow["id"]),
                //                //RowNum = Convert.ToInt64(dataRow["row_num"]),
                //                //Category = dataRow["category"].ToString(),
                //                //Value = dataRow["value"].ToString(),
                //                //Text = dataRow["text"].ToString(),
                //                //Remark = dataRow["remark"].ToString(),
                //                IsEnabled = Convert.ToInt32(dataRow["is_enabled"]),
                //                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                //                UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                //            });
                //        }
                //    }
                //}

                ////当前页数
                //PageIndex = MenuModels.Count > 0 ? 1 : 0;
                //MaxPageCount = 0;

                ////最大页数
                //MaxPageCount = PageIndex > 0 ? (int)Math.Ceiling((decimal)MenuModels.Count / DataCountPerPage) : 0;

                ////数据分页
                //Paging(PageIndex);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        public void Edit(int id)
        {
            try
            {
                var selected = GridModelList.Where(w => w.IsSelected == true).ToList();

                if (selected.Count != 1)
                {
                    MessageBox.Show($"请选择一条记录进行编辑", "提示信息");
                    return;
                }

                id = (int)selected.First().Id;

                var sql = new StringBuilder(@"SELECT [id],[category],[value],[text],[remark],[is_enabled],[is_deleted],[create_dt]
                ,[create_user_id],[update_dt],[update_user_id]
                FROM [dbo].[sys_Menu] WHERE [is_deleted]=0 AND [id]=@id");

                var parameters = new SqlParameter[1] { new SqlParameter("@id", id) };
                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data == null || data.Rows.Count == 0)
                    {
                        MessageBox.Show($"数据库不存在 主键ID={id} 的记录", "提示信息");
                        return;
                    }

                    var dataRow = data.Rows[0];
                    var model = new MenuModel()
                    {
                        Id = Convert.ToInt64(dataRow["id"]),
                        //Category = dataRow["category"].ToString(),
                        //Value = dataRow["value"].ToString(),
                        //Text = dataRow["text"].ToString(),
                        //Remark = dataRow["remark"].ToString(),
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
                            sql.Append(@"UPDATE [dbo].[sys_Menu]
   SET [category] = @category
      ,[value] = @value
      ,[text] = @text
      ,[remark] = @remark
      ,[is_enabled] = @is_enabled
      ,[update_dt] = @update_dt
      ,[update_user_id] = @user_id
 WHERE [id]=@id");
                            parameters = new SqlParameter[] {
                            //new SqlParameter("@category", model.Category),
                            //new SqlParameter("@value", model.Value),
                            //new SqlParameter("@text", model.Text),
                            //new SqlParameter("@remark", model.Remark),
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
                var selected = GridModelList.Where(w => w.IsSelected == true).ToList();

                if (selected.Count == 0)
                {
                    MessageBox.Show($"请至少选择一条记录进行删除", "提示信息");
                    return;
                }

                if (selected != null)
                {
                    //var r = MessageBox.Show($"确定要删除【{string.Join(",", selected.Select(s => $"{s.Category}|{s.Value}|{s.Text}"))}】吗？", "提示", MessageBoxButton.YesNo);
                    //if (r == MessageBoxResult.Yes)
                    //{
                    //    foreach (var dr in selected)
                    //    {
                    //        //var sql = new StringBuilder(@"DELETE FROM [dbo].[sys_Menu] WHERE [id] IN(@id)");
                    //        var sql = new StringBuilder(@"UPDATE [dbo].[sys_Menu] SET [is_deleted]=1 WHERE [id]=@id");

                    //        var parameters = new SqlParameter[1] { new SqlParameter("@id", dr.Id) };
                    //        var result = SQLHelper.ExecuteNonQuery(sql.ToString(), parameters);
                    //    }

                    //    this.Query();
                    //}
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
        public void Add()
        {
            try
            {
                MenuModel model = new MenuModel();
                Edit view = new Edit(model);
                var r = view.ShowDialog();
                if (r.Value)
                {
                    var sql = @"INSERT INTO [dbo].[sys_Menu]
           ([category]
           ,[value]
           ,[text]
           ,[remark]
           ,[is_enabled]
           ,[is_deleted]
           ,[create_dt]
           ,[create_user_id]
           ,[update_dt]
           ,[update_user_id])
     VALUES
           (@category
           ,@value
           ,@text
           ,@remark
           ,@is_enabled
           ,0
           ,@create_dt
           ,@user_id
           ,@create_dt
           ,@user_id)";

                    var parameters = new SqlParameter[] {
                    //new SqlParameter("@category", model.Category),
                    //new SqlParameter("@value", model.Value),
                    //new SqlParameter("@text", model.Text),
                    //new SqlParameter("@remark", model.Remark),
                    new SqlParameter("@is_enabled", model.IsEnabled),
                    new SqlParameter("@create_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SqlParameter("@user_id", SessionInfo.Instance.Session.Id)
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

        #endregion

        #region Privates

        private ObservableCollection<MenuDataModel> GetDataList()
        {
            return new ObservableCollection<MenuDataModel>
            {
                new MenuDataModel{ Name = "Name1", DataList = new ObservableCollection<MenuDataModel>{ new MenuDataModel { Name = "Name1-1", DataList = null},
                                                                                                       new MenuDataModel { Name = "Name1-2", DataList = null},} },
                new MenuDataModel{ Name = "Name2",IsSelected=true,  DataList = new ObservableCollection<MenuDataModel>{ new MenuDataModel { Name = "Name2-1", DataList = null},
                                                                                                       new MenuDataModel { Name = "Name2-2", DataList = null},} },
                new MenuDataModel{ Name = "Name3", DataList = null},
            };
        }

        #endregion
    }
}
