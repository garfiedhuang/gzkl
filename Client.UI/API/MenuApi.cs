using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GZKL.Client.UI.Models;

namespace GZKL.Client.UI.API
{
     public class MenuApi
    {
        /// <summary>
        /// 获取模块分组集合
        /// </summary>
        /// <returns></returns>
        public List<ModuleGroupModel> GetModuleGroups() 
        {
            List<ModuleGroupModel> list = new List<ModuleGroupModel>();
            list.Add(new ModuleGroupModel
            {
                GroupName = "系统管理",
                Icon= "\ue691",
                ContractionTemplate = false,
                Modules = new System.Collections.ObjectModel.ObservableCollection<ModuleModel>(GetModules("系统管理"))
            });
            list.Add(new ModuleGroupModel
            {
                GroupName = "采集管理",
                ContractionTemplate = false,
                Icon = "\ue668",
                Modules = new System.Collections.ObjectModel.ObservableCollection<ModuleModel>(GetModules("采集管理"))
            });
            list.Add(new ModuleGroupModel { 
              GroupName="统计报表",
              ContractionTemplate=false,
              Icon= "\ue670"
            });
            return list;
        }

        /// <summary>
        /// 获取模块集合
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public List<ModuleModel> GetModules(string group)
        {
            List<ModuleModel> list = new List<ModuleModel>();
            if (group == "系统管理")
            {
                list.Add(new ModuleModel
                {
                    Code = "\ue693",
                    Name = "用户管理",
                    TypeName = "SystemMgt.User.User"
                });
                list.Add(new ModuleModel
                {
                    Code = "\ue663",
                    Name = "菜单管理",
                    TypeName = "SystemMgt.Menu.Menu"
                });
                list.Add(new ModuleModel
                {
                    Code = "\ue663",
                    Name = "角色管理",
                    TypeName = "SystemMgt.Role.Role"
                });
                list.Add(new ModuleModel
                {
                    Code = "\ue66e",
                    Name = "权限管理",
                    TypeName = "SystemMgt.Permission.Permission"
                });
                list.Add(new ModuleModel
                {
                    Code = "\ue66a",
                    Name = "数据字典",
                    TypeName = "SystemMgt.Config.Config"
                });
            }
            else if(group=="采集管理")
            {
                list.Add(new ModuleModel
                {
                    Code = "\ue669",
                    Name = "自动采集",
                    TypeName = "CollectMgt.AutoCollect.Index"
                });
                list.Add(new ModuleModel
                {
                    Code = "\ue669",
                    Name = "接口设置",
                    TypeName = "CollectMgt.Interface.Interface"
                });
                list.Add(new ModuleModel
                {
                    Code = "\ue669",
                    Name = "数据备份",
                    TypeName = "CollectMgt.Backup.Backup"
                });
                list.Add(new ModuleModel
                {
                    Code = "\ue669",
                    Name = "数据清理",
                    TypeName = "CollectMgt.Clear.Clear"
                });
                list.Add(new ModuleModel
                {
                    Code = "\ue669",
                    Name = "数据导出",
                    TypeName = "CollectMgt.Export.Export"
                });
                list.Add(new ModuleModel
                {
                    Code = "\ue669",
                    Name = "参数设置",
                    TypeName = "CollectMgt.Parameter.Parameter"
                });
                list.Add(new ModuleModel
                {
                    Code = "\ue669",
                    Name = "单位设置",
                    TypeName = "CollectMgt.Org.Org"
                });
                list.Add(new ModuleModel
                {
                    Code = "\ue669",
                    Name = "软件注册",
                    TypeName = "CollectMgt.Register.Register"
                });

            }
            return list;
        }
    }
}
