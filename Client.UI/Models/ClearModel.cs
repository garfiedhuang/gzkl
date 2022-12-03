using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Client.UI.Models
{
    /// <summary>
    /// 数据清理模型
    /// </summary>
    public class ClearModel : ObservableObject
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        public long RowNum { get; set; }

        /// <summary>
        /// 清理编号
        /// </summary>
        public string ClearNo { get; set; }

        /// <summary>
        /// 清理时间
        /// </summary>
        public DateTime? ClearTime { get; set; }

        /// <summary>
        /// 清理类型
        /// </summary>
        public string ClearType { get; set; } = "试验日期";

        /// <summary>
        /// 条件
        /// </summary>
        public string Conditions { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; } = "";

        /// <summary>
        /// 是否启用 0-否 1-是
        /// </summary>
        public int IsEnabled { get; set; } = 1;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDt { get; set; }

        /// <summary>
        /// 是否选中?
        /// </summary>
        private bool isSelected = false;

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; RaisePropertyChanged("IsSelected"); }
        }

    }

    /// <summary>
    /// 查下模型
    /// </summary>
    public class ClearQueryModel
    {
        /// <summary>
        /// 清理类型
        /// </summary>
        public string ClearType { get; set; } = "试验日期";


        /// <summary>
        /// 试验开始日期
        /// </summary>
        public DateTime? StartTestDt { get; set; } = DateTime.Now.AddDays(-7);

        /// <summary>
        /// 试验结束日期
        /// </summary>
        public DateTime? EndTestDt { get; set; } = DateTime.Now;

        /// <summary>
        /// 检测开始编号
        /// </summary>
        public string StartTestNo { get; set; }

        /// <summary>
        /// 检测结束编号
        /// </summary>
        public string EndTestNo { get; set; }

        /// <summary>
        /// 样品开始编号
        /// </summary>
        public string StartSampleNo { get; set; }

        /// <summary>
        /// 样品结束编号
        /// </summary>
        public string EndSampleNo { get; set; }
    }
}
