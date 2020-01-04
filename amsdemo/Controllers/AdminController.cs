using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using amsdemo.Infrastructure;
using amsdemo.Models;
using amsdemo.ViewModel;

namespace amsdemo.Controllers
{
    public class AdminController : Controller
    {
        private readonly SqlContext db = new SqlContext();

        [HttpGet]
        public ActionResult EmployeetoUser()
        {

            return View();
            
        }

        [HttpPost]
        public ActionResult EmployeetoUser(EmployeeUserVM viewModel)
        {
            tblUser check = db.tblUsers.Where(a => a.UserName == viewModel.Username).FirstOrDefault<tblUser>();

            if(check == null)
            {
                if (ModelState.IsValid)
                {
                    List<object> lst = new List<object>();
                    lst.Add(viewModel.Username);
                    lst.Add(viewModel.Password);
                    lst.Add(viewModel.EmployeeId);
                    lst.Add(viewModel.DepartmentId);
                    object[] item = lst.ToArray();
                    int output = db.Database.ExecuteSqlCommand("insert into tblUsers(UserName,Password,EmployeeId,DepartmentId) values(@p0,@p1,@p2,@p3)", item);
                    if (output > 0)
                    {
                        List<object> emp = new List<object>();                       
                        emp.Add(viewModel.EmployeeId);
                        object[] item1 = emp.ToArray();
                        int userid = db.Database.ExecuteSqlCommand("select UserId from tblEmployee where EmployeeId=@p0", item1);
                        if (userid >0)
                        {
                            List<object> emp1 = new List<object>();
                            emp1.Add(userid);
                            object[] item2 = emp.ToArray();
                            int output2 = db.Database.ExecuteSqlCommand("insert into tblEmployee(UserId) values(@p0)",item2);
                            if (output2 > 0)
                            {
                                TempData["SuccessMessage"] = "User Created";
                            }
                        }

                        
                    }
                }


            }

            return View();

        }
        public ActionResult GetEmployeeList()
        {

            var Data = (from emp in db.tblEmployees
                        join dep in db.tblDepartments on emp.DepartmentId equals dep.DepartmentId
                        where emp.UserId == null
                        select new
                        {
                            emp.EmployeeId,
                            emp.EmployeeName,
                            emp.DepartmentId,
                            dep.DepartmentName
                        }).ToList();

            return Json(new { data = Data }, JsonRequestBehavior.AllowGet);
        }

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