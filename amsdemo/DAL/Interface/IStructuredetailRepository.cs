using amsdemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace amsdemo.DAL.Interface
{
    public interface IStructuredetailRepository
    {
        IEnumerable<tblStructuredetail> GetAll();

        IEnumerable<tblStructuredetail> Getcondition(int companycode,int citycode,string cname);

        IEnumerable<tblStructuredetail> Getcondition1(int companycode, int citycode);

        IEnumerable<tblStructuredetail> Getcondition2(int citycode,string cname);

        IEnumerable<tblStructuredetail> Getcondition3(int citycode);

        IEnumerable<tblStructuredetail> Getcondition4(string cname);

        IEnumerable<tblStructuredetail> Getcondition5(int companycode);

        IEnumerable<tblOrganizationStructure> Getvalidation(int companycode, int citycode, string sl,string po);

        IEnumerable<tblOrganizationStructure> Getvalidation1(int companycode, int citycode);

        IEnumerable<tblOrganizationStructure> Getvalidation2(string sl, string po);


        tblStructuredetail GetById(int Id);

        void Add(int companycode, int citycode, string companyname, string cityname);

        void Update(tblStructuredetail obj);

        IEnumerable<tblRole> Getroles();


        void Delete(int Id);

        void Save();



    }
}
