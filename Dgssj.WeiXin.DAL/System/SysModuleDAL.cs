using Dgssj.WeiXin.Model;
using MySql.Data.MySqlClient;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dgssj.WeiXin.DAL
{
    public class SysModuleDAL
    {
        //private static Database db = new Database(new MySqlConnection(SysConfig.DefaultConnection));


        /// <summary>
        /// 检验是否有访问权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="area"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool CheckAdminPermit(int userId = 0, string area = "", string controller = "", string action = "")
        {

            var modules = (List<SysModule>)GetModuleByUser(userId).Data;
            area = area?.ToLower() ?? string.Empty;
            controller = controller?.ToLower() ?? string.Empty;
            action = action?.ToLower() ?? string.Empty;
            return modules.Any(e =>
                                e.Area.ToLower() == area
                                && e.Controller.ToLower() == controller
                                && e.Action.ToLower() == action);
        }

        /// <summary>
        /// 保存模块数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static RtnMessage SaveSysModule(SysModule data)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;
            try
            {
                using (var db = new Database(new MySqlConnection(SysConfig.DefaultConnection)))
                {
                    if (data.Id == 0)
                    {
                        data.CreateTime = DateTime.Now;
                        if (!(Convert.ToInt32(db.Insert("Sys_Module", "ID", data)) > 0))
                        {
                            rtnMsg.Code = 1;
                        }
                    }
                    else
                    {
                        if (!(Convert.ToInt32(db.Save(data)) > 0))
                        {
                            rtnMsg.Code = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rtnMsg.Code = -1;
                Common.LoggerHelper.Error(ex.ToString());
                return rtnMsg;
            }
            return rtnMsg;
        }

        public static RtnMessage DeleteModule(SysModule data)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;

            //using (var db = new Database(new MySqlConnection(ConnString)))
            //{
            //using (var scope = db.GetTransaction())
            //{
            //    try
            //    {
            //        foreach (var item in list)
            //        {
            //            db.Update<Sys_Module>($"SET Status=99 WHERE ID={item.ID}");
            //        }
            //        scope.Complete();
            //    }
            //    catch (Exception ex)
            //    {
            //        db.AbortTransaction();
            //        rtnMsg.Code = -1;
            //        rtnMsg.Message = ex.Message;
            //        return rtnMsg;
            //    }
            //}
            //}
            return rtnMsg;
        }
        public static RtnMessage CheckRoleName(string rolename)
        {
            RtnMessage rtnMsg = new RtnMessage() { Code = 0 };
            try
            {
                using (var db = new Database(new MySqlConnection(SysConfig.DefaultConnection)))
                {
                    var users = db.FirstOrDefault<SysModule>($"WHERE RoleName='{rolename}' And Status<>99");
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

        public static RtnMessage GetModuleList()
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;
            List<SysModule> menuList = new List<SysModule>();
            List<MenusTreeModel> treeList = new List<MenusTreeModel>();
            try
            {
                using (var db = new Database(new MySqlConnection(SysConfig.DefaultConnection)))
                {
                    menuList = db.Fetch<SysModule>($"SELECT * FROM sys_module");
                    #region 构造树形结构
                    var One_list = menuList.Where(e => e.ParentId == 0).ToList();
                    foreach (var item in One_list)
                    {
                        MenusTreeModel tree = new MenusTreeModel()
                        {
                            id = item.Id,
                            label = item.ModuleName,
                            children = new List<MenusTreeModel>()
                        };
                        var two_list = menuList.Where(e => e.ParentId == item.Id).ToList();
                        foreach (var _item in two_list)
                        {
                            MenusTreeModel _tree = new MenusTreeModel()
                            {
                                id = _item.Id,
                                label = _item.ModuleName,
                            };
                            tree.children.Add(_tree);
                        }
                        treeList.Add(tree);
                        //}
                        #endregion

                        rtnMsg.Data = treeList;
                    }
                }
            }
            catch (Exception ex)
            {
                rtnMsg.Code = -1;
                Common.LoggerHelper.Error(ex.ToString());
                return rtnMsg;
            }
            return rtnMsg;
        }

        public static RtnMessage GetModuleByUser(int userId)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;
            try
            {
                using (var db = new Database(new MySqlConnection(SysConfig.DefaultConnection)))
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append($"select DISTINCT a.* from sys_module a LEFT JOIN sys_role_menu b on ");
                    sql.Append($"a.ID=b.MenuID left join sys_user_role c on b.RoleID=c.RoleID where c.UserID={userId}");
                    sql.Append($" UNION select * from  sys_module  where ID IN");
                    sql.Append($" (select a.ParentId from sys_module a LEFT JOIN sys_role_menu b on ");
                    sql.Append($" a.ID=b.MenuID left join sys_user_role c on b.RoleID=c.RoleID where c.UserID={userId} ) ");
                    var list = db.Fetch<SysModule>(sql.ToString());
                    rtnMsg.Data = list;
                }
            }
            catch (Exception ex)
            {
                rtnMsg.Code = -1;
                Common.LoggerHelper.Error(ex.ToString());
                return rtnMsg;
            }
            return rtnMsg;
        }

        public static RtnMessage GetModuleListByRole(int roleId)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;
            try
            {
                using (var db = new Database(new MySqlConnection(SysConfig.DefaultConnection)))
                {
                    List<int> list = db.Fetch<int>($"SELECT a.MenuID FROM Sys_Role_Menu a left join sys_module b on a.MenuID=b.Id  where b.ParentId<>0 and  RoleID={roleId} ");
                    rtnMsg.Data = list;
                }
            }
            catch (Exception ex)
            {
                rtnMsg.Code = -1;
                Common.LoggerHelper.Error(ex.ToString());
                return rtnMsg;
            }
            return rtnMsg;
        }
    }
}
