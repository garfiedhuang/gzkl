﻿using GalaSoft.MvvmLight;
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
        /// 接口ID
        /// </summary>
        private long interfaceId = 0L;

        public long InterfaceId
        {
            get { return interfaceId; }
            set
            {
                interfaceId = value;
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

        #region 检测结果数据

        /// <summary>
        /// 检测主表数据
        /// </summary>
        private List<ExecuteTestInfo> testData;

        public List<ExecuteTestInfo> TestData
        {
            get { return testData; }
            set
            {
                testData = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 检测主表数据（待处理）
        /// </summary>
        private List<ExecuteTestInfo> unfinishTestData;

        public List<ExecuteTestInfo> UnfinishTestData
        {
            get { return unfinishTestData; }
            set
            {
                unfinishTestData = value;
                RaisePropertyChanged();
            }
        }


        /// <summary>
        /// 检测明细表数据
        /// </summary>
        private List<ExecuteTestDetailInfo> testDetailData;

        public List<ExecuteTestDetailInfo> TestDetailData
        {
            get { return testDetailData; }
            set
            {
                testDetailData = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 检测明细表数据（待处理）
        /// </summary>
        private List<ExecuteTestDetailInfo> unfinishTestDetailData;

        public List<ExecuteTestDetailInfo> UnfinishTestDetailData
        {
            get { return unfinishTestDetailData; }
            set
            {
                unfinishTestDetailData = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 原始数据
        /// </summary>
        private List<ExecuteOriginalDataInfo> originalData;

        public List<ExecuteOriginalDataInfo> OriginalData
        {
            get { return originalData; }
            set
            {
                originalData = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 原始数据（待处理）
        /// </summary>
        private List<ExecuteOriginalDataInfo> unfinishOriginalData;

        public List<ExecuteOriginalDataInfo> UnfinishOriginalData
        {
            get { return unfinishOriginalData; }
            set
            {
                unfinishOriginalData = value;
                RaisePropertyChanged();
            }
        }

        #endregion
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

    /// <summary>
    /// 检测主表信息
    /// </summary>
    public class ExecuteTestInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 机构编号
        /// </summary>
        public string OrgNo { set; get; }

        /// <summary>
        /// 检测编号
        /// </summary>
        public string TestNo { set; get; }

        /// <summary>
        /// 样品编号
        /// </summary>
        public string SampleNo { set; get; }

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
    }

    /// <summary>
    /// 检测明细表信息
    /// </summary>
    public class ExecuteTestDetailInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 主表ID
        /// </summary>
        public long TestId { set; get; }

        /// <summary>
        /// 试件序号
        /// </summary>
        public int ExperimentNo { set; get; }

        /// <summary>
        /// 试验时间
        /// </summary>
        public DateTime PlayTime { set; get; }

        /// <summary>
        /// 试样的面积
        /// </summary>
        public string Area { set; get; }

        /// <summary>
        /// 上屈服点力值
        /// </summary>
        public string UpYieldDot { set; get; }

        /// <summary>
        /// 下屈服点力值
        /// </summary>
        public string DownYieldDot { set; get; }

        /// <summary>
        /// 最大值(力值)
        /// </summary>
        public string MaxDot { set; get; }
    }

    /// <summary>
    /// 原始数据信息
    /// </summary>
    public class ExecuteOriginalDataInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 主表ID
        /// </summary>
        public long TestId { set; get; }

        /// <summary>
        /// 试验时间（时间值）
        /// </summary>
        public DateTime PlayTime { set; get; }

        /// <summary>
        /// 试件序号
        /// </summary>
        public int ExperimentNo { set; get; }

        /// <summary>
        /// 力值
        /// </summary>
        public string LoadValue { set; get; }

        /// <summary>
        /// 位移的值
        /// </summary>
        public string PositionValue { set; get; }

        /// <summary>
        /// 引伸记的值
        /// </summary>
        public string ExtendValue { set; get; }

        /// <summary>
        /// 大变形的值
        /// </summary>
        public string BigDeformValue { set; get; }

        /// <summary>
        /// 变现切换
        /// </summary>
        public string DeformSwitch { set; get; }

        /// <summary>
        /// 控制步骤
        /// </summary>
        public string CtrlStep { set; get; }

        /// <summary>
        /// 扩展设备1
        /// </summary>
        public string ExtendDevice1 { set; get; }

        /// <summary>
        /// 扩展设备2
        /// </summary>
        public string ExtendDevice2 { set; get; }

        /// <summary>
        /// 扩展设备3
        /// </summary>
        public string ExtendDevice3 { set; get; }

        /// <summary>
        /// 扩展设备4
        /// </summary>
        public string ExtendDevice4 { set; get; }

        /// <summary>
        /// 扩展设备5
        /// </summary>
        public string ExtendDevice5 { set; get; }

        /// <summary>
        /// 扩展设备6
        /// </summary>
        public string ExtendDevice6 { set; get; }

        /// <summary>
        /// 位移速率
        /// </summary>
        public string PosiSpeed { set; get; }

        /// <summary>
        /// 应力速率
        /// </summary>
        public string StressSpeed { set; get; }

    }
}
