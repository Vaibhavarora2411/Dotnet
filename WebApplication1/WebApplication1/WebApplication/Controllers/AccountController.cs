using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using System.Web.Security;

namespace WebApplication.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {
            using (var context = new OfficeEntities())
            {
                bool isValid = context.User.Any(x => x.UserName == model.UserName && x.Password == model.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Events", "Employees");
                }

                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }
            
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User model)
        {
            using(var context = new OfficeEntities())
            {
                context.User.Add(model);
                context.SaveChanges();

            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}