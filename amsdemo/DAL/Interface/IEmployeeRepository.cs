using amsdemo.Models;
using amsdemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amsdemo.DAL.Interface
{
    public interface IEmployeeRepository
    {
      

        IEnumerable<tblEmployee> GetAll();

        tblEmployee GetById(int employeeId);

        void Add(tblEmployee obj);

        void Update(int employeeid,int userid);

        IEnumerable<tblPosition> Getpos();

        void Delete(int employeeId);

        void Save();



    }
}
