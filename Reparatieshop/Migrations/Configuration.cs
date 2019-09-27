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

            //var userStore = new UserStore<ApplicationUser>(ApplicationDbContext.Create());
            //var userManager = new UserManager<ApplicationUser>(userStore);

            //List<string> userIdCust = new List<string>();

            //foreach (var user in userManager.Users.ToList())
            //{
            //    if (userManager.IsInRole(user.Id, "Customer"))
            //    {
            //        userIdCust.Add(user.Id);
            //    }
            //}

            //List<Customer> customers = new List<Customer>
            //{
            //    new Customer{CustomerId=userIdCust[0], FirstName="Ad",LastName="A", DoB=DateTime.Parse("1990-12-01"), City="Amsterdam", Zipcode="A", HouseNumber=1},
            //    new Customer{CustomerId=userIdCust[1], FirstName="Bert",LastName="B", DoB=DateTime.Parse("1980-09-01"), City="B", Zipcode="B", HouseNumber=2},
            //    new Customer{CustomerId=userIdCust[2], FirstName="Clara",LastName="C", DoB=DateTime.Parse("2000-05-01"), City="C", Zipcode="C", HouseNumber=3},
            //    new Customer{CustomerId=userIdCust[3], FirstName="Dave",LastName="D", DoB=DateTime.Parse("1993-07-01"), City="Den Bosch", Zipcode="D", HouseNumber=4},
            //    new Customer{CustomerId=userIdCust[4], FirstName="Eef",LastName="E", DoB=DateTime.Parse("1985-09-10"), City="E", Zipcode="E", HouseNumber=5}
            //};

            //customers.ForEach(c => context.Customers.Add(c));
            //context.SaveChanges();

            //List<string> userIdRep = new List<string>();

            //foreach (var user in userManager.Users.ToList())
            //{
            //    if (userManager.IsInRole(user.Id, "Repairer"))
            //    {
            //        userIdRep.Add(user.Id);
            //    }
            //}

            //IList<Repairer> repairers = new List<Repairer>();

            //repairers.Add(new Repairer() { RepairerId = userIdRep[0], FirstName = "Frans", LastName = "F", DoB = DateTime.Parse("1985-11-15"), Wage = 14.70 });
            //repairers.Add(new Repairer() { RepairerId = userIdRep[1], FirstName = "Gerard", LastName = "G", DoB = DateTime.Parse("1993-10-02"), Wage = 14.70 });
            //repairers.Add(new Repairer() { RepairerId = userIdRep[2], FirstName = "Harry", LastName = "H", DoB = DateTime.Parse("1989-03-03"), Wage = 14.70 });
            //repairers.Add(new Repairer() { RepairerId = userIdRep[3], FirstName = "Ian", LastName = "I", DoB = DateTime.Parse("1995-05-11"), Wage = 14.70 });
            //repairers.Add(new Repairer() { RepairerId = userIdRep[4], FirstName = "Jan", LastName = "J", DoB = DateTime.Parse("1987-07-27"), Wage = 14.70 });


            //context.Repairers.AddRange(repairers);
            //context.SaveChanges();

            //List<Product> products = new List<Product>
            //{
            //    new Product{Name="K", Price=200},
            //    new Product{Name="L", Price=250},
            //    new Product{Name="M", Price=200},
            //    new Product{Name="N", Price=300},
            //    new Product{Name="O", Price=50}
            //};

            //products.ForEach(p => context.Products.Add(p));
            //context.SaveChanges();

            //var custid = context.Customers.ToList();
            //var repId = context.Repairers.ToList();

            //IList<Assignment> assignments = new List<Assignment>();

            //assignments.Add(new Assignment() { Start = new DateTime(2000, 01, 1), End = new DateTime(2001, 02, 2), Status = Status.Pending, HoursWorked = 1, Customer = custid[0], Repairer = repId[0] });
            //assignments.Add(new Assignment() { Start = new DateTime(2001, 02, 2), End = new DateTime(2002, 03, 3), Status = Status.in_progress, HoursWorked = 2, Customer = custid[1], Repairer = repId[1] });
            //assignments.Add(new Assignment() { Start = new DateTime(2002, 03, 3), End = new DateTime(2003, 04, 4), Status = Status.Done, HoursWorked = 3, Customer = custid[2], Repairer = repId[2] });
            //assignments.Add(new Assignment() { Start = new DateTime(2003, 04, 4), End = new DateTime(2004, 05, 5), Status = Status.Waiting_for_parts, HoursWorked = 4, Customer = custid[3], Repairer = repId[3] });
            //assignments.Add(new Assignment() { Start = new DateTime(2004, 05, 5), End = new DateTime(2005, 06, 6), Status = Status.Done, HoursWorked = 5, Customer = custid[4], Repairer = repId[4] });

            //context.Assignments.AddRange(assignments);
            //context.SaveChanges();
        }
    }
}
