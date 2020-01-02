using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using amsdemo.Infrastructure;
using amsdemo.Models;

namespace amsdemo.Controllers
{
    public class AdminController : Controller
    {
        private readonly SqlContext db = new SqlContext();

      



        [HttpGet]
        public ActionResult CreateUsers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUsers(tblUser user)
        {
            
            var adduser = (from u in db.tblUsers
                            join a in db.tblDepartments on u.DepartmentId equals a.DepartmentId
                            where u.UserName == user.UserName && u.UserId == user.UserId
                            select new
                            {
                                u.UserName,
                                u.DepartmentId,
                                u.Password
                            }).FirstOrDefault();
            if (adduser == null)
            {
                if (ModelState.IsValid)
                {
                    var useradd = new tblUser()
                    {
                        
                    };

                    user.UserName = adduser.UserName;
                    user.DepartmentId = adduser.DepartmentId;
                    user.Password = adduser.Password;
                    db.tblUsers.Add(user);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "User Created";
                    return RedirectToAction("UserList");
                }
            }
            else
            {
                TempData["ErrorMessage"] = " User already exists";

            }

            return View(user);
        }
        public ActionResult UserList()
        {
            return View();
        }
        public ActionResult UserRolesList()
        {         
            return View();
        }
        public ActionResult GetUsers()
        {
            var users = db.spGetAllUsers();
            return Json(new { data = users }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Assignroles(tblUser user)
        {
            

            return View();
        }
    }
}