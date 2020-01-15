using amsdemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace amsdemo.ViewModel
{
    public class Requests
    {
        private readonly SqlContext db = new SqlContext();

        public IEnumerable<RequestVM> GetRequests
        {
            get
            {
                var rd = (from d in db.tblRequestdetails
                          join req in db.tblRequests on d.RequestId equals req.RequestId
                          join emp in db.tblEmployees on d.EmployeeId equals emp.EmployeeId
                          join sd in db.tblStructuredetails on emp.Id equals sd.Id
                          join dep in db.tblDepartments on d.DepartmentId equals dep.DepartmentId
                          select new
                          {


                              d.CompanyCode,
                              d.CityCode,
                              sd.CityName,
                              sd.CompanyName,

                              dep.DepartmentName,
                              d.Position



                          });

                return rd.ToList().Select(c => new RequestVM()
                {
                    CompanyCode = Convert.ToInt32(c.CompanyCode),
                    CityCode = Convert.ToInt32(c.CityCode),
                    CityName = c.CityName,
                    CompanyName = c.CompanyName,

                    DepartmentName = c.DepartmentName,
                    Position = c.Position


                });
            }


        }





    }
}