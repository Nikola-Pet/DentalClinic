using Dental.Data;
using Dental.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dental.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthenticate _authenticate;



        public HomeController(ILogger<HomeController> logger, IAuthenticate authenticate)
        {
            _logger = logger;
            _authenticate = authenticate;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            string rola = _authenticate.ValidateRoleJwtToken(token);
            if (rola == "Patient")
            {
                return View();
            }

            return BadRequest(new { message = "incorect" });
        }


       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
