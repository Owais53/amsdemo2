﻿using amsdemo.Infrastructure;
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
            
                using (var context = new SqlContext())
                {
                var isActive = context.tblUsers.Where(x => x.IsActive == 1)
                    .Where(a=>a.UserName==model.UserName && a.Password == model.Password).FirstOrDefault();

                if (isActive != null)
                {
                    var user1 = context.tblUsers.SqlQuery("Select u.UserName,u.UserId,d.DepartmentName,a.desc,r.RoleName from tblUsers u join tblDepartments d on u.DepartmentId=d.DepartmentId join tblAdmincheck a on u.AdminId=a.AdminId join tblRoles r on u.RoleId=r.Id join tblEmployee e on u.EmployeeId=e.EmployeeId join tblStructuredetail s on e.CityCode=s.CityCode and e.CompanyCode=s.CompanyCode where u.UserName='"+model.UserName+" and u.Password='"+model.Password+"" ).FirstOrDefault();
                    var user = (from u in context.tblUsers
                               join d in context.tblDepartments on u.DepartmentId equals d.DepartmentId
                               join a in context.tblAdminchecks on u.AdminId equals a.AdminId
                               join r in context.tblRoles on u.RoleId equals r.Id
                               join e in context.tblEmployees on u.EmployeeId equals e.EmployeeId
                                                         
                               where u.UserName == model.UserName && u.Password == model.Password
                               select new
                               {
                                   u.UserName,
                                   u.UserId,
                                   d.DepartmentName,
                                   a.desc,
                                   r.RoleName
                               }).FirstOrDefault();

                   
                    if (user1 != null)
                    {
                        Session["UserName"] = user.UserName;
                        Session["UserId"] = user.UserId;
                        Session["DepartmentName"] = user.DepartmentName;
                        Session["RoleName"] = user.RoleName;
                        Session["isAdmin"] = user.desc;
                        Session["DepartmentName"] = user.DepartmentName;
                        Session["RoleName"] = user.RoleName;
                        Session["isAdmin"] = user.desc;

                        TempData["SuccessMessage"] = "Login Successfull";
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {

                        TempData["ErrorMessage"] = "Invalid UserName or Password";
                        
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "User is not Active.Please Login Again.";
                }
                return View();


            }
                       

        }


        public ActionResult Logout()
        {
            Session.Abandon();
            Session["UserName"] = string.Empty;
            Session["UserId"] = string.Empty;
            Session["DepartmentName"] = string.Empty;
            Session["RoleName"] = string.Empty;
            Session["isAdmin"] = string.Empty;
            
            return RedirectToAction("Login", "Account");
        }

    }
}