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
    /// 接口模型
    /// </summary>
    public class InterfaceModel
    {
        /// <summary>
        /// 接口信息
        /// </summary>
        public List<InterfaceInfo> InterfaceInfos { get; set; }

        /// <summary>
        /// 接口对应检测项目
        /// </summary>
        public List<InterfaceTestItemInfo> InterfaceTestItemInfos { get; set; }

        /// <summary>
        /// 系统对应检测项目
        /// </summary>
        public List<SystemTestItemInfo> SystemTestItemInfos { get; set; }

        /// <summary>
        /// 接口与检测项关系
        /// </summary>
        public List<InterfaceTestItemRelationInfo> InterfaceTestItemRelationInfos { get; set; }

    }

    /// <summary>
    /// 接口信息
    /// </summary>
    public class InterfaceInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 接口名称
        /// </summary>
        public string InterfaceName { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string AccessDbPath { get; set; }

        /// <summary>
        /// 实例名
        /// </summary>
        public string AccessDbName { get; set; }

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
    }

    /// <summary>
    /// 接口对应检测项目
    /// </summary>
    public class InterfaceTestItemInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 接口ID
        /// </summary>
        public long InterfaceId { get; set; }

        /// <summary>
        /// 检测项名称
        /// </summary>
        public string TestItemName { get; set; }

        /// <summary>
        /// 主表
        /// </summary>
        public string TableMaster { get; set; }

        /// <summary>
        /// 明细表
        /// </summary>
        public string TableDetail { get; set; }

        /// <summary>
        /// 点表
        /// </summary>
        public string TableDot { get; set; }

    }

    /// <summary>
    /// 系统对应检测项目
    /// </summary>
    public class SystemTestItemInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 检测项编号
        /// </summary>
        public string TestItemNo { get; set; }

        /// <summary>
        /// 检测项名称
        /// </summary>
        public string TestItemName { get; set; }
    }

    /// <summary>
    /// 接口与检测项关系
    /// </summary>
    public class InterfaceTestItemRelationInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 接口ID
        /// </summary>
        public long InterfaceId { get; set; }

        /// <summary>
        /// 接口检测项编号
        /// </summary>
        public string InterfaceTestItemNo { get; set; }

        /// <summary>
        /// 接口检测项名称
        /// </summary>

        public string InterfaceTestItemName { get; set; }

        /// <summary>
        /// 系统检测项编号
        /// </summary>
        public string SystemTestItemNo { get; set; }

        /// <summary>
        /// 系统检测项名称
        /// </summary>
        public string SystemTestItemName { get; set; }
    }
}
