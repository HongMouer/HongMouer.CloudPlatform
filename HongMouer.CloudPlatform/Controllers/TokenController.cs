﻿using Microsoft.AspNetCore.Mvc;

namespace HongMouer.CloudPlatform.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        public static string CookieName { get; set; }

        [HttpGet]
        public IActionResult Authorize()
        {
            var token = HttpContext.Request.Cookies[CookieName];

            return Ok(new { token });
        }
    }
}
