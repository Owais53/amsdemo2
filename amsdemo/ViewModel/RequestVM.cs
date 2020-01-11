using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace amsdemo.ViewModel
{
    public class RequestVM
    {
        public int CompanyCode { get; set; }
        public int CityCode { get; set; }
        public int DepartmentId { get; set; }
        public string Position { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string ReasonofRequests { get; set; }
        [Required]
        public DateTime LastWorkingDate { get; set; }
        public string RequestType { get; set; }
        public string Status { get; set; }


    }
}