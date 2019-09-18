using Reparatieshop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Reparatieshop.DAL
{
    public class ShopContext : DbContext
    {
        public ShopContext() : base("DefaultConnection")
        { 
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Repairer> Repairers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}