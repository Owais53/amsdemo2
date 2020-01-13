using amsdemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using amsdemo.ViewModel;

namespace amsdemo.Controllers
{
    public class RequestsController : Controller
    {
        private readonly SqlContext db = new SqlContext();

        [HttpGet]
        public ActionResult EmployeeResignations()
        {
            return View();
        }



        [HttpPost]
        public ActionResult EmployeeResignations(RequestVM view)
        {
            var empid = Session["EmployeeId"].ToString();
            var cc = Convert.ToInt32(Session["CompanyCode"].ToString());
            var cci = Convert.ToInt32(Session["CityCode"].ToString());
            var di = Convert.ToInt32(Session["DepartmentId"].ToString());
            var pos = Session["Position"].ToString();
            var ename = Session["Employeename"].ToString();

            var req = db.tblRequestdetails.SqlQuery("select * from tblRequestdetail where EmployeeId=" + Convert.ToInt32(empid) + "").FirstOrDefault();

            if (req == null)
            {
                var reqdetail = new tblRequestdetail()
                {
                    CompanyCode = cc,
                    CityCode = cci,
                    DepartmentId = di,
                    Position = pos,
                    EmployeeId = Convert.ToInt32(empid),
                    EmployeeName = ename,
                    ReasonofRequest = view.ReasonofRequests,
                    LastWorkingDate = view.LastWorkingDate,
                    
                };
                db.tblRequestdetails.Add(reqdetail);
                db.SaveChanges();

                if(reqdetail != null)
                {
                    var request = new tblRequest()
                    {
                        RequestId = reqdetail.RequestId,
                        EmployeeId = Convert.ToInt32(empid),
                        RequestType = "Resignation",
                        DateofRequest = DateTime.Now,
                        Status="Pending",

                    };
                    db.tblRequests.Add(request);
                    db.SaveChanges();
                    if(request != null)
                    {
                        TempData["SuccessMessage"] = "Resignation Submitted";
                        return RedirectToAction("SubmittedRequests","Requests");
                        
                    }

                }

            }
            else
            {
                TempData["ErrorMessage1"] = "You already Submitted Resignation";
            }


            return View();
        }

        public ActionResult RequestList()
        {
            return View();
        }

        public ActionResult ReqList()
        {
            var Coc = Convert.ToInt32(Session["CompanyCode"].ToString());
            var Cic = Convert.ToInt32(Session["CityCode"].ToString());
            var Data = (from req in db.tblRequestdetails
                        join detail in db.tblRequests on req.RequestId equals detail.RequestId
                        join dep in db.tblDepartments on req.DepartmentId equals dep.DepartmentId
                        where req.CityCode == Cic && req.CompanyCode == Coc
                        select new
                        {
                            req.EmployeeName,
                            dep.DepartmentName,
                            detail.RequestType,
                            req.RequestId

                        }).ToList();

            return Json(new { data = Data }, JsonRequestBehavior.AllowGet);


        }
        public ActionResult SubmittedRequests()
        {
            return View();
        }


        public ActionResult subRequestList()
        {
            var empid = Convert.ToInt32(Session["EmployeeId"].ToString());
           

            var Data = (from req in db.tblRequests
                        where req.EmployeeId == empid
                        select new
                        {
                            req.RequestType,
                            req.DateofRequest,
                            req.Status

                        }).ToList();

            return Json(new { data = Data }, JsonRequestBehavior.AllowGet);
        }



    }
}