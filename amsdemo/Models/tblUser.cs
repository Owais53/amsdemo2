//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace amsdemo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> AdminId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> IsActive { get; set; }
    
        public virtual tblAdmincheck tblAdmincheck { get; set; }
        public virtual tblRole tblRole { get; set; }
    }
}
