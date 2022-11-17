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
using GZKL.Client.UI.Views.CollectMgt.Clear;
using GalaSoft.MvvmLight.Messaging;

namespace GZKL.Client.UI.ViewsModels
{
    public class ClearViewModel : ViewModelBase
    {
        #region Construct and property

        /// <summary>
        /// 构造函数
        /// </summary>
        public ClearViewModel()
        {
            QueryCommand = new RelayCommand(this.Query);
            ResetCommand = new RelayCommand(this.Reset);
            EditCommand = new RelayCommand<int>(this.Edit);
            DeleteCommand = new RelayCommand<int>(this.Delete);
            AddCommand = new RelayCommand(this.Add);
            PageUpdatedCommand = new RelayCommand<FunctionEventArgs<int>>(PageUpdated);

            ClearModels = new List<ClearModel>();
            GridModelList = new ObservableCollection<ClearModel>();
        }

        /// <summary>
        /// 查询之后的结果数据，用于分页显示
        /// </summary>
        private static List<ClearModel> ClearModels { get; set; }

        /// <summary>
        /// 网格数据集合
        /// </summary>
        private ObservableCollection<ClearModel> gridModelList;
        public ObservableCollection<ClearModel> GridModelList
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

        /// <summary>
        /// 最大页面数
        /// </summary>
        private int maxPageCount = 1;

        public int MaxPageCount
        {
            get { return maxPageCount; }
            set
            {
                maxPageCount = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 1;

        public int PageIndex
        {
            get { return pageIndex; }
            set
            {
                pageIndex = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 分页大小
        /// </summary>
        private int dataCountPerPage = 20;

        public int DataCountPerPage
        {
            get { return dataCountPerPage; }
            set
            {
                dataCountPerPage = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// 查询命令
        /// </summary>
        public RelayCommand QueryCommand { get; set; }

        /// <summary>
        /// 重置命令
        /// </summary>
        public RelayCommand ResetCommand { get; set; }

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

        /// <summary>
        /// 分页
        /// </summary>
        public RelayCommand<FunctionEventArgs<int>> PageUpdatedCommand { get; set; }


        #endregion

        #region Command implement

        /// <summary>
        /// 查询
        /// </summary>
        public void Query()
        {
            try
            {
                var sql = new StringBuilder(@"SELECT row_number()over(order by update_dt desc )as row_num
                ,[id],[Clear_no],[Clear_name],[Clear_level],[remark],[is_enabled],[is_deleted],[create_dt]
                ,[create_user_id],[update_dt],[update_user_id]
                FROM [dbo].[base_Clear] WHERE [is_deleted]=0");

                SqlParameter[] parameters = null;

                if (!string.IsNullOrEmpty(Search.Trim()))
                {
                    sql.Append($" AND ([Clear_no] LIKE @search or [Clear_name] LIKE @search)");
                    parameters = new SqlParameter[1] { new SqlParameter("@search", $"%{Search}%") };
                }

                sql.Append($" ORDER BY [update_dt] DESC");

                ClearModels.Clear();//清空前端分页数据

                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            //ClearModels.Add(new ClearModel()
                            //{
                            //    Id = Convert.ToInt64(dataRow["id"]),
                            //    RowNum = Convert.ToInt64(dataRow["row_num"]),
                            //    ClearNo = dataRow["Clear_no"].ToString(),
                            //    ClearName = dataRow["Clear_name"].ToString(),
                            //    ClearLevel = dataRow["Clear_level"].ToString(),
                            //    Remark = dataRow["remark"].ToString(),
                            //    IsEnabled = Convert.ToInt32(dataRow["is_enabled"]),
                            //    CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                            //    UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                            //});
                        }
                    }
                }

                //当前页数
                PageIndex = ClearModels.Count > 0 ? 1 : 0;
                MaxPageCount = 0;

                //最大页数
                MaxPageCount = PageIndex > 0 ? (int)Math.Ceiling((decimal)ClearModels.Count / DataCountPerPage) : 0;

                //数据分页
                Paging(PageIndex);

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
                var selected = GridModelList.Where(w => w.IsSelected == true).ToList();

                if (selected.Count != 1)
                {
                    MessageBox.Show($"请选择一条记录进行编辑", "提示信息");
                    return;
                }

                id = (int)selected.First().Id;

                var sql = new StringBuilder(@"SELECT [id],[Clear_no],[Clear_name],[Clear_level],[remark]
                ,[is_enabled],[is_deleted],[create_dt],[create_user_id],[update_dt],[update_user_id]
                FROM [dbo].[base_Clear] WHERE [is_deleted]=0 AND [id]=@id");

                var parameters = new SqlParameter[1] { new SqlParameter("@id", id) };
                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data == null || data.Rows.Count == 0)
                    {
                        MessageBox.Show($"数据库不存在 主键ID={id} 的记录", "提示信息");
                        return;
                    }

                    var dataRow = data.Rows[0];
                    var model = new ClearModel()
                    {
                        Id = Convert.ToInt64(dataRow["id"]),
                        //ClearNo = dataRow["Clear_no"].ToString(),
                        //ClearName = dataRow["Clear_name"].ToString(),
                        //ClearLevel = dataRow["Clear_level"].ToString(),
                        //Remark = dataRow["remark"].ToString(),
                        //IsEnabled = Convert.ToInt32(dataRow["is_enabled"]),
                        //CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                        //UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                    };

                    if (model != null)
                    {
 //                       Edit view = new Edit(model);
 //                       var r = view.ShowDialog();
 //                       if (r.Value)
 //                       {
 //                           sql.Clear();
 //                           sql.Append(@"UPDATE [dbo].[base_Clear]
 //  SET [Clear_name] = @Clear_name
 //     ,[Clear_level] = @Clear_level
 //     ,[remark] = @remark
 //     ,[is_enabled] = @is_enabled
 //     ,[update_dt] = @update_dt
 //     ,[update_user_id] = @user_id
 //WHERE [id]=@id");
 //                           parameters = new SqlParameter[] {
 //                           new SqlParameter("@Clear_name", model.ClearName),
 //                           new SqlParameter("@Clear_level", model.ClearLevel),
 //                           new SqlParameter("@remark", model.Remark),
 //                           new SqlParameter("@is_enabled", model.IsEnabled),
 //                           new SqlParameter("@update_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
 //                           new SqlParameter("@user_id", SessionInfo.Instance.Session.Id),
 //                           new SqlParameter("@id", id)
 //                       };

 //                           var result = SQLHelper.ExecuteNonQuery(sql.ToString(), parameters);

 //                           this.Query();
 //                       }
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
                    //var r = MessageBox.Show($"确定要删除【{string.Join(",", selected.Select(s => $"{s.ClearNo}|{s.ClearName}"))}】吗？", "提示", MessageBoxButton.YesNo);
                    //if (r == MessageBoxResult.Yes)
                    //{
                    //    foreach (var dr in selected)
                    //    {
                    //        //var sql = new StringBuilder(@"DELETE FROM [dbo].[base_Clear] WHERE [id] IN(@id)");
                    //        var sql = new StringBuilder(@"UPDATE [dbo].[base_Clear] SET [is_deleted]=1 WHERE [id]=@id");

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
     //           ClearModel model = new ClearModel();
     //           Edit view = new Edit(model);
     //           var r = view.ShowDialog();
     //           if (r.Value)
     //           {
     //               var sql = @"INSERT INTO [dbo].[base_Clear]
     //      ([Clear_no]
     //      ,[Clear_name]
     //      ,[Clear_level]
     //      ,[remark]
     //      ,[is_enabled]
     //      ,[is_deleted]
     //      ,[create_dt]
     //      ,[create_user_id]
     //      ,[update_dt]
     //      ,[update_user_id])
     //VALUES
     //      (@Clear_no
     //      ,@Clear_name
     //      ,@Clear_level
     //      ,@remark
     //      ,@is_enabled
     //      ,0
     //      ,@create_dt
     //      ,@user_id
     //      ,@create_dt
     //      ,@user_id)";

     //               var parameters = new SqlParameter[] {
     //               new SqlParameter("@Clear_no", model.ClearNo),
     //               new SqlParameter("@Clear_name", model.ClearName),
     //               new SqlParameter("@Clear_level", model.ClearLevel),
     //               new SqlParameter("@remark", model.Remark),
     //               new SqlParameter("@is_enabled", model.IsEnabled),
     //               new SqlParameter("@create_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
     //               new SqlParameter("@user_id", SessionInfo.Instance.Session.Id)
     //           };

     //               var result = SQLHelper.ExecuteNonQuery(sql, parameters);

     //               this.Query();
     //           }
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

        #endregion

        #region Privates

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageIndex"></param>
        private void Paging(int pageIndex)
        {

            GridModelList.Clear();//清空依赖属性

            var pagedData = ClearModels.Skip((pageIndex - 1) * DataCountPerPage).Take(DataCountPerPage).ToList();

            if (pagedData.Count > 0)
            {
                pagedData.ForEach(item =>
                {
                    GridModelList.Add(item);
                });
            }
        }

        #endregion
    }
}
