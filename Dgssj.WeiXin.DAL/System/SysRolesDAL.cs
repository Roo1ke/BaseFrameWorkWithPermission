using Dgssj.WeiXin.Model;
using MySql.Data.MySqlClient;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dgssj.WeiXin.DAL
{
    public class SysRolesDAL
    {
        private static readonly string ConnString = SysConfig.DefaultConnection;
        public static RtnMessage GetRoleList(Sys_Roles roles)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;
            List<Sys_Roles> list = new List<Sys_Roles>();
            try
            {
                using (var db = new Database(new MySqlConnection(ConnString)))
                {
                    Sql sql = Sql.Builder;
                    sql.Append("SELECT a.*,b.RealName UserRealName FROM Sys_Roles a left join  sys_users b on a.UserId=b.ID WHERE a.Status <>99");
                    if (!string.IsNullOrEmpty(roles.RoleName))
                        sql.Append($" AND a.RoleName like '%{roles.RoleName}%'");
                    list = db.Fetch<Sys_Roles>(sql);
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
        public static RtnMessage SaveRoles(Sys_Roles roles)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;
            try
            {
                using (var db = new Database(new MySqlConnection(ConnString)))
                {
                    if (roles.ID == 0)
                    {
                        roles.CreateTime = DateTime.Now;
                        if (!(Convert.ToInt32(db.Insert("sys_roles", "ID", roles)) > 0))
                        {
                            rtnMsg.Code = 1;
                        }
                    }
                    else
                    {
                        if (!(Convert.ToInt32(db.Save(roles)) > 0))
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
        public static RtnMessage DeleteRoles(List<Sys_Roles> list)
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
                            db.Update<Sys_Roles>($"SET Status=99 WHERE ID={item.ID}");
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
        public static RtnMessage CheckRoleName(string rolename)
        {
            RtnMessage rtnMsg = new RtnMessage() { Code = 0 };
            try
            {
                using (var db = new Database(new MySqlConnection(ConnString)))
                {
                    var users = db.FirstOrDefault<Sys_Roles>($"WHERE RoleName='{rolename}' And Status<>99");
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

        public static RtnMessage SetRoleMenu(int roleID, int[] menuids)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;
            using (var db = new Database(new MySqlConnection(ConnString)))
            {
                try
                {
                    using (var scope = db.GetTransaction())
                    {
                        db.Delete<Sys_Role_Menu>($"WHERE RoleID={roleID}");
                        foreach (var item in menuids)
                        {
                            Sys_Role_Menu entity = new Sys_Role_Menu()
                            {
                                RoleID = roleID,
                                MenuID=item
                            };
                            db.Insert("Sys_Role_Menu", "ID", entity);
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
    }
}
