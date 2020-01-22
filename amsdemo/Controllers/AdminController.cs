using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using amsdemo.DAL.Interface;
using amsdemo.DAL.Repository;
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
        public ActionResult EmployeetoUser(EmployeeUserVM viewmodel)
        {
            IUserRepository objuserRepository = new UserRepository();
            IEmployeeRepository objemployeeRepository = new EmployeeRepository();


            var empid = objuserRepository.GetAll().Where(x => x.EmployeeId == viewmodel.EmployeeId).FirstOrDefault();
            var check = objuserRepository.GetAll().Where(a => a.UserName == viewmodel.UserName).FirstOrDefault();



            if (empid == null)
            {


                if (check == null && empid == null)
                {

                    var users = objuserRepository.AddUser(viewmodel.UserName, viewmodel.Password, viewmodel.EmployeeId, viewmodel.DepartmentId);
                    objuserRepository.Add(users);
                    objuserRepository.Save();

                    if (users != null)
                    {
                        objemployeeRepository.Update(viewmodel.EmployeeId, users.UserId);
                        objemployeeRepository.Save();
                        TempData["SuccessMessage1"] = "User Created";
                    }

                }
                else
                {
                    TempData["ErrorMessage1"] = "User with Name " + viewmodel.UserName + " already Exists";
                }
            }
            else if (empid != null)
            {
                TempData["ErrorMessage1"] = "User Already Exists with Name " + viewmodel.UserName + " or You have not selected from Employee List to Create Users";
            }
            else if (check != null && empid == null)
            {
                TempData["ErrorMessage1"] = "User with Name " + viewmodel.UserName + " already Exists";
            }
            return View();

        }
        public ActionResult GetcreatedUserList()
        {
            IUserRepository objuserrepository = new UserRepository();
            IDepartmentRepository objdepart = new DepartmentRepository();
            IEmployeeRepository objemp = new EmployeeRepository();

            var Data = (from users in objuserrepository.GetAll()
                        join dep in objdepart.GetAll() on users.DepartmentId equals dep.DepartmentId
                        join emp in objemp.GetAll() on users.EmployeeId equals emp.EmployeeId
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

            IEmployeeRepository objemployeeRepository = new EmployeeRepository();
            IDepartmentRepository objdepartmentRepository = new DepartmentRepository();


            var Data = (from emp in objemployeeRepository.GetAll()
                        join dep in objdepartmentRepository.GetAll() on emp.DepartmentId equals dep.DepartmentId
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
        public ActionResult CreateUser()
        {
            
            return View();
        }
       
        [HttpPost]
        public ActionResult CreateUser(EmployeeUserVM viewmodel)
        {
            IUserRepository objuserRepository = new UserRepository();
            IEmployeeRepository objemployeeRepository = new EmployeeRepository();


            var empid = objuserRepository.GetAll().Where(x => x.EmployeeId == viewmodel.EmployeeId).FirstOrDefault();
            var check = objuserRepository.GetAll().Where(a => a.UserName == viewmodel.UserName).FirstOrDefault();

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

                        var users = objuserRepository.AddUser(viewmodel.UserName, viewmodel.Password, viewmodel.EmployeeId, viewmodel.DepartmentId);
                        objuserRepository.Add(users);
                        objuserRepository.Save();

                        if (users != null)
                        {
                            objemployeeRepository.Update(viewmodel.EmployeeId, users.UserId);
                            objemployeeRepository.Save();
                            TempData["SuccessMessage1"] = "User Created";
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
            

            return View();
        }
        public ActionResult EmployeeList()
        {
            return View();
        }

        public ActionResult UserforroleList()
        {
            IUserRepository objuserRepository = new UserRepository();
            IEmployeeRepository objemployeeRepository = new EmployeeRepository();
            IDepartmentRepository objdepartmentRepository = new DepartmentRepository();

            var Data = (from user in objuserRepository.GetAll()
                        join emp in objemployeeRepository.GetAll() on user.EmployeeId equals emp.EmployeeId
                        join pos in objemployeeRepository.Getpos() on emp.PositionId equals pos.Id
                        join dep in objdepartmentRepository.GetAll() on user.DepartmentId equals dep.DepartmentId
                        where emp.UserId != null
                        select new
                        {
                            user.UserId,
                            user.UserName,
                            pos.Position,
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
        public ActionResult Assignroles()
        {
            IStructuredetailRepository objstructureRepository = new StructuredetailRepository();

            var rolelist = objstructureRepository.Getroles().ToList();

            SelectList list = new SelectList(rolelist, "Id", "RoleName");
            ViewBag.getrolelist = list;

            return View();
        }

        [HttpPost]
        public ActionResult Assignroles(EmployeeUserVM user,FormCollection form)
        {
            IStructuredetailRepository objstructureRepository = new StructuredetailRepository();
            IUserRepository objuserRepository = new UserRepository();


            var usercheck = objuserRepository.GetAll().Where(a => a.UserId == user.UserId).FirstOrDefault();
            if (usercheck != null)
            {
                var chkadmin = form["getadmin"];
                var adminId = Convert.ToInt32(chkadmin);

                objuserRepository.Update(user.RoleId, adminId, user.UserId);
                objuserRepository.Save();

                if (chkadmin == null)
                {
                    chkadmin = "0";
                }
                TempData["SuccessMessage1"] = "Role Assigned";

                var rolelist = objstructureRepository.Getroles().ToList();
                SelectList list = new SelectList(rolelist, "Id", "RoleName");
                ViewBag.getrolelist = list;
            }
            else
            {
                var rolelist = objstructureRepository.Getroles().ToList();
                SelectList list = new SelectList(rolelist, "Id", "RoleName");
                ViewBag.getrolelist = list;

                TempData["ErrorMessage1"] = "Select from User List";

            }


            return View();
        }
    }
}