using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dgssj.WeiXin.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Dgssj.WeiXin.Web.CheckPermissionFilter;

namespace Dgssj.WeiXin.Web.Controllers
{
    [Authorize]
    [CheckPermissionFilter]
    public class SystemController : ControllerBase
    {
        #region 用户管理
        public IActionResult Users()
        {
            return View();
        }

        [ParentPermissionAttribute("","system", "users")]
        [HttpGet]
        public string GetUserList(Sys_Users data) {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.SysUsersBLL.GetUserList(data);
            var roleList = BLL.SysRolesBLL.GetRoleList(new Sys_Roles());
            return Newtonsoft.Json.JsonConvert.SerializeObject(new { rtnMsg ,roleList });
        }

        [ParentPermissionAttribute("", "system", "users")]
        [HttpPost]
        public string SaveUser(Sys_Users data) {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.SysUsersBLL.Save(data);
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }

        [ParentPermissionAttribute("", "system", "users")]
        [HttpPost]
        public string SaveUserAtIndex(Sys_Users data)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.SysUsersBLL.SaveUserAtIndex(data);
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }

        [ParentPermissionAttribute("", "system", "users")]
        [HttpPost]
        public string ModifyPwd(string pwd,string newpwd) {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.SysUsersBLL.ModifyPassword(_userId,pwd,newpwd);
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }

        [ParentPermissionAttribute("", "system", "users")]
        [HttpPost]
        public string ResetPwd(int userID)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.SysUsersBLL.ResetPassword(userID);
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }

        [ParentPermissionAttribute("", "system", "users")]
        [HttpPost]
        public string DeleteUsers(List<Sys_Users> list)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.SysUsersBLL.DeleteUsers(list);
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }

        [ParentPermissionAttribute("", "system", "users")]
        [HttpPost]
        public string CheckUserName(string username)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.SysUsersBLL.CheckUserName(username);
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }

        [ParentPermissionAttribute("", "system", "roles")]
        [HttpPost]
        public string SetUserRoles(int userID, int[] roleIDS)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.SysUsersBLL.SetUserRoles(userID,roleIDS);
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }
        [ParentPermissionAttribute("", "system", "roles")]
        [HttpPost]
        public string GetUserRoles(int userID)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.SysUsersBLL.GetUserRoles(userID);
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }
        [ParentPermissionAttribute("", "system", "roles")]
        [HttpGet]
        public string GetUserInfo() {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.SysUsersBLL.GetUserInfo(_userId);
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }
        #endregion

        #region 角色管理
        public IActionResult Roles()
        {
            return View();
        }
        [ParentPermissionAttribute("", "system", "roles")]
        [HttpGet]
        public string GetRoleList(Sys_Roles data)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.SysRolesBLL.GetRoleList(data);
            var list = BLL.SysModuleBLL.GetModuleList();
            return Newtonsoft.Json.JsonConvert.SerializeObject(new { rtnMsg ,list});
        }
        [ParentPermissionAttribute("", "system", "roles")]
        [HttpPost]
        public string SaveRole(Sys_Roles data)
        {
            RtnMessage rtnMsg = new RtnMessage();
            data.UserID = _userId;
            rtnMsg = BLL.SysRolesBLL.Save(data);
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }
        [ParentPermissionAttribute("", "system", "roles")]
        [HttpPost]
        public string DeleteRoles(List<Sys_Roles> list)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.SysRolesBLL.DeleteRoles(list);
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }
        [ParentPermissionAttribute("", "system", "roles")]
        [HttpPost]
        public string CheckRoleName(string rolename)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.SysRolesBLL.CheckRoleName(rolename);
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }
        #endregion

        #region 菜单管理

        [ParentPermissionAttribute("", "system", "roles")]
        [HttpPost]
        public string SetRoleMenu(int roleID, int[] menuids)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.SysRolesBLL.SetRoleMenu(roleID,menuids);
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }
        #endregion
        #region 菜单权限V2.0
        public ActionResult Module()
        {
            return View();
        }
        [ParentPermissionAttribute("", "system", "roles")]
        [HttpGet]
        public string GetModuleList()
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.SysModuleBLL.GetModuleList();
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }
        [ParentPermissionAttribute("", "system", "roles")]
        [HttpPost]
        public string GetModuleListByRole(int roleId)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.SysModuleBLL.GetModuleListByRole(roleId);
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }
        #endregion


    }

}