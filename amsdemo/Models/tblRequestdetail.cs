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
    
    public partial class tblRequestdetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblRequestdetail()
        {
            this.tblRequests = new HashSet<tblRequest>();
        }
    
        public int RequestId { get; set; }
        public Nullable<int> CompanyCode { get; set; }
        public Nullable<int> CityCode { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> PositionId { get; set; }
        public string Position { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string ReasonofRequest { get; set; }
        public Nullable<System.DateTime> LastWorkingDate { get; set; }
    
        public virtual tblEmployee tblEmployee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblRequest> tblRequests { get; set; }
    }
}
