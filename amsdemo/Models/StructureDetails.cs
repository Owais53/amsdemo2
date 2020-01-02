using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace amsdemo.Models
{
    [Table("tblStructuredetail")]
    public class StructureDetails
    {
        
        public int CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
       
    }
}