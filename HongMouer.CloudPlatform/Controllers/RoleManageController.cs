using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HongMouer.CloudPlatform.Controllers
{
    public class RoleManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}