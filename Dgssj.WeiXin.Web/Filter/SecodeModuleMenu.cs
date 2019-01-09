using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Dgssj.WeiXin.Web
{
    /// <summary>
    /// 模块菜单
    /// </summary>
    public enum SecondModuleMenu
    {
        [Description(FirstModuleMenu.系统管理)]
        系统设置,

        [Description(FirstModuleMenu.用户管理)]
        用户管理,
    }
    public class FirstModuleMenu
    {
        public const string 系统管理 = "系统|icon-setting";
        public const string 用户管理 = "用户|icon-user";
    }
}
