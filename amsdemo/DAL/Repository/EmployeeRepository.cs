using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using amsdemo.Models;
using amsdemo.ViewModel;

namespace amsdemo.DAL.Repository
{
    public class EmployeeRepository : Interface.IEmployeeRepository
    {

       private SqlContext context;

        public EmployeeRepository()
        {
            context = new SqlContext();

        }
        public EmployeeRepository(SqlContext context)
        {
            context = this.context;
        }

        public void Add(tblEmployee obj)
        {
            context.tblEmployees.Add(obj);
        }

        public void Delete(int employeeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tblEmployee> GetAll()
        {
            return context.tblEmployees;
        }
        public IEnumerable<tblPosition> Getpos()
        {
            return context.tblPositions;
        }

        public tblEmployee GetById(int employeeId)
        {
            throw new NotImplementedException();
        }

       

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(int employeeid,int userid)
        {
            if (employeeid != null && userid != null)
            {

                tblEmployee employee = context.tblEmployees.Single(x => x.EmployeeId == employeeid);

                employee.UserId = userid;
            }
            else
            {
                return;
            }
        }
    }
}