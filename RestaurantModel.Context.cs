﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RestaurantEntities : DbContext
    {
        public RestaurantEntities()
            : base("name=RestaurantEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bills> Bills { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<ITEMS> ITEMS { get; set; }
        public virtual DbSet<Tables> Tables { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
    }
}
