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
    
    public partial class tblQuestion
    {
        public int QuestionId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public Nullable<int> CorrectAnswer { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    }
}