using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HongMouer.EHR.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HongMouer.CloudPlatform.Controllers
{
    public class LoginController : Controller
    {
        private const string ReturnUrlKey = "ReturnUrl";

        private readonly IUserAuthorize _UserAuthorize;

        public LoginController(IUserAuthorize userAuthorize)
        {
            _UserAuthorize = userAuthorize;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region 登录
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (!returnUrl.Contains("http:")) { returnUrl = Request.Scheme+"://"+ Request.Host.Value + returnUrl; }
            if (!string.IsNullOrEmpty(returnUrl))
                HttpContext.Response.Cookies.Append(ReturnUrlKey, returnUrl, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Lax
                });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserInfo loginModel)
        {
           // var user = new UserInfo();
            //if (user.Vaild(loginModel.UserName, loginModel.Password))
            return await SignIn(loginModel);

            return Ok(new { Msg = "登录失败", Success = false });
        }
        #endregion

        #region 注销
        public async Task<IActionResult> Logout()
        {
            await SignOut();
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region 私有方法
        private async Task<IActionResult> SignIn(UserInfo user)
        {
            //var claims = new List<Claim>
            //{
            //    new Claim(JwtClaimTypes.Id,user.UserId),
            //    new Claim(JwtClaimTypes.Name,user.UserName),
            //    new Claim(JwtClaimTypes.NickName,user.RealName),
            //};

            //https://blog.csdn.net/weixin_42331508/article/details/108105091
            var claims = new List<Claim>
            {
                //new Claim("UserId" ,user.UserId.ToString()),
                //new Claim("UserName",user.UserName),
                //new Claim("NickName",user.NickName),
                new Claim("UserNum",user.UserNum),
            };

            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));


            var returnUrl = HttpContext.Request.Cookies[ReturnUrlKey];
            await HttpContext.SignInAsync(userPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = returnUrl
                });

            HttpContext.Response.Cookies.Delete(ReturnUrlKey);

            return Ok(new { ReturnUrl = returnUrl ?? "/", Success = true });
        }

        private async Task SignOut()
        {
            await HttpContext.SignOutAsync();
        }
        #endregion
    }
}