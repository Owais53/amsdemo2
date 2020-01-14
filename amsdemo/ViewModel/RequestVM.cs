using amsdemo.Models;
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
        public string CityName { get; set; }
        public string CompanyName { get; set; }
        public DateTime DateofRequest { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Position { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string ReasonofRequests { get; set; }
        [Required]
        [Display(Name = "Last Working Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastWorkingDate { get; set; } 
        public string RequestType { get; set; }
        public string Status { get; set; }
        public int RequestId { get; set; }
    }
}