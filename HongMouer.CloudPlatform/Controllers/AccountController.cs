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
    public class AccountController : Controller
    {
        private const string ReturnUrlKey = "ReturnUrl";

        private readonly IUserAuthorize _UserAuthorize;

        public AccountController(IUserAuthorize userAuthorize)
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
            //if (!string.IsNullOrEmpty(returnUrl))
            //    HttpContext.Response.Cookies.Append(ReturnUrlKey, returnUrl, new CookieOptions
            //    {
            //        HttpOnly = true,
            //        SameSite = SameSiteMode.Lax
            //    });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserInfo loginModel)
        {
            //HttpContext.Response.StatusCode=
            //HttpContext.Response.Headers.Add("location","http://www.baidu.com");


            //var response = HttpContext.Response;
            //response.StatusCode = StatusCodes.Status302Found;
            //response. = RuleResult.EndResponse;
            //response.Headers["location"] = "https://www.cnblogs.com";

            //HttpContext.Response.Redirect(users.reurl);
            //return View(new { Msg = "登录失败", Success = false,token="sfgasfasaghfgshfgfghshg" });

            // HttpContext.Response.Cookies.Delete(ReturnUrlKey);



            return Ok(new ResponseMessage<UserInfo>(1,new UserInfo(),0,"保存成功!"));// new { Msg = "登录失败", Success = false, token = "sfgasfasaghfgshfgfghshg" }); ; ; ; ;// ;
            //// var user = new UserInfo();
            ////if (user.Vaild(loginModel.UserName, loginModel.Password))
            //// await SignIn(loginModel);


            //var claims = new List<Claim>
            //{
            //    //new Claim("UserId" ,user.UserId.ToString()),
            //    //new Claim("UserName",user.UserName),
            //    //new Claim("NickName",user.NickName),
            //    new Claim("UserNum",loginModel.UserNum),
            //};

            //var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));


            //var returnUrl = HttpContext.Request.Cookies[ReturnUrlKey];
            //await HttpContext.SignInAsync(userPrincipal,
            //    new AuthenticationProperties
            //    {
            //        IsPersistent = true,
            //        RedirectUri = returnUrl
            //    });

            //HttpContext.Response.Cookies.Delete(ReturnUrlKey);

            //return Ok(new { Msg = "登录失败", Success = false });
        }
        #endregion

        #region 注销
        public async Task<IActionResult> Logout()
        {
            await SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        public async Task SignOutAsync()
        {


        }
        #endregion
    }
}