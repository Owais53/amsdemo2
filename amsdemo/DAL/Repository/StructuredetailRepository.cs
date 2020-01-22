using amsdemo.DAL.Interface;
using amsdemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace amsdemo.DAL.Repository
{
    public class StructuredetailRepository : IStructuredetailRepository
    {
        private SqlContext context;

        public StructuredetailRepository()
        {
            context = new SqlContext();
        }
        public StructuredetailRepository(SqlContext context)
        {
            context = this.context;
        }

        public void Add(tblStructuredetail obj)
        {
            context.tblStructuredetails.Add(obj);
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tblStructuredetail> GetAll()
        {
            return context.tblStructuredetails;
        }

        public tblStructuredetail GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(tblStructuredetail obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tblRole> Getroles()
        {
            return context.tblRoles;
        }
    }
}