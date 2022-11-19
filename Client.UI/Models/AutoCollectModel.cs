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
    /// 自动采集模型
    /// </summary>
    public class AutoCollectModel : ObservableObject
    {

        /// <summary>
        /// 机构
        /// </summary>
        private string orgNo = string.Empty;

        public string OrgNo
        {
            get { return orgNo; }
            set
            {
                orgNo = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 检测类型
        /// </summary>
        private string testTypeNo = string.Empty;

        public string TestTypeNo
        {
            get { return testTypeNo; }
            set
            {
                testTypeNo = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 系统检测项目
        /// </summary>
        private string systemTestItemNo = string.Empty;

        public string SystemTestItemNo
        {
            get { return systemTestItemNo; }
            set
            {
                systemTestItemNo = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 接口名称
        /// </summary>
        private string interfaceName = string.Empty;

        public string InterfaceName
        {
            get { return interfaceName; }
            set
            {
                interfaceName = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 接口检测项目
        /// </summary>
        private string interfaceTestItemNo = string.Empty;

        public string InterfaceTestItemNo
        {
            get { return interfaceTestItemNo; }
            set
            {
                interfaceTestItemNo = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 查询条件-检测编号
        /// </summary>
        private string queryTestItemNo = string.Empty;

        public string QueryTestItemNo
        {
            get { return queryTestItemNo; }
            set
            {
                queryTestItemNo = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 查询条件-样品编号
        /// </summary>
        private string querySampleNo = string.Empty;

        public string QuerySampleNo
        {
            get { return querySampleNo; }
            set
            {
                querySampleNo = value;
                RaisePropertyChanged();
            }
        }

    }

    /// <summary>
    /// 机构信息
    /// </summary>
    public class OrgInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 机构编号
        /// </summary>
        public string OrgNo { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public string OrgLevel { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; } = "";

    }

    /// <summary>
    /// 检测类型
    /// </summary>
    public class TestTypeInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public string Category { set; get; }

        /// <summary>
        /// 编号
        /// </summary>
        public string TestTypeNo { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string TestTypeName { set; get; }
    }
}
