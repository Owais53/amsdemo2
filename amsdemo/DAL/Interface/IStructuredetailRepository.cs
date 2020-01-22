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

         tblStructuredetail GetById(int Id);

        void Add(tblStructuredetail obj);

        void Update(tblStructuredetail obj);

        IEnumerable<tblRole> Getroles();


        void Delete(int Id);

        void Save();



    }
}
