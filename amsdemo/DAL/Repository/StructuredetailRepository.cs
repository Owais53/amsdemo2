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

        public void Add(int companycode,int citycode,string companyname,string cityname)
        {
           
            var structure = new tblStructuredetail()
            {
                CompanyCode = companycode,
                CityCode = citycode,
                CompanyName = companyname,
                CityName = cityname,
            };
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tblStructuredetail> GetAll()
        {
            return context.tblStructuredetails;
        }

        public IEnumerable<tblStructuredetail> Getcondition(int companycode,int citycode,string cname)
        {
            return context.tblStructuredetails
                 .Where(a => a.CompanyCode == companycode)
                 .Where(b => b.CityCode == citycode)
                 .Where(c => c.CityName == cname);
            
        }

        public IEnumerable<tblStructuredetail> Getcondition1(int companycode, int citycode)
        {
            return context.tblStructuredetails
                 .Where(a => a.CompanyCode == companycode)
                 .Where(b => b.CityCode == citycode);

        }

        public IEnumerable<tblStructuredetail> Getcondition2(int citycode,string cname)
        {
            return context.tblStructuredetails
                 .Where(a => a.CityCode == citycode)
                 .Where(b => b.CityName == cname);

        }

        public IEnumerable<tblStructuredetail> Getcondition3(int citycode)
        {
            return context.tblStructuredetails
                 .Where(a => a.CityCode == citycode);

        }

        public IEnumerable<tblStructuredetail> Getcondition4(string cname)
        {
            return context.tblStructuredetails
                 .Where(a => a.CityName == cname);

        }

        public IEnumerable<tblStructuredetail> Getcondition5(int companycode)
        {
            return context.tblStructuredetails
                 .Where(a => a.CompanyCode == companycode);

        }

        public IEnumerable<tblOrganizationStructure> Getvalidation(int companycode, int citycode, string sl, string po)
        {
            return 
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