using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace MachineKeyAlternative.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() {
            return View();
        }

        [Authorize]
        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;

            var props = new AuthenticationProperties { RedirectUri = returnUrl };
            var identity = new ClaimsIdentity(new []
            {
                new Claim("sub", "d38a7b49-3cab-40a5-8bd4-1801a4a983d3"), 
                new Claim("email", "joe.user@example.com")
            }, CookieAuthenticationDefaults.AuthenticationType);

            HttpContext.GetOwinContext().Authentication.SignIn(props, identity);
            return Redirect(returnUrl);
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return RedirectToAction("Index");
        }
    }
}