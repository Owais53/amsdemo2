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
                    var users = new tblUser()
                    {
                        UserName = viewModel.Username,
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
                TempData["ErrorMessage"] = "User with Name " + viewModel.Username + " already Exists";
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

      public ActionResult EmployeeList()
        {
            return View();
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