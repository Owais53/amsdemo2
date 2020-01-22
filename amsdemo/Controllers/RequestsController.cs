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
                        where req.CityCode == Cic && req.CompanyCode == Coc && detail.Status =="Pending"
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

        public ActionResult VacancyAvailablity()
        {


            return View();
        }
        public ActionResult vacantAvailList()
        {
            var Coc = Convert.ToInt32(Session["CompanyCode"].ToString());
            var Cic = Convert.ToInt32(Session["CityCode"].ToString());
            var Data = (from req in db.tblRequestdetails
                        join detail in db.tblRequests on req.RequestId equals detail.RequestId
                        join dep in db.tblDepartments on req.DepartmentId equals dep.DepartmentId
                        join emp in db.tblEmployees on req.EmployeeId equals emp.EmployeeId
                        join s in db.tblStructuredetails on emp.Id equals s.Id
                        join d in db.tblVacancydetails on dep.DepartmentId equals d.DepartmentId
                        where req.CityCode == Cic && req.CompanyCode == Coc && detail.Status == "Approved"
                        select new
                        {
                            
                            s.CompanyName,
                            s.CityName,
                            dep.DepartmentName,
                            req.Position,
                            d.SeatAvailablityDate

                        }).ToList();

            return Json(new { data = Data }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult RequestDetail(int id = 0)
        {

            
                 if (id == 0)
            {
                return View(new RequestVM());
            }
            else
            {


                  var rd = (from d in db.tblRequestdetails
                                            join req in db.tblRequests on d.RequestId equals req.RequestId
                                            join emp in db.tblEmployees on d.EmployeeId equals emp.EmployeeId
                                            join sd in db.tblStructuredetails on emp.Id equals sd.Id
                                            join dep in db.tblDepartments on d.DepartmentId equals dep.DepartmentId
                                            where d.RequestId == id
                                            select new
                                            {

                                                d.RequestId,
                                                d.CompanyCode,
                                                d.CityCode,
                                                sd.CityName,
                                                sd.CompanyName,
                                                d.EmployeeName,
                                                d.DepartmentId,
                                                dep.DepartmentName,
                                                d.PositionId,
                                                d.Position,
                                                d.ReasonofRequest,
                                                req.RequestType,
                                                req.DateofRequest,
                                                d.LastWorkingDate


                                            }).Select(c=>new RequestVM() {
                                                RequestId = (int)(c.RequestId),
                                                CompanyCode = (int)(c.CompanyCode),
                                                CityCode = (int)(c.CityCode),
                                                CityName = c.CityName,
                                                CompanyName = c.CompanyName,
                                                DepartmentId = (int)(c.DepartmentId),
                                                DepartmentName = c.DepartmentName,
                                                EmployeeName = c.EmployeeName,
                                                PositionId = (int)c.PositionId,
                                                Position = c.Position,
                                                ReasonofRequests = c.ReasonofRequest,
                                                RequestType = c.RequestType,
                                                DateofRequest = (DateTime)(c.DateofRequest),
                                                LastWorkingDate = (DateTime)(c.LastWorkingDate),
                                                date = (DateTime)(c.LastWorkingDate)


                                            }).FirstOrDefault();

                return View(rd);
                            

                        
            }

        }
        [HttpPost]
        public ActionResult RequestDetail(RequestVM vM)
        {
            var ename = Session["EmployeeName"].ToString();
            var vacantdetail = new tblVacancydetail()
            {
                CompanyCode = vM.CompanyCode,
                CityCode = vM.CityCode,
                DepartmentId = vM.DepartmentId,
                PositionId = vM.PositionId,
                SeatAvailablityDate = vM.date,
            };
            db.tblVacancydetails.Add(vacantdetail);
            db.SaveChanges();

            if(vacantdetail!= null)
            {
                List<object> lst = new List<object>();
                lst.Add(ename);
                lst.Add(DateTime.Now);
                lst.Add(vM.RequestId);
                object[] item = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("Update tblRequests set Status='Approved',Respondedby=@p0,ResponseDate=@p1 where RequestId=@p2", item);
                if (output > 0)
                {
                    TempData["SuccessMessage1"] = "Resignation Approved";
                    return RedirectToAction("RequestList");
                }

                
            }

            return View();
        }

        [HttpPost]
        public ActionResult Reject(RequestVM vM)
        {
            var ename = Session["EmployeeName"].ToString();
            var reqid = db.tblRequests.Where(x => x.RequestId == vM.RequestId).FirstOrDefault();
            if (reqid != null)
            {
                List<object> lst = new List<object>();
                lst.Add(ename);
                lst.Add(DateTime.Now);
                lst.Add(vM.RequestId);
                object[] item = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("Update tblRequests set Status='Rejected',Respondedby=@p0,ResponseDate=@p1 where RequestId=@p2", item);
                if (output > 0)
                {
                    TempData["SuccessMessage1"] = "Resignation Rejected";
                    return RedirectToAction("RequestList");
                }
            }
            else
            {
                TempData["ErrorMessage1"] = "You have not selected any request from Request List";
            }

            return View("RequestDetail");
        }



    }
}