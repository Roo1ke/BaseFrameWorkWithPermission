using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dgssj.WeiXin.Model
{
    public class Sys_Roles
    {
        public int ID { set; get; }
        public int UserID { set; get; }
        public DateTime CreateTime { set; get; }
        public string RoleName { set; get; }
        public string Description { set; get; }
        public int Status { set; get; }
        [ResultColumn]public string UserRealName { set; get; }
    }
}
