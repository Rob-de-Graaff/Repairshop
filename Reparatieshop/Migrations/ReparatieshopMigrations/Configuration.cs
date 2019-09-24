namespace Reparatieshop.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Reparatieshop.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Reparatieshop.DAL.ShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations";
            ContextKey = "Reparatieshop.DAL.ShopContext";
        }

        protected override void Seed(Reparatieshop.DAL.ShopContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            List<Customer> customers = new List<Customer>
            {
                new Customer{FirstName="Ad",LastName="A", DoB=DateTime.Parse("1990-12-01"), City="Amsterdam", Zipcode="A", HouseNumber=1},
                new Customer{FirstName="Bert",LastName="B", DoB=DateTime.Parse("1980-09-01"), City="B", Zipcode="B", HouseNumber=2},
                new Customer{FirstName="Clara",LastName="C", DoB=DateTime.Parse("2000-05-01"), City="C", Zipcode="C", HouseNumber=3},
                new Customer{FirstName="Dave",LastName="D", DoB=DateTime.Parse("1993-07-01"), City="Den Bosch", Zipcode="D", HouseNumber=4},
                new Customer{FirstName="Eef",LastName="E", DoB=DateTime.Parse("1985-09-10"), City="E", Zipcode="E", HouseNumber=5}
            };

            customers.ForEach(c => context.Customers.Add(c));
            context.SaveChanges();

            IList<Repairer> repairers = new List<Repairer>();

            repairers.Add(new Repairer() { FirstName = "F", LastName = "F", DoB = DateTime.Now.Date, Wage = 14.70 });
            repairers.Add(new Repairer() { FirstName = "G", LastName = "G", DoB = DateTime.Now.Date, Wage = 14.70 });
            repairers.Add(new Repairer() { FirstName = "H", LastName = "H", DoB = DateTime.Now.Date, Wage = 14.70 });
            repairers.Add(new Repairer() { FirstName = "I", LastName = "I", DoB = DateTime.Now.Date, Wage = 14.70 });
            repairers.Add(new Repairer() { FirstName = "J", LastName = "J", DoB = DateTime.Now.Date, Wage = 14.70 });

            context.Repairers.AddRange(repairers);
            context.SaveChanges();

            List<Product> products = new List<Product>
            {
                new Product{Name="K", Price=200},
                new Product{Name="L", Price=250},
                new Product{Name="M", Price=200},
                new Product{Name="N", Price=300},
                new Product{Name="O", Price=50}
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();

            IList<Assignment> assignments = new List<Assignment>();

            assignments.Add(new Assignment() { Start = new DateTime(2000, 01, 1), End = new DateTime(2001, 02, 2), Status = Status.Pending, HoursWorked = 1 });
            assignments.Add(new Assignment() { Start = new DateTime(2001, 02, 2), End = new DateTime(2002, 03, 3), Status = Status.in_progress, HoursWorked = 2 });
            assignments.Add(new Assignment() { Start = new DateTime(2002, 03, 3), End = new DateTime(2003, 04, 4), Status = Status.Done, HoursWorked = 3 });
            assignments.Add(new Assignment() { Start = new DateTime(2003, 04, 4), End = new DateTime(2004, 05, 5), Status = Status.Waiting_for_parts, HoursWorked = 4 });
            assignments.Add(new Assignment() { Start = new DateTime(2004, 05, 5), End = new DateTime(2005, 06, 6), Status = Status.Done, HoursWorked = 5 });
            
            context.Assignments.AddRange(assignments);
            context.SaveChanges();
        }
    }
}
