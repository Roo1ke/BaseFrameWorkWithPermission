using Dgssj.WeiXin.Model;
using MySql.Data.MySqlClient;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dgssj.WeiXin.DAL
{
    public class BaseDictDAL
    {
        private static readonly string ConnString = SysConfig.DefaultConnection;
        public static RtnMessage GetDictList(base_dict info)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;
            List<base_dict> list = new List<base_dict>();
            try
            {
                using (var db = new Database(new MySqlConnection(ConnString)))
                {
                    Sql sql = Sql.Builder;
                    sql.Append("SELECT a.* FROM base_dict a WHERE  a.State <>-1");
                    if (!string.IsNullOrEmpty(info.Name))
                        sql.Append($" AND a.Name like '%{info.Name}%'");
                    if (info.typeID != 0)
                        sql.Append($" AND a.TypeID ={info.typeID}");
                    list = db.Fetch<base_dict>(sql);
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
        public static RtnMessage Save(base_dict info)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;
            using (var db = new Database(new MySqlConnection(ConnString)))
            {
                try
                {
                    if (info.ID == 0)
                    {

                        if (!(Convert.ToInt32(db.Insert("base_dict", "ID", info)) > 0))
                        {
                            rtnMsg.Code = 1;
                        }
                    }
                    else
                    {
                        if (!(Convert.ToInt32(db.Save(info)) > 0))
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
            }
            return rtnMsg;

        }
        public static RtnMessage DeleteDict(List<base_dict> list)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg.Code = 0;
            using (var db = new Database(new MySqlConnection(ConnString)))
            {
                    try
                    {
                        foreach (var item in list)
                        {
                            db.Update<base_dict>($"SET State=-1 WHERE ID={item.ID}");
                        }
                    }
                    catch (Exception ex)
                    {
                        rtnMsg.Code = -1;
                        rtnMsg.Message = ex.Message;
                        return rtnMsg;
                    }
            }
            return rtnMsg;
        }
    }
}
