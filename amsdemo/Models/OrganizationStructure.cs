using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace amsdemo.Models
{
    [Table("tblOrganizationStructure")]
    public class OrganizationStructure
    {
        public int CompanyCode { get; set; }
        public string CityCode { get; set; }
        public string StorageLocation { get; set; }
        public string PurchaseOrganization { get; set; }

    }
}