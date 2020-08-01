using cobamvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace cobamvc.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [ClaimsAuthorize("general_approval:=test")]
        public String Test()
        {
            var s = HttpContext.User;
            var t = User;
            var user = User as ClaimsPrincipal;
            var identity = user.Identity as ClaimsIdentity;
            return "Hello From secret Test, "+ User;
        }
    }
}