using amsdemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amsdemo.DAL.Interface
{
    public interface IDepartmentRepository
    {
        IEnumerable<tblDepartment> GetAll();

       
        tblDepartment GetById(int departmentId);

        tblDepartment AddDep(int departmentid, string departmentname);

        void Add(tblDepartment obj);

        void Update(tblDepartment obj);

        void Delete(int departmentId);

        void Save();


    }
}
