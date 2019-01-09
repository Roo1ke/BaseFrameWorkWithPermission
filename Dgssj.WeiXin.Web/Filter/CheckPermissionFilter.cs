using Dgssj.WeiXin.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dgssj.WeiXin.Web
{
    public class CheckPermissionFilter : Attribute, IAuthorizationFilter
    {
        private UserClaims _Users;
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext.HttpContext.User.HasClaim(e => e.Type == ClaimTypes.AuthorizationDecision))
            {
                _Users = JsonConvert.DeserializeObject<UserClaims>(filterContext.HttpContext.User.FindFirst(ClaimTypes.AuthorizationDecision).Value);
            }
            else
            {
                filterContext.Result = new RedirectResult("/account/login");
                return;
            }

            //匿名标识 无需验证
            if (filterContext.Filters.Any(e => (e as AllowAnonymous) != null))
                return;

            //对应action方法或者Controller上若存在NonePermissionAttribute标识，即表示为管理员的默认权限,只要登录就有权限
            var isNone = filterContext.Filters.Any(e => (e as NonePermissionAttribute) != null);
            if (isNone)
                return;
            //获取请求的区域，控制器，action名称
            var area = filterContext.RouteData.DataTokens["area"]?.ToString();
            var controller = filterContext.RouteData.Values["controller"]?.ToString();
            var action = filterContext.RouteData.Values["action"]?.ToString();
            var isPermit = false;
            //校验权限
            isPermit = BLL.SysModuleBLL.CheckAdminPermit(_Users.userId, area, controller, action);
            if (isPermit)
                return;
            //此action方法的父辈权限判断，只要有此action对应的父辈权限，皆有权限访问
            var pAttrs = filterContext.Filters.Where(e => (e as ParentPermissionAttribute) != null).ToList();
            if (pAttrs.Count > 0)
            {
                foreach (ParentPermissionAttribute pattr in pAttrs)
                {
                    if (!string.IsNullOrEmpty(pattr.Area))
                        area = pattr.Area;
                    isPermit = BLL.SysModuleBLL.CheckAdminPermit(_Users.userId, area, pattr.Controller, pattr.Action);
                    if (isPermit)
                        return;
                }
            }
            if (!isPermit)
            {
                filterContext.Result = new ContentResult() { Content = "无权限访问" };
                return;
            }
        }
        /// <summary>
        /// 管理员的默认权限
        /// </summary>
        public class NonePermissionAttribute : Attribute, IFilterMetadata { }

        /// <summary>
        /// 匿名验证
        /// </summary>
        public class AllowAnonymous : Attribute, IFilterMetadata { }

        /// <summary>
        /// 长辈权限
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
        public class ParentPermissionAttribute : Attribute, IFilterMetadata
        {
            /// <summary>
            /// 区域
            /// </summary>
            public string Area { get; set; }
            /// <summary>
            /// 控制器
            /// </summary>
            public string Controller { get; set; }
            /// <summary>
            /// Action名称
            /// </summary>
            public string Action { get; set; }

            public ParentPermissionAttribute(string area, string controller, string action)
            {
                this.Area = area;
                this.Controller = controller;
                this.Action = action;
            }

            public ParentPermissionAttribute(string controller, string action)
            {
                this.Controller = controller;
                this.Action = action;
            }
        }
    }
}
