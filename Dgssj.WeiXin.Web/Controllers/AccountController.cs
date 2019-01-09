using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Dgssj.WeiXin.Web.Models;
using Dgssj.WeiXin.Web.Models.AccountViewModels;
using Dgssj.WeiXin.Model;
using Dgssj.WeiXin.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;

namespace Dgssj.WeiXin.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {

        private readonly ILogger _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            RtnMessage rtnMsg = new RtnMessage();
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                rtnMsg = BLL.SysUsersBLL.CheckUserLogin(model.UserName, model.Password);
                if (rtnMsg.Code == 0)
                {
                    //储存用户信息
                    List<Claim> claims = new List<Claim>();
                    //var res = DAL.SysMenusDAL.GetMenuByUser(rtnMsg.Data.ID);
                    var res = BLL.SysModuleBLL.GetModuleListByUser(rtnMsg.Data.ID);
                    UserClaims _userClaims = new UserClaims
                    {
                        userId = rtnMsg.Data.ID,
                        menusList = res.Data,
                        users = rtnMsg.Data,
                    };
                    claims.Add(new Claim(ClaimTypes.AuthorizationDecision, JsonConvert.SerializeObject(_userClaims)));
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, rtnMsg.Data.RealName));
                    identity.AddClaims(claims);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    //跳转到系统首页
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError(nameof(model.Password), rtnMsg.Message);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
