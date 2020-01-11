using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace amsdemo.Models
{
   
    public class StructureDetails
    {
        
        public int CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public int CityCode { get; set; }
        public string CityName { get; set; }
       
    }
}