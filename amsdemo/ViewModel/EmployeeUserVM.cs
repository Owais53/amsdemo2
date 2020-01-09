using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace amsdemo.ViewModel
{
    public class EmployeeUserVM
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType("Password")]
        public string Password { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [NotMapped]
        public string DepartmentName { get; set; }
        [NotMapped]
        public string Position { get; set; }
        public int RoleId { get; set; }
        [NotMapped]
        public string RoleName { get; set; }
        public int AdminId { get; set; }
        [DataType("Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
          



    }
}