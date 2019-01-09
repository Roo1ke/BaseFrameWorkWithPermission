using Dgssj.WeiXin.Model;
using System;
using System.Collections.Generic;

namespace Dgssj.WeiXin.BLL
{
    public class SysUsersBLL
    {
        public static RtnMessage GetUserList(Sys_Users users)
        {
            return DAL.SysUsersDAL.GetUserList(users);
        }

        public static RtnMessage Save(Sys_Users users)
        {
            return DAL.SysUsersDAL.SaveUsers(users);
        }

        public static RtnMessage SaveUserAtIndex(Sys_Users users)
        {
            return DAL.SysUsersDAL.SaveUserAtIndex(users);
        }

        public static RtnMessage GetUserInfo(int userid)
        {
            return DAL.SysUsersDAL.GetUserInfo(userid);
        }

        public static RtnMessage ModifyPassword(int userID, string pwd,string newpwd)
        {
            return DAL.SysUsersDAL.ModifyPassword(userID, pwd,newpwd);
        }

        public static RtnMessage ResetPassword(int userID)
        {
            return DAL.SysUsersDAL.ResetPassword(userID);
        }

        public static RtnMessage DeleteUsers(List<Sys_Users> list) {
            return DAL.SysUsersDAL.DeleteUsers(list);
        }

        public static RtnMessage CheckUserLogin(string username, string password)
        {
            return DAL.SysUsersDAL.CheckUserLogin(username, password);
        }

        public static RtnMessage CheckUserName(string username)
        {
            return DAL.SysUsersDAL.CheckUserName(username);
        }

        public static RtnMessage SetUserRoles(int userID, int[] roleIDS)
        {
            return DAL.SysUsersDAL.SetUserRoles(userID, roleIDS);
        }

        public static RtnMessage GetUserRoles(int userID)
        {
            return DAL.SysUsersDAL.GetUserRoles(userID);
        }
    }
}
