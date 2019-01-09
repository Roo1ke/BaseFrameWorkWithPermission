using System;
using System.Collections.Generic;
using System.Text;

namespace Dgssj.WeiXin.Model
{
    public class UserClaims
    {
        public int userId { set; get; }
        public List<Sys_Roles> rolesList { set; get; }
        public List<SysModule> menusList { set; get; }
        public Sys_Users users { set; get; }
    }
}
