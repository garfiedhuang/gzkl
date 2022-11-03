using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GZKL.Cilent.UI.Models;
using System.Windows;
using HandyControl.Data;
using System.Data.SqlClient;
using GZKL.Client.UI.Common;
using System.Data;
using System.Windows.Controls;
using MessageBox = HandyControl.Controls.MessageBox;
using GZKL.Cilent.UI.Views.SystemMgt.User;

namespace GZKL.Cilent.UI.ViewsModels
{
    public class UserViewModel : ViewModelBase
    {
        #region Construct and property

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserViewModel()
        {
            QueryCommand = new RelayCommand(this.Query);
            ResetCommand = new RelayCommand(this.Reset);
            EditCommand = new RelayCommand<int>(this.Edit);
            DeleteCommand = new RelayCommand<int>(this.Delete);
            AddCommand = new RelayCommand(this.Add);
            PageUpdatedCommand = new RelayCommand<FunctionEventArgs<int>>(PageUpdated);

            UserModels = new List<UserModel>();
            GridModelList = new ObservableCollection<UserModel>();
        }


        /// <summary>
        /// 查询之后的结果数据，用于分页显示
        /// </summary>
        private static List<UserModel> UserModels { get; set; }

        /// <summary>
        /// 网格数据集合
        /// </summary>
        private ObservableCollection<UserModel> gridModelList;
        public ObservableCollection<UserModel> GridModelList
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
                var sql = new StringBuilder(@"SELECT [id],[name],[email],[address],[status],[user_type],[is_deleted],
                                                     [create_dt],[create_user_id],[update_dt],[update_user_id] 
                                              FROM [dbo].[sys_user] WHERE 1=1");

                var parameters = new SqlParameter[] { };

                if (!string.IsNullOrEmpty(Search.Trim()))
                {
                    sql.Append($" AND [name] LIKE @search");
                    parameters[0] = new SqlParameter("@search", $"%{Search}%");
                }

                sql.Append($" ORDER BY [update_dt] DESC");

                UserModels.Clear();//清空前端分页数据

                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            UserModels.Add(new UserModel()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                Name = dataRow["name"].ToString(),
                                Email = dataRow["email"].ToString(),
                                Address = dataRow["address"].ToString(),
                                Status = dataRow["status"].ToString(),
                                UserType = dataRow["user_type"].ToString(),
                                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                                UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                            });
                        }
                    }
                }

                //当前页数
                PageIndex = UserModels.Count > 0 ? 1 : 0;
                MaxPageCount = 0;

                //最大页数
                MaxPageCount = PageIndex > 0 ? (int)Math.Ceiling((decimal)UserModels.Count / DataCountPerPage) : 0;

                //数据分页
                Paging(PageIndex);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        /// <param name="Id"></param>
        public void Edit(int Id)
        {
            //var model = localDb.QueryById(Id);
            //if (model != null)
            //{
            //    StudentWindow view = new StudentWindow(model);
            //    var r = view.ShowDialog();
            //    if (r.Value)
            //    {
            //        var newModel = GridModelList.FirstOrDefault(t => t.Id == model.Id);
            //        if (newModel != null)
            //        {
            //            newModel.Name = model.Name;
            //            newModel.Age = model.Age;
            //            newModel.Classes = model.Classes;
            //        }
            //        this.Query();
            //    }
            //}
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(int Id)
        {
            //var model = localDb.QueryById(Id);
            //if (model != null)
            //{
            //    var r = MessageBox.Show($"确定要删除吗【{model.Name}】？", "提示", MessageBoxButton.YesNo);
            //    if (r == MessageBoxResult.Yes)
            //    {
            //        localDb.DelStudent(Id);
            //        this.Query();
            //    }
            //}
        }

        /// <summary>
        /// 新增
        /// </summary>
        public void Add()
        {
            UserModel model = new UserModel();
            Edit view = new Edit(model);
            var r = view.ShowDialog();
            //if (r.Value)
            //{
            //    model.Id = GridModelList.Max(t => t.Id) + 1;
            //    //localDb.AddStudent(model);
            //    this.Query();
            //}
        }


        //private void BtTest(object o)
        //{
        //    string str = "";
        //    for (int i = 0; i < GridModelList.Count; i++) {
        //        UserModel temp = GridModelList[i];
        //        Console.WriteLine(temp.IsSelected);
        //        Console.WriteLine(temp.Name);
        //        if (temp.IsSelected) { 
        //            str=string.IsNullOrEmpty(str)? temp.Name:(str+","+ temp.Name);
        //        }
        //    }
        //    MessageBox.Success(str,"提示信息" );
        //}

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

            var pagedData = UserModels.Skip((pageIndex - 1) * DataCountPerPage).Take(DataCountPerPage).ToList();

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
