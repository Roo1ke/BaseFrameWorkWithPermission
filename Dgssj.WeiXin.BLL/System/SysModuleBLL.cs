using Dgssj.WeiXin.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dgssj.WeiXin.BLL
{
    public class SysModuleBLL
    {
        /// <summary>
        /// 验证用户是否有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="area"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool CheckAdminPermit(int userId, string area, string controller, string action)
        {
            return DAL.SysModuleDAL.CheckAdminPermit(userId, area, controller, action);
        }

        public static RtnMessage GetModuleList()
        {
            return DAL.SysModuleDAL.GetModuleList();
        }
        public static RtnMessage GetModuleListByUser(int userID)
        {
            return DAL.SysModuleDAL.GetModuleByUser(userID);
        }
        public static RtnMessage GetModuleListByRole(int roleId) {
            return DAL.SysModuleDAL.GetModuleByUser(roleId);
        }

    }
}
