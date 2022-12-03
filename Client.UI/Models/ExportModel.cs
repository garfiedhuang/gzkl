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
    /// 导出模型
    /// </summary>
    public class ExportModel : ObservableObject
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
        /// 机构编号
        /// </summary>
        public string OrgNo { get; set; }

        /// <summary>
        /// 检测编号
        /// </summary>
        public string TestNo { get; set; }

        /// <summary>
        /// 样品编号
        /// </summary>
        public string SampleNo { get; set; }

        /// <summary>
        /// 检测类型编号
        /// </summary>
        public string TestTypeNo { set; get; }

        /// <summary>
        /// 检测项编号
        /// </summary>
        public string TestItemNo { set; get; }
        /// <summary>
        /// 龄期
        /// </summary>
        public string Deadline { set; get; }

        /// <summary>
        /// 试验次数
        /// </summary>
        public string ExperimentNo { set; get; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime PlayTime { set; get; }

        /// <summary>
        /// 负载单位名称
        /// </summary>
        public string LoadUnitName { set; get; }

        /// <summary>
        /// 压力单位名称
        /// </summary>
        public string PressUnitName { set; get; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string SampleShape { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Area { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string GaugeLength { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string UpYieldDot { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string DownYieldDot { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MaxDot { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string SampleWidth { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string SampleThick { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string SampleDia { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string SampleMinDia { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string SampleOutDia { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string SampleInnerDia { set; get; }

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

    public class SearchModel
    {

    }
}
