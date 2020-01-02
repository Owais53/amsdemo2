using amsdemo.Infrastructure;
using amsdemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace amsdemo.Controllers
{
    
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tblUser model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new SqlContext())
                {
                    tblUser user = context.tblUsers
                                       .Where(u => u.UserName == model.UserName && u.Password == model.Password)
                                       .FirstOrDefault();

                    if (user != null)
                    {
                        Session["UserName"] = user.UserName;
                        Session["UserId"] = user.UserId;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {

                        ModelState.AddModelError("", "Invalid User Name or Password");
                        return View(model);
                    }

                }
            }
            else
            {
                return View(model);
            }

        }


        public ActionResult Logout()
        {
            Session.Abandon();
            Session["UserName"] = string.Empty;
            Session["UserId"] = string.Empty;
            return RedirectToAction("Login", "Account");
        }

    }
}