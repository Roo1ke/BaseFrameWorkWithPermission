using Dgssj.WeiXin.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dgssj.WeiXin.BLL
{
    public class BaseDictBLL
    {
        public static RtnMessage GetDictList(base_dict info)
        {
            return DAL.BaseDictDAL.GetDictList(info);
        }
        public static RtnMessage Save(base_dict info)
        {
            return DAL.BaseDictDAL.Save(info);
        }
        public static RtnMessage DeleteDict(List<base_dict> list)
        {
            return DAL.BaseDictDAL.DeleteDict(list);
        }
    }
}
