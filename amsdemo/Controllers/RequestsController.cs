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
                    
                };
                db.tblRequestdetails.Add(reqdetail);
                db.SaveChanges();

            }
           



            return View();
        }
    }
}