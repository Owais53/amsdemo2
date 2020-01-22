using amsdemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amsdemo.DAL.Interface
{
    public interface IUserRepository
    {
        IEnumerable<tblUser> GetAll();

        IEnumerable<tblAdmincheck> GetAdmin();


        tblUser GetById(int userId);

        void Add(tblUser obj);

        void Update(int roleid,int adminid,int userid);

        void Delete(int userId);

        tblUser AddUser(string username,string pass,int empid,int depid);

        void Save();

       
    }
}
