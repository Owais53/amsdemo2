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
    
    public partial class tblVacancydetail
    {
        public int Id { get; set; }
        public Nullable<int> CompanyCode { get; set; }
        public Nullable<int> CityCode { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string Position { get; set; }
        public Nullable<int> Availableseats { get; set; }
    
        public virtual tblDepartment tblDepartment { get; set; }
    }
}
