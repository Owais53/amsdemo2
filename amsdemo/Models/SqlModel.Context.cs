﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SqlContext : DbContext
    {
        public SqlContext()
            : base("name=SqlContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblAdmincheck> tblAdminchecks { get; set; }
        public virtual DbSet<tblDepartment> tblDepartments { get; set; }
        public virtual DbSet<tblEmployee> tblEmployees { get; set; }
        public virtual DbSet<tblEmployeeDetail> tblEmployeeDetails { get; set; }
        public virtual DbSet<tblRole> tblRoles { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tblOrganizationStructure> tblOrganizationStructures { get; set; }
        public virtual DbSet<tblStructuredetail> tblStructuredetails { get; set; }
        public virtual DbSet<tblVacancydetail> tblVacancydetails { get; set; }
    
        public virtual int spGetAllUsers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spGetAllUsers");
        }
    }
}
