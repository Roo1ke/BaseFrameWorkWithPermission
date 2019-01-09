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
    public class BaseInfoController : Controller
    {
        #region 字典管理
        public IActionResult Dict()
        {
            return View();
        }

        [ParentPermissionAttribute("", "baseinfo", "dict")]
        [HttpGet]
        public string GetDictList(base_dict data)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.BaseDictBLL.GetDictList(data);
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }
        [ParentPermissionAttribute("", "baseinfo", "dict")]
        [HttpPost]
        public string SaveDict(base_dict data)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.BaseDictBLL.Save(data);
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }
        [ParentPermissionAttribute("", "baseinfo", "dict")]
        [HttpPost]
        public string DeleteDict(List<base_dict> list)
        {
            RtnMessage rtnMsg = new RtnMessage();
            rtnMsg = BLL.BaseDictBLL.DeleteDict(list);
            return Newtonsoft.Json.JsonConvert.SerializeObject(rtnMsg);
        }

        #endregion
    }
}