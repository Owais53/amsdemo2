using amsdemo.DAL.Interface;
using amsdemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace amsdemo.DAL.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {

        private SqlContext context;

        public DepartmentRepository()
        {
            context = new SqlContext();

        }
        public DepartmentRepository(SqlContext context)
        {
            context = this.context;
        }


        public void Add(tblDepartment obj)
        {
            context.tblDepartments.Add(obj);
        }

        public tblDepartment AddDep(int departmentid,string departmentname)
        {
            var dep = new tblDepartment()
            {
                DepartmentId = departmentid,
                DepartmentName = departmentname,

            };

            return dep;

        }

        public void Delete(int departmentId)
        {
            var entity = context.tblDepartments.Where(x => x.DepartmentId == departmentId).FirstOrDefault();
            context.tblDepartments.Remove(entity);
        }

        public IEnumerable<tblDepartment> GetAll()
        {
            return context.tblDepartments;
        }

        public tblDepartment GetById(int departmentId)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(tblDepartment obj)
        {
            context.Entry(obj).State = EntityState.Modified;
        }
    }
}