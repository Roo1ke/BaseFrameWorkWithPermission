using Dgssj.WeiXin.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dgssj.WeiXin.BLL
{
    public class SysRolesBLL
    {
        public static RtnMessage GetRoleList(Sys_Roles roles)
        {
            return DAL.SysRolesDAL.GetRoleList(roles);
        }

        public static RtnMessage Save(Sys_Roles roles)
        {
            return DAL.SysRolesDAL.SaveRoles(roles);
        }
        public static RtnMessage DeleteRoles(List<Sys_Roles> list)
        {
            return DAL.SysRolesDAL.DeleteRoles(list);
        }
        public static RtnMessage CheckRoleName(string rolename)
        {
            return DAL.SysRolesDAL.CheckRoleName(rolename);
        }

        public static RtnMessage SetRoleMenu(int roleID, int[] menuids)
        {
            return DAL.SysRolesDAL.SetRoleMenu(roleID, menuids);
        }
    }
}
