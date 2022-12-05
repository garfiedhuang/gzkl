using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.Views.SystemMgt.Menu;
using MessageBox = HandyControl.Controls.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;

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

            SelectItemChangedCommand = new RelayCommand<MenuDataModel>(this.SelectItemChanged);

            this.GridModelList = new ObservableCollection<MenuModel>();
            this.TreeViewList = new ObservableCollection<MenuDataModel>();

            this.Query();
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

        /// <summary>
        /// 已选中的节点
        /// </summary>
        private long selectedMenuId;
        public long SelectedMenuId
        {
            get { return selectedMenuId; }
            set { selectedMenuId = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 已选中的节点类型
        /// </summary>
        private int selectedMenuType;
        public int SelectedMenuType
        {
            get { return selectedMenuType; }
            set { selectedMenuType = value; RaisePropertyChanged(); }
        }

        private static List<MenuModel> MenuModels { get; set; }

        #endregion

        #region Command

        public RelayCommand<MenuDataModel> SelectItemChangedCommand { get; set; }

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
                ,[id],[parent_id],[name],[url],[icon],[type],[sort],[is_enabled],[is_deleted],[create_dt]
                ,[create_user_id],[update_dt],[update_user_id]
                FROM [dbo].[sys_menu] WHERE [is_deleted]=0 ORDER BY [type] ASC,[sort] ASC");

                SqlParameter[] parameters = null;

                if (MenuModels == null)
                {
                    MenuModels = new List<MenuModel>();
                }
                else
                {
                    MenuModels.Clear();
                }

                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            MenuModels.Add(new MenuModel()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                ParentId = Convert.ToInt64(dataRow["parent_id"]),
                                Name = dataRow["name"].ToString(),
                                Url = dataRow["url"].ToString(),
                                Icon = dataRow["icon"].ToString(),
                                Type = Convert.ToInt32(dataRow["type"]),
                                Sort = Convert.ToInt32(dataRow["sort"]),
                                IsEnabled = Convert.ToInt32(dataRow["is_enabled"]),
                                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                                UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                            });
                        }
                    }
                }

                this.TreeViewList?.Clear();

                //封装菜单树
                var roots = MenuModels.Where(w => w.Type == 1)?.ToList();
                roots?.ForEach(data =>
                {
                    if (SelectedMenuId == 0)
                    {
                        SelectedMenuId = data.Id;
                        SelectedMenuType= data.Type;
                    }
                    TreeViewList.Add(new MenuDataModel()
                    {
                        Index = Convert.ToInt32(data.Id),
                        Name = data.Name,
                        Type = (MenuType)data.Type,
                        IsExpanded = data.Type < 2,
                        DataList = GetTreeViewList(MenuModels.Where(w => w.ParentId == data.Id)?.ToList(), MenuModels),
                        IsSelected = SelectedMenuId == data.Id,
                    });
                });

                //查询一级菜单明细
                GetMenuDetail();
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
        public void Edit(long id)
        {
            try
            {
                var selected = GridModelList.FirstOrDefault(w => w.Id == id);

                var sql = new StringBuilder(@"SELECT * FROM [dbo].[sys_menu] WHERE [is_deleted]=0 AND [id]=@id");

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
                        ParentId = Convert.ToInt64(dataRow["parent_id"]),
                        Name = dataRow["name"].ToString(),
                        Url = dataRow["url"].ToString(),
                        Icon = dataRow["icon"].ToString(),
                        Type = Convert.ToInt32(dataRow["type"]),
                        Sort = Convert.ToInt32(dataRow["sort"]),
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
                            sql.Append(@"UPDATE [dbo].[sys_menu]
   SET [parent_id] = @parentId
      ,[name] = @name
      ,[url] = @url
      ,[icon] = @icon
      ,[type] = @type
      ,[sort] = @sort
      ,[is_enabled] = @is_enabled
      ,[update_dt] = @update_dt
      ,[update_user_id] = @user_id
 WHERE [id]=@id");
                            parameters = new SqlParameter[] {
                            new SqlParameter("@parentId", model.ParentId),
                            new SqlParameter("@name", model.Name),
                            new SqlParameter("@url", model.Url),
                            new SqlParameter("@icon", model.Icon),
                            new SqlParameter("@type", model.Type),
                            new SqlParameter("@sort", model.Sort),
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
        /// <param name="id"></param>
        public void Delete(long id)
        {
            try
            {
                var selected = MenuModels.FirstOrDefault(x => x.Id == id);
                var r = MessageBox.Show($"确定要删除【{string.Join(",", $"{selected.Id}|{selected.Name}")}】吗？", "提示", MessageBoxButton.YesNo);
                if (r == MessageBoxResult.Yes)
                {

                    //判断下级是否还有菜单，如果有不能删除
                    var includeSubmenu = MenuModels.Count(c => c.ParentId == selected.Id);
                    if (includeSubmenu > 0)
                    {
                        throw new Exception("当前菜单包含子菜单，不能删除！");
                    }

                    var sql = new StringBuilder(@"UPDATE [dbo].[sys_Menu] SET [is_deleted]=1 WHERE [id]=@id");
                    var parameters = new SqlParameter[1] { new SqlParameter("@id", id) };
                    var result = SQLHelper.ExecuteNonQuery(sql.ToString(), parameters);

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
        public void Add()
        {
            try
            {
                MenuModel model = new MenuModel();

                model.ParentId = SelectedMenuId;
                model.Type = SelectedMenuType+1;

                if (model.Type == 5)
                {
                    throw new Exception("最多只能新增三级菜单！");
                }
                
                Edit view = new Edit(model);
                var r = view.ShowDialog();
                if (r.Value)
                {
                    var sql = @"INSERT INTO [dbo].[sys_menu]
           ([parent_id]
           ,[name]
           ,[url]
           ,[icon]
           ,[type]
           ,[sort]
           ,[is_enabled]
           ,[is_deleted]
           ,[create_dt]
           ,[create_user_id]
           ,[update_dt]
           ,[update_user_id])
     VALUES
           (@parentId
           ,@name
           ,@url
           ,@icon
           ,@type
           ,@sort
           ,@is_enabled
           ,0
           ,@create_dt
           ,@user_id
           ,@create_dt
           ,@user_id)";

                    var parameters = new SqlParameter[] {
                    new SqlParameter("@parentId", SelectedMenuId),
                    new SqlParameter("@name", model.Name),
                    new SqlParameter("@url", model.Url??""),
                    new SqlParameter("@icon", model.Icon??""),
                    new SqlParameter("@type", model.Type),
                    new SqlParameter("@sort", model.Sort),
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
        /// 菜单选中改变事件
        /// </summary>
        /// <param name="menuDataModel"></param>
        private void SelectItemChanged(MenuDataModel menuDataModel)
        {
            if (menuDataModel == null)
            {
                return;
            }

            SelectedMenuId = menuDataModel.Index;
            selectedMenuType = (int)menuDataModel.Type;

            GetMenuDetail();
        }
        #endregion

        #region Privates

        private ObservableCollection<MenuDataModel> GetTreeViewList(List<MenuModel> currentData, List<MenuModel> allData)
        {
            var result = new ObservableCollection<MenuDataModel>();
            currentData?.ForEach(item =>
            {
                result.Add(new MenuDataModel()
                {
                    Index = Convert.ToInt32(item.Id),
                    Name = item.Name,
                    Type = (MenuType)item.Type,
                    DataList = GetTreeViewList(allData.Where(w => w.ParentId == item.Id)?.ToList(), allData),
                    IsSelected = SelectedMenuId == item.Id
                });
            });

            return result;
        }

        private void GetMenuDetail()
        {
            this.GridModelList?.Clear();

            var pramaryNodes = MenuModels?.Where(w => w.ParentId == SelectedMenuId)?.ToList();

            pramaryNodes?.ForEach(data =>
            {
                GridModelList.Add(new MenuModel()
                {
                    Id = Convert.ToInt64(data.Id),
                    ParentId = data.ParentId,
                    Name = data.Name,
                    Url = data.Url,
                    Icon = data.Icon,
                    Type = data.Type,
                    Sort = data.Sort,
                    IsEnabled = data.IsEnabled,
                    CreateDt = data.CreateDt,
                    UpdateDt = data.UpdateDt
                });
            });
        }

        #endregion
    }
}
