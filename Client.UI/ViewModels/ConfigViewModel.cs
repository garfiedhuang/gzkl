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
using GZKL.Client.UI.Views.SystemMgt.Config;
using GalaSoft.MvvmLight.Messaging;

namespace GZKL.Client.UI.ViewsModels
{
    public class ConfigViewModel : ViewModelBase
    {
        #region Construct and property

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfigViewModel()
        {
            QueryCommand = new RelayCommand(this.Query);
            ResetCommand = new RelayCommand(this.Reset);
            EditCommand = new RelayCommand<int>(this.Edit);
            DeleteCommand = new RelayCommand<int>(this.Delete);
            AddCommand = new RelayCommand(this.Add);
            PageUpdatedCommand = new RelayCommand<FunctionEventArgs<int>>(PageUpdated);

            ConfigModels = new List<ConfigModel>();
            GridModelList = new ObservableCollection<ConfigModel>();
        }

        /// <summary>
        /// 查询之后的结果数据，用于分页显示
        /// </summary>
        private static List<ConfigModel> ConfigModels { get; set; }

        /// <summary>
        /// 网格数据集合
        /// </summary>
        private ObservableCollection<ConfigModel> gridModelList;
        public ObservableCollection<ConfigModel> GridModelList
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
                ,[id],[name],[password],[head_img],[phone],[email]
                ,[sex],[birthday],[is_enable],[is_deleted],[create_dt]
                ,[create_Config_id],[update_dt],[update_Config_id]
                FROM [dbo].[sys_Config] WHERE [is_deleted]=0");

                SqlParameter[] parameters = null;

                if (!string.IsNullOrEmpty(Search.Trim()))
                {
                    sql.Append($" AND [name] LIKE @search");
                    parameters = new SqlParameter[1] { new SqlParameter("@search", $"%{Search}%") };
                }

                sql.Append($" ORDER BY [update_dt] DESC");

                ConfigModels.Clear();//清空前端分页数据

                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            ConfigModels.Add(new ConfigModel()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                RowNum = Convert.ToInt64(dataRow["row_num"]),
                                Name = dataRow["name"].ToString(),
                                Email = dataRow["email"].ToString(),
                                Phone = dataRow["phone"].ToString(),
                                HeadImg = dataRow["head_img"].ToString(),
                                Sex = Convert.ToInt32(dataRow["sex"]),
                                Birthday = Convert.ToDateTime(dataRow["birthday"] ?? DateTime.MinValue),
                                IsEnabled = Convert.ToInt32(dataRow["is_enable"]),
                                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                                UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                            });
                        }
                    }
                }

                //当前页数
                PageIndex = ConfigModels.Count > 0 ? 1 : 0;
                MaxPageCount = 0;

                //最大页数
                MaxPageCount = PageIndex > 0 ? (int)Math.Ceiling((decimal)ConfigModels.Count / DataCountPerPage) : 0;

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

                var sql = new StringBuilder(@"SELECT [id],[name],[password],[head_img],[phone],[email]
                ,[sex],[birthday],[is_enable],[is_deleted],[create_dt]
                ,[create_Config_id],[update_dt],[update_Config_id]
                FROM [dbo].[sys_Config] WHERE [is_deleted]=0 AND [id]=@id");

                var parameters = new SqlParameter[1] { new SqlParameter("@id", id) };
                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data == null || data.Rows.Count == 0)
                    {
                        MessageBox.Show($"数据库不存在 主键ID={id} 的记录", "提示信息");
                        return;
                    }

                    var dataRow = data.Rows[0];
                    var model = new ConfigModel()
                    {
                        Id = Convert.ToInt64(dataRow["id"]),
                        Name = dataRow["name"].ToString(),
                        Email = dataRow["email"].ToString(),
                        Phone = dataRow["phone"].ToString(),
                        HeadImg = dataRow["head_img"].ToString(),
                        Sex = Convert.ToInt32(dataRow["sex"]),
                        Birthday = Convert.ToDateTime(dataRow["birthday"] ?? DateTime.MinValue),
                        IsEnabled = Convert.ToInt32(dataRow["is_enable"]),
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
                            sql.Append(@"UPDATE [dbo].[sys_Config]
   SET [head_img] = @head_img
      ,[phone] = @phone
      ,[email] = @email
      ,[sex] = @sex
      ,[birthday] = @birthday
      ,[is_enable] = @is_enable
      ,[update_dt] = @update_dt
      ,[update_Config_id] = @Config_id
 WHERE [id]=@id");
                            parameters = new SqlParameter[] {
                            new SqlParameter("@head_img", "/Assets/Images/default.png"),
                            new SqlParameter("@phone", model.Phone),
                            new SqlParameter("@email", model.Email),
                            new SqlParameter("@sex", model.Sex),
                            new SqlParameter("@birthday", model.Birthday),
                            new SqlParameter("@is_enable", model.IsEnabled),
                            new SqlParameter("@update_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                            new SqlParameter("@Config_id", SessionInfo.Instance.Session.Id),
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
                    var r = MessageBox.Show($"确定要删除【{string.Join(",", selected.Select(s => s.Name))}】吗？", "提示", MessageBoxButton.YesNo);
                    if (r == MessageBoxResult.Yes)
                    {
                        foreach (var dr in selected)
                        {
                            //var sql = new StringBuilder(@"DELETE FROM [dbo].[sys_Config] WHERE [id] IN(@id)");
                            var sql = new StringBuilder(@"UPDATE [dbo].[sys_Config] SET [is_deleted]=1 WHERE [id]=@id");

                            var parameters = new SqlParameter[1] { new SqlParameter("@id", dr.Id) };
                            var result = SQLHelper.ExecuteNonQuery(sql.ToString(), parameters);
                        }

                        this.Query();
                    }
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
                ConfigModel model = new ConfigModel();
                Edit view = new Edit(model);
                var r = view.ShowDialog();
                if (r.Value)
                {
                    var sql = @"INSERT INTO [dbo].[sys_Config]
           ([name]
           ,[password]
           ,[head_img]
           ,[phone]
           ,[email]
           ,[sex]
           ,[birthday]
           ,[is_enable]
           ,[is_deleted]
           ,[create_dt]
           ,[create_Config_id]
           ,[update_dt]
           ,[update_Config_id])
     VALUES
           (@name
           ,@password
           ,@head_img
           ,@phone
           ,@email
           ,@sex
           ,@birthday
           ,1
           ,0
           ,@create_dt
           ,@Config_id
           ,@create_dt
           ,@Config_id)";

                    var parameters = new SqlParameter[] {
                    new SqlParameter("@name", model.Name),
                    new SqlParameter("@password", "123456"),
                    new SqlParameter("@head_img", "/Assets/Images/default.png"),
                    new SqlParameter("@phone", model.Phone),
                    new SqlParameter("@email", model.Email),
                    new SqlParameter("@sex", model.Sex),
                    new SqlParameter("@birthday", model.Birthday),
                    new SqlParameter("@create_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SqlParameter("@Config_id", SessionInfo.Instance.Session.Id)
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

            GridModelList.Clear();//情况依赖属性

            var pagedData = ConfigModels.Skip((pageIndex - 1) * DataCountPerPage).Take(DataCountPerPage).ToList();

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
