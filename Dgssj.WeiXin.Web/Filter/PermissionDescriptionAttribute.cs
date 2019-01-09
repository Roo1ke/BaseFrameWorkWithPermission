using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dgssj.WeiXin.Web
{
    /// <summary>
    /// 功能权限
    /// </summary>
    public class PermissionDescriptionAttribute : Attribute
    {
        /// <summary>
        /// 功能名称
        /// </summary>
        public string FuncName { get; set; }
        /// <summary>
        /// 是否菜单
        /// </summary>
        public bool IsMenu { get; set; }
        /// <summary>
        /// 所属模块
        /// </summary>
        public SecondModuleMenu ParentMenu { get; set; }

        public PermissionDescriptionAttribute(SecondModuleMenu menu, string funcName, bool isMenu = false)
        {
            this.FuncName = funcName;
            this.ParentMenu = menu;
            this.IsMenu = isMenu;
        }
    }
}
