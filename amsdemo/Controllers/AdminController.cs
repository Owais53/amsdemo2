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
            tblUser check = db.tblUsers.Where(a => a.UserName == viewModel.UserName).FirstOrDefault<tblUser>();

            if(check == null)
            {
                if (ModelState.IsValid)
                {
                    var users = new tblUser()
                    {
                        UserName = viewModel.UserName,
                        Password = viewModel.Password,
                        EmployeeId = viewModel.EmployeeId,
                        DepartmentId = viewModel.DepartmentId,

                    };
                    db.tblUsers.Add(users);
                    db.SaveChanges();

                    if (users != null)
                    {                        
                        List<object> lst = new List<object>();
                        lst.Add(users.UserId);
                        lst.Add(viewModel.EmployeeId);                       
                        object[] item = lst.ToArray();
                        int output = db.Database.ExecuteSqlCommand("Update tblEmployee set UserId=@p0 where EmployeeId=@p1", item);
                        if (output > 0)                         
                        {
                            TempData["SuccessMessage"] = "User Created";
                        }
                    }
                }


            }
            else
            {
                TempData["ErrorMessage"] = "User with Name " + viewModel.UserName + " already Exists";
            }
            return View();

        }
        public ActionResult GetcreatedUserList()
        {

            var Data = (from users in db.tblUsers
                        join dep in db.tblDepartments on users.DepartmentId equals dep.DepartmentId
                        join emp in db.tblEmployees on users.EmployeeId equals emp.EmployeeId
                        where emp.UserId != null
                        select new
                        {
                            users.UserId,
                            users.UserName,
                            dep.DepartmentName
                        }).ToList();

            return Json(new { data = Data }, JsonRequestBehavior.AllowGet);

 
        }
        public ActionResult Modaltest()
        {
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
        public ActionResult CreateUsers(tblUser viewmodel)
        {
            var empid = db.tblUsers.Where(x => x.EmployeeId == viewmodel.EmployeeId).FirstOrDefault();
            var check = db.tblUsers.Where(a => a.UserName == viewmodel.UserName).FirstOrDefault();
            if (empid == null)
            {

                if (viewmodel.DepartmentId == null && viewmodel.EmployeeId == null)
                {
                    TempData["ErrorMessage1"] = "Please Select from Employee List to Create Users";
                }
                
               else if (check == null)
                {

                    if (ModelState.IsValid)
                    {
                        
                            var users = new tblUser()
                            {
                                UserName = viewmodel.UserName,
                                Password = viewmodel.Password,
                                EmployeeId = viewmodel.EmployeeId,
                                DepartmentId = viewmodel.DepartmentId,

                            };
                            db.tblUsers.Add(users);
                            db.SaveChanges();

                            if (users != null)
                            {
                                List<object> lst = new List<object>();
                                lst.Add(users.UserId);
                                lst.Add(viewmodel.EmployeeId);
                                object[] item = lst.ToArray();
                                int output = db.Database.ExecuteSqlCommand("Update tblEmployee set UserId=@p0 where EmployeeId=@p1", item);
                                if (output > 0)
                                {
                                    TempData["SuccessMessage1"] = "User Created";
                                }
                            }
                        }
                    


                }

                else if(check != null)
                {
                    TempData["ErrorMessage1"] = "User with Name " + viewmodel.UserName + " already Exists";
                }
            }
            else
            {
                TempData["ErrorMessage1"] = "User already Exists.Please Select from Employee List to Create Users";
            }


            return View(viewmodel);
        }
      public ActionResult EmployeeList()
        {
            return View();
        }
        public ActionResult UserforroleList()
        {
            var Data = (from user in db.tblUsers
                        join emp in db.tblEmployees on user.EmployeeId equals emp.EmployeeId
                        join dep in db.tblDepartments on user.DepartmentId equals dep.DepartmentId
                        where emp.UserId != null
                        select new
                        {
                            user.UserId,
                            user.UserName,
                            emp.Position,
                            dep.DepartmentName

                        }).ToList();

            return Json(new { data = Data }, JsonRequestBehavior.AllowGet);
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
        public ActionResult UserManagement()
        {
            var rolelist = db.tblRoles.ToList();
            SelectList list = new SelectList(rolelist, "Id", "RoleName");
            ViewBag.getrolelist = list;

            return View();
        }


        [HttpPost]
        public ActionResult CreateUserPartial(tblUser user)
        {
            var empid = db.tblUsers.Where(x => x.EmployeeId == user.EmployeeId).FirstOrDefault();
            tblUser check = db.tblUsers.Where(a => a.UserName == user.UserName).FirstOrDefault<tblUser>();
            if (empid == null)
            {


                if (user.DepartmentId == null && user.EmployeeId == null)
                {
                    TempData["ErrorMessage1"] = "Please Select from Employee List to Create Users";
                }
                else if (check == null)
                {
                    if (ModelState.IsValid)
                    {
                        var users = new tblUser()
                        {
                            UserName = user.UserName,
                            Password = user.Password,
                            EmployeeId = user.EmployeeId,
                            DepartmentId = user.DepartmentId,

                        };
                        db.tblUsers.Add(users);
                        db.SaveChanges();

                        if (users != null)
                        {
                            List<object> lst = new List<object>();
                            lst.Add(users.UserId);
                            lst.Add(user.EmployeeId);
                            object[] item = lst.ToArray();
                            int output = db.Database.ExecuteSqlCommand("Update tblEmployee set UserId=@p0 where EmployeeId=@p1", item);
                            if (output > 0)
                            {
                                TempData["SuccessMessage1"] = "User Created";
                            }
                        }
                    }
                }

                else if (check != null)
                {
                    TempData["ErrorMessage1"] = "User with Name " + user.UserName + " already Exists";
                }
            }
            else
            {
                TempData["ErrorMessage1"] = "User already Exists.Please Select from Employee List to Create Users";
            }

            return View("UserManagement");
        }
        [HttpGet]
        public ActionResult AssignrolesPartial()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AssignrolesPartial(tblUser user,FormCollection form)
        {
            
            if (ModelState.IsValid)
            {
                var chkadmin = form["getadmin"];
                if(chkadmin == null)
                {
                    chkadmin = "0";
                }
                List<object> lst = new List<object>();
                lst.Add(user.RoleId);
                lst.Add(Convert.ToInt32(chkadmin));
                lst.Add(user.UserId);
                object[] item = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("Update tblUsers set RoleId=@p0,AdminId=@p1,IsActive=1 where UserId=@p2", item);
                if (output > 0)
                {
                    TempData["SuccessMessage1"] = "User Created";
                }
            }
            var rolelist = db.tblRoles.ToList();
            SelectList list = new SelectList(rolelist, "Id", "RoleName");
            ViewBag.getrolelist = list;

            return View();
        }
    }
}