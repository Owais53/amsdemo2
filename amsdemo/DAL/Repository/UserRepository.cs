using amsdemo.DAL.Interface;
using amsdemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace amsdemo.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private SqlContext context;

        public UserRepository()
        {
            context = new SqlContext();

        }
        public UserRepository(SqlContext context)
        {
            context = this.context;
        }




        public void Add(tblUser obj)
        {
            context.tblUsers.Add(obj);
        }

        public void Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tblUser> GetAll()
        {
            return context.tblUsers;
        }
        public IEnumerable<tblAdmincheck> GetAdmin()
        {
            return context.tblAdminchecks;
        }

        public tblUser GetById(int userId)
        {
            return context.tblUsers.Single(model => model.UserId == userId);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(int roleid, int adminid, int userid)
        {
             
              var user = context.tblUsers.Find(userid);
              user.RoleId = roleid;
              user.IsActive = 1;
              user.AdminId = adminid;
              context.Entry(user).State = EntityState.Modified;
              
            
        }

        public tblUser AddUser(string username, string pass, int empid, int depid)
        {
            var users = new tblUser()
            {
                UserName = username,
                Password = pass,
                EmployeeId = empid,
                DepartmentId = depid,

            };

            return users;

        }

    }
}