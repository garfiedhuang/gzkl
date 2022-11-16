using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HandyControl.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GZKL.Client.UI.ViewsModels
{
    /// <summary>
    /// 查询ViewModel基类
    /// </summary>
    public abstract class BaseSearchViewModel<TModel> : ViewModelBase where TModel : class
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseSearchViewModel()
        {
            QueryCommand = new RelayCommand(this.Query);
            ResetCommand = new RelayCommand(this.Reset);
            EditCommand = new RelayCommand<int>(this.Edit);
            DeleteCommand = new RelayCommand<int>(this.Delete);
            AddCommand = new RelayCommand(this.Add);
            PageUpdatedCommand = new RelayCommand<FunctionEventArgs<int>>(PageUpdated);


            TModels = new List<TModel>();
            GridData = new ObservableCollection<TModel>();
        }

        #region 通用属性

        /// <summary>
        /// 查询之后的结果数据，用于分页显示
        /// </summary>
        public static List<TModel> TModels { get; set; }

        /// <summary>
        /// 网格数据集合
        /// </summary>
        private ObservableCollection<TModel> gridData;
        public ObservableCollection<TModel> GridData
        {
            get { return gridData; }
            set { gridData = value; RaisePropertyChanged(); }
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

        #region 通用命令

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

        #region 命令基类实现

        /// <summary>
        /// 查询
        /// </summary>
        public virtual void Query() { }

        /// <summary>
        /// 重置
        /// </summary>
        public virtual void Reset() { }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Edit(int obj) { }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Delete(int obj) { }

        /// <summary>
        /// 新增
        /// </summary>
        public virtual void Add() { }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="obj"></param>
        public virtual void PageUpdated(FunctionEventArgs<int> obj)
        {
            Paging(obj.Info);
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageIndex"></param>
        public virtual void Paging(int pageIndex = 0)
        {
            //当前页数
            PageIndex = TModels.Count > 0 ? 1 : 0;
            MaxPageCount = 0;

            if (pageIndex == -1)
            {
                pageIndex = PageIndex;
            }

            //最大页数
            MaxPageCount = PageIndex > 0 ? (int)Math.Ceiling((decimal)TModels.Count / DataCountPerPage) : 0;

            //清空依赖属性
            GridData.Clear();

            //数据分页
            var pagedData = TModels.Skip((pageIndex - 1) * DataCountPerPage).Take(DataCountPerPage).ToList();

            if (pagedData.Count > 0)
            {
                pagedData.ForEach(item =>
                {
                    GridData.Add(item);
                });
            }
        }

        #endregion
    }
}
