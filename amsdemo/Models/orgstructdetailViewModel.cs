using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace amsdemo.Models
{
    public class orgstructdetailViewModel
    {
        
        public int CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string StorageLocation { get; set; }
        public string PurchaseOrganization { get; set; }

    }
}