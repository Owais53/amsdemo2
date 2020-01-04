using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace amsdemo.ViewModel
{
    public class EmployeeUserVM
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }        
        [Required]
        public string Password { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }



    }
}