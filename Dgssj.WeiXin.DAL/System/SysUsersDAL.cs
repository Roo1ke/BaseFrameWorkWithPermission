using Dgssj.WeiXin.Common;
using Dgssj.WeiXin.Model;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dgssj.WeiXin.DAL
{
    public class SysUsersDAL
    {
        private static readonly string ConnString = SysConfig.DefaultConnection;
        private static readonly string Default_Pwd = SysConfig.DefaultPassword;
        private static readonly string Key = SysConfig.Key;
        public static RtnMessage GetUserList(Sys_Users users)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;
            List<Sys_Users> list = new List<Sys_Users>();
            try
            {
                using (var db = new Database(new MySqlConnection(ConnString)))
                {

                    Sql sql = Sql.Builder;
                    sql.Append("SELECT * FROM Sys_Users WHERE Status <>99");
                    if (!string.IsNullOrEmpty(users.RealName))
                        sql.Append($" AND RealName like '%{users.RealName}%'");
                    list = db.Fetch<Sys_Users>(sql);
                    rtnMsg.Data = list;
                }
            }
            catch (Exception ex)
            {
                rtnMsg.Code = -1;
                rtnMsg.Message = ex.ToString();
                return rtnMsg;
            }
            return rtnMsg;
        }

        public static RtnMessage SaveUsers(Sys_Users users)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;
            try
            {
                using (var db = new Database(new MySqlConnection(ConnString)))
                {
                    if (users.ID == 0)
                    {
                        users.PassWord = StringHelper.Encrypt(SysConfig.DefaultPassword, SysConfig.Key);
                        users.CreateTime = DateTime.Now;
                        users.EditTime = DateTime.Now;
                        if (!(Convert.ToInt32(db.Insert("sys_users", "ID", users)) > 0))
                        {
                            rtnMsg.Code = 1;
                        }
                    }
                    else
                    {
                        users.EditTime = DateTime.Now;
                        if (!(Convert.ToInt32(db.Save(users)) > 0))
                        {
                            rtnMsg.Code = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rtnMsg.Code = -1;
                rtnMsg.Message = ex.ToString();
                return rtnMsg;
            }
            return rtnMsg;
        }

        public static RtnMessage SaveUserAtIndex(Sys_Users users)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;
            try
            {
                using (var db = new Database(new MySqlConnection(ConnString)))
                {
                    users.EditTime = DateTime.Now;
                    string sql = $"SET RealName='{users.RealName}',Sex={users.Sex},Phone='{users.Phone}',EditTime='{users.EditTime}' WHERE ID={users.ID}";
                    if (!(Convert.ToInt32(db.Update<Sys_Users>(sql)) > 0))
                    {
                        rtnMsg.Code = 1;
                    }

                }
            }
            catch (Exception ex)
            {
                rtnMsg.Code = -1;
                rtnMsg.Message = ex.ToString();
                return rtnMsg;
            }
            return rtnMsg;
        }

        public static RtnMessage GetUserInfo(int userid)
        {
            RtnMessage rtnMsg = new RtnMessage() { Code = 0 };
            try
            {
                using (var db = new Database(new MySqlConnection(ConnString)))
                {
                    var users = db.FirstOrDefault<Sys_Users>($"WHERE id='{userid}' And Status<>99");
                    rtnMsg.Data = users;
                }
            }
            catch (Exception ex)
            {
                rtnMsg.Code = -1;
                rtnMsg.Message = ex.Message;
                return rtnMsg;
            }
            return rtnMsg;
        }


        public static RtnMessage ModifyPassword(int userID, string password,string newpwd)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;
            try
            {
                using (var db = new Database(new MySqlConnection(ConnString)))
                {
                    password = StringHelper.Encrypt(password, SysConfig.Key);
                    newpwd= StringHelper.Encrypt(newpwd, SysConfig.Key);
                    if (db.Update<Sys_Users>($"SET PassWord='{newpwd}' WHERE ID={userID} AND PassWord='{password}'") <= 0)
                    {
                        rtnMsg.Code = 1;
                        rtnMsg.Message = "原密码错误";
                    }
                }
            }
            catch (Exception ex)
            {
                rtnMsg.Code = -1;
                rtnMsg.Message = ex.Message;
                return rtnMsg;
            }
            return rtnMsg;
        }

        public static RtnMessage ResetPassword(int userID)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;
            try
            {
                using (var db = new Database(new MySqlConnection(ConnString)))
                {
                    string pwd = StringHelper.Encrypt(SysConfig.DefaultPassword, SysConfig.Key);
                    if (db.Update<Sys_Users>($"SET PassWord='{pwd}' WHERE ID={userID}") <= 0)
                    {
                        rtnMsg.Code = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                rtnMsg.Code = -1;
                rtnMsg.Message = ex.Message;
                return rtnMsg;
            }
            return rtnMsg;
        }

        public static RtnMessage DeleteUsers(List<Sys_Users> list)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;

            using (var db = new Database(new MySqlConnection(ConnString)))
            {
                using (var scope = db.GetTransaction())
                {
                    try
                    {
                        foreach (var item in list)
                        {
                            db.Update<Sys_Users>($"SET Status=99 WHERE ID={item.ID}");
                        }
                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        db.AbortTransaction();
                        rtnMsg.Code = -1;
                        rtnMsg.Message = ex.Message;
                        return rtnMsg;
                    }
                }
            }
            return rtnMsg;
        }

        public static RtnMessage CheckUserLogin(string username, string password)
        {
            RtnMessage rtnMsg = new RtnMessage() { Code = 0 };
            try
            {
                using (var db = new Database(new MySqlConnection(ConnString)))
                {
                    password = StringHelper.Encrypt(password, SysConfig.Key);
                    var users = db.FirstOrDefault<Sys_Users>($"WHERE UserName='{username}' AND PassWord='{password}' And Status<>99");
                    if (users == null)
                    {
                        rtnMsg.Code = 1;
                        rtnMsg.Message = "用户名或密码错误";
                    }
                    else
                    {
                        rtnMsg.Data = users;
                        if (users.Status == -1)
                        {
                            rtnMsg.Code = 1;
                            rtnMsg.Message = "该用户未启用，请联系管理员";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                rtnMsg.Code = -1;
                rtnMsg.Message = ex.Message;
                return rtnMsg;
            }
            return rtnMsg;
        }

        public static RtnMessage CheckUserName(string username)
        {
            RtnMessage rtnMsg = new RtnMessage() { Code = 0 };
            try
            {
                using (var db = new Database(new MySqlConnection(ConnString)))
                {
                    var users = db.FirstOrDefault<Sys_Users>($"WHERE UserName='{username}' And Status<>99");
                    if (users != null)
                    {
                        rtnMsg.Code = 1;
                        rtnMsg.Message = "此用户名已存在";
                    }
                }
            }
            catch (Exception ex)
            {
                rtnMsg.Code = -1;
                rtnMsg.Message = ex.Message;
                return rtnMsg;
            }
            return rtnMsg;
        }

        /// <summary>
        /// 给用户赋予角色
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleIDS"></param>
        /// <returns></returns>
        public static RtnMessage SetUserRoles(int userID, int[] roleIDS)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;
            using (var db = new Database(new MySqlConnection(ConnString)))
            {
                try
                {
                    using (var scope = db.GetTransaction())
                    {
                        db.Delete<Sys_User_Role>($"WHERE UserID={userID}");
                        foreach (var item in roleIDS)
                        {
                            Sys_User_Role entity = new Sys_User_Role()
                            {
                                UserID = userID,
                                RoleID = item
                            };
                            db.Insert("Sys_User_Role", "ID", entity);
                        }
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    rtnMsg.Code = -1;
                    db.AbortTransaction();
                    rtnMsg.Message = ex.Message;
                    return rtnMsg;
                }
            }
            return rtnMsg;
        }

        /// <summary>
        /// 获取用户的角色
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static RtnMessage GetUserRoles(int userID)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;
            try
            {
                using (var db = new Database(new MySqlConnection(ConnString)))
                {
                    var list = db.Fetch<int>($"Select RoleID from Sys_User_Role WHERE UserID={userID}");
                    rtnMsg.Data = list;
                }
            }
            catch (Exception ex)
            {
                rtnMsg.Code = -1;
                rtnMsg.Message = ex.Message;
                return rtnMsg;
            }
            return rtnMsg;
        }
    }
}

