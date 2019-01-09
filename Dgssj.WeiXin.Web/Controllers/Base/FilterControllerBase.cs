using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dgssj.WeiXin.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Dgssj.WeiXin.Web.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private UserClaims _Users;
        private void Init() {
            if (User.HasClaim(e => e.Type == ClaimTypes.AuthorizationDecision))
            {
                _Users = JsonConvert.DeserializeObject<UserClaims>(User.FindFirst(ClaimTypes.AuthorizationDecision).Value);
            }
            else {
                _Users = null;
            }
        }

        public int _userId {
            get
            {
                Init();
                return _Users == null ? 0 : _Users.userId;
            }
        }
    }
}