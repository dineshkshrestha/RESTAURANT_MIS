//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RESTAURANT_MIS
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orders
    {
        public int Orderid { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public int CustomerId { get; set; }
        public int ItemId { get; set; }
        public int TableId { get; set; }
    
        public virtual Customers Customers { get; set; }
        public virtual ITEMS ITEMS { get; set; }
        public virtual Tables Tables { get; set; }
    }
}
