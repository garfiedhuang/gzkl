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
                Modules = new System.Collections.ObjectModel.ObservableCollection<ModuleModel>(GetModules())
            });
            list.Add(new ModuleGroupModel
            {
                GroupName = "质量管理",
                ContractionTemplate = false,
                Icon = "\ue668"
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
        /// <returns></returns>
        public List<ModuleModel> GetModules()
        {
            List<ModuleModel> list = new List<ModuleModel>();
            list.Add(new ModuleModel
            {
                Code = "\ue693",
                Name = "用户管理",
                TypeName = "SystemMgt.User.User"
            });
            list.Add(new ModuleModel
            {
                Code = "\ue663",
                Name = "角色管理",
                TypeName = "SystemMgt.Role.Role"
            });
            list.Add(new ModuleModel
            {
                Code = "\ue695",
                Name = "通知公告",
                TypeName = "SystemMgt.Notice.Notice"
            });
            return list;
        }
    }
}
