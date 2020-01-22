//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace amsdemo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPurchase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPurchase()
        {
            this.tblExpenses = new HashSet<tblExpens>();
            this.tblPurchaseitems = new HashSet<tblPurchaseitem>();
        }
    
        public int PurchaseId { get; set; }
        public Nullable<int> DocumentNo { get; set; }
        public string PurchaseOrg { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
        public Nullable<System.DateTime> OrderedDate { get; set; }
    
        public virtual tblDocument tblDocument { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblExpens> tblExpenses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPurchaseitem> tblPurchaseitems { get; set; }
    }
}