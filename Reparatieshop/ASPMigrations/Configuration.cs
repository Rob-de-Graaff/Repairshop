namespace Reparatieshop.ASPMigrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Reparatieshop.DAL;
    using Reparatieshop.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Reparatieshop.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"ASPMigrations";
            ContextKey = "Reparatieshop.Models.ApplicationDbContext";
        }

        protected override void Seed(Reparatieshop.Models.ApplicationDbContext context)
        {
            //ShopContext shopContext = new ShopContext();

            //var userStore = new UserStore<ApplicationUser>(context);
            //var userManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var applicationRoleAdministrator = new ApplicationRole { Name = "Administrator" };
            var applicationRoleCustomer = new ApplicationRole { Name = "Customer" };
            var applicationRoleRepairer = new ApplicationRole { Name = "Repairer" };
            if (!roleManager.RoleExists(applicationRoleAdministrator.Name))
            {
                roleManager.Create(applicationRoleAdministrator);
            }
            if (!roleManager.RoleExists(applicationRoleCustomer.Name))
            {
                roleManager.Create(applicationRoleCustomer);
            }
            if (!roleManager.RoleExists(applicationRoleRepairer.Name))
            {
                roleManager.Create(applicationRoleRepairer);
            }

            //string[] roles = new string[] { "Administrator", "Customer", "Repairer" };
            //foreach (string role in roles)
            //{
            //    if (!context.Roles.Any(r => r.Name == role))
            //    {
            //        context.Roles.Add(new IdentityRole(role));
            //    }
            //}
            //var admin = new ApplicationUser { UserName = "admin@email.nl", Email = "admin@email.nl" };
            //userManager.Create(admin, "12#$abCD");
            //userManager.AddToRole(admin.Id, "Administrator");

            if (!context.Users.Any(u => u.UserName == "admin@email.nl"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "admin@email.nl", Email = "admin@email.nl" };
                userManager.Create(user, "12#$abCD");
                userManager.AddToRole(user.Id, "Administrator");
            }

            if (!context.Users.Any(u => u.UserName == "testcust1@email.nl"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "testcust1@email.nl", Email = "testcust1@email.nl" };
                userManager.Create(user, "23#$abCD");
                userManager.AddToRole(user.Id, "Customer");
                using (ShopContext shopContext = new ShopContext())
                {
                    shopContext.Customers.Add(new Customer 
                    { 
                        CustomerId = user.Id, 
                        FirstName = "Ad", 
                        LastName = "A", 
                        DoB = DateTime.Parse("1990-12-01"), 
                        City = "Amsterdam", 
                        Zipcode = "A", 
                        HouseNumber = 1 
                        });
                    shopContext.SaveChanges();
                }
            }

            if (!context.Users.Any(u => u.UserName == "testcust2@email.nl"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "testcust2@email.nl", Email = "testcust2@email.nl" };
                userManager.Create(user, "34#$abCD");
                userManager.AddToRole(user.Id, "Customer");
                using (ShopContext shopContext = new ShopContext())
                {
                    shopContext.Customers.Add(new Customer
                    {
                        CustomerId = user.Id,
                        FirstName = "Bert",
                        LastName = "B",
                        DoB = DateTime.Parse("1980-09-01"),
                        City = "B",
                        Zipcode = "B",
                        HouseNumber = 2
                    });
                    shopContext.SaveChanges();
                }
            }

            if (!context.Users.Any(u => u.UserName == "testrep1@email.nl"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "testrep1@email.nl", Email = "testrep1@email.nl" };
                userManager.Create(user, "56#$abCD");
                userManager.AddToRole(user.Id, "Repairer");
                using (ShopContext shopContext = new ShopContext())
                {
                    shopContext.Customers.Add(new Customer
                    {
                        CustomerId = user.Id,
                        FirstName = "Frans",
                        LastName = "F",
                        DoB = DateTime.Parse("1985-11-15"),
                        City = "F",
                        Zipcode = "F",
                        HouseNumber = 1
                    });
                    shopContext.SaveChanges();
                }
            }

            if (!context.Users.Any(u => u.UserName == "testrep2@email.nl"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "testrep2@email.nl", Email = "testrep2@email.nl" };
                userManager.Create(user, "67#$abCD");
                userManager.AddToRole(user.Id, "Repairer");
                using (ShopContext shopContext = new ShopContext())
                {
                    shopContext.Customers.Add(new Customer
                    {
                        CustomerId = user.Id,
                        FirstName = "Gerard",
                        LastName = "G",
                        DoB = DateTime.Parse("1993-10-02"),
                        City = "G",
                        Zipcode = "G",
                        HouseNumber = 2
                    });
                    shopContext.SaveChanges();
                }
            }
            //var appuser1 = 
            //userManager.Create(appuser1, "12#$abCD");
            //var appuser2 = new ApplicationUser { UserName = "testcust2@email.nl", Email = "testcust2@email.nl" };
            //userManager.Create(appuser2, "12#$abCD");
            //var appuser3 = new ApplicationUser { UserName = "testcust3@email.nl", Email = "testcust3@email.nl" };
            //userManager.Create(appuser3, "12#$abCD");
            //var appuser4 = new ApplicationUser { UserName = "testcust4@email.nl", Email = "testcust4@email.nl" };
            //userManager.Create(appuser4, "12#$abCD");
            //var appuser5 = new ApplicationUser { UserName = "testcust5@email.nl", Email = "testcust5@email.nl" };
            //userManager.Create(appuser5, "12#$abCD");


            //string[] userIdCust = new string[5];
            //int countCust = 0;
            //foreach (var user in userManager.Users)
            //{
            //    userIdCust[countCust] = user.Id;
            //    userManager.AddToRole(user.Id, "Customer");
            //    countCust++;
            //}
            //var userList = userManager.Users;

            //foreach (var user in userList)
            //{
            //    userManager.AddToRole(user.Id, "Customer");
            //}

            //List<Customer> customers = new List<Customer>
            //{
            //    new Customer{CustomerId=userIdCust[0], FirstName="Ad",LastName="A", DoB=DateTime.Parse("1990-12-01"), City="Amsterdam", Zipcode="A", HouseNumber=1},
            //    new Customer{CustomerId=userIdCust[1], FirstName="Bert",LastName="B", DoB=DateTime.Parse("1980-09-01"), City="B", Zipcode="B", HouseNumber=2},
            //    new Customer{CustomerId=userIdCust[2], FirstName="Clara",LastName="C", DoB=DateTime.Parse("2000-05-01"), City="C", Zipcode="C", HouseNumber=3},
            //    new Customer{CustomerId=userIdCust[3], FirstName="Dave",LastName="D", DoB=DateTime.Parse("1993-07-01"), City="Den Bosch", Zipcode="D", HouseNumber=4},
            //    new Customer{CustomerId=userIdCust[4], FirstName="Eef",LastName="E", DoB=DateTime.Parse("1985-09-10"), City="E", Zipcode="E", HouseNumber=5}
            //};

            //using (ShopContext shopContext = new ShopContext())
            //{
            //    customers.ForEach(c => shopContext.Customers.Add(c));
            //    shopContext.SaveChanges();
            //}


            //var appuser6 = new ApplicationUser { UserName = "testrep1@email.nl", Email = "testrep1@email.nl" };
            //userManager.Create(appuser6, "12#$abCD");
            //var appuser7 = new ApplicationUser { UserName = "testrep2@email.nl", Email = "testrep2@email.nl" };
            //userManager.Create(appuser7, "12#$abCD");
            //var appuser8 = new ApplicationUser { UserName = "testrep3@email.nl", Email = "testrep3@email.nl" };
            //userManager.Create(appuser8, "12#$abCD");
            //var appuser9 = new ApplicationUser { UserName = "testrep4@email.nl", Email = "testrep4@email.nl" };
            //userManager.Create(appuser9, "12#$abCD");
            //var appuser10 = new ApplicationUser { UserName = "testrep5@email.nl", Email = "testrep5@email.nl" };
            //userManager.Create(appuser10, "12#$abCD");


            //string[] userIdRep = new string[5];
            //int countRep = 5;
            //foreach (var user in userManager.Users)
            //{
            //    if (!userManager.IsInRole(user.Id, "Customer"))
            //    {
            //        userIdRep[countRep] = user.Id;
            //        userManager.AddToRole(user.Id, "Repairer");
            //        countRep++;
            //    }
            //}
            //foreach (var user in userList)
            //{
            //    if (!userManager.IsInRole(user.Id, "Customer"))
            //    {
            //        userManager.AddToRole(user.Id, "Repairer");
            //    }
            //}

            //IList<Repairer> repairers = new List<Repairer>();

            //repairers.Add(new Repairer() { RepairerId = userIdRep[0], FirstName = "F", LastName = "F", DoB = DateTime.Now.Date, Wage = 14.70 });
            //repairers.Add(new Repairer() { RepairerId = userIdRep[1], FirstName = "G", LastName = "G", DoB = DateTime.Now.Date, Wage = 14.70 });
            //repairers.Add(new Repairer() { RepairerId = userIdRep[2], FirstName = "H", LastName = "H", DoB = DateTime.Now.Date, Wage = 14.70 });
            //repairers.Add(new Repairer() { RepairerId = userIdRep[3], FirstName = "I", LastName = "I", DoB = DateTime.Now.Date, Wage = 14.70 });
            //repairers.Add(new Repairer() { RepairerId = userIdRep[4], FirstName = "J", LastName = "J", DoB = DateTime.Now.Date, Wage = 14.70 });

            //using (ShopContext shopContext = new ShopContext())
            //{
            //    shopContext.Repairers.AddRange(repairers);
            //    shopContext.SaveChanges();
            //}
            //List<Product> products = new List<Product>
            //{
            //    new Product{Name="K", Price=200},
            //    new Product{Name="L", Price=250},
            //    new Product{Name="M", Price=200},
            //    new Product{Name="N", Price=300},
            //    new Product{Name="O", Price=50}
            //};

            //using (ShopContext shopContext = new ShopContext())
            //{
            //    products.ForEach(p => shopContext.Products.Add(p));
            //    shopContext.SaveChanges();
            //}

            //IList<Assignment> assignments = new List<Assignment>();

            //assignments.Add(new Assignment() { Start = new DateTime(2000, 01, 1), End = new DateTime(2001, 02, 2), Status = Status.Pending, HoursWorked = 1 });
            //assignments.Add(new Assignment() { Start = new DateTime(2001, 02, 2), End = new DateTime(2002, 03, 3), Status = Status.in_progress, HoursWorked = 2 });
            //assignments.Add(new Assignment() { Start = new DateTime(2002, 03, 3), End = new DateTime(2003, 04, 4), Status = Status.Done, HoursWorked = 3 });
            //assignments.Add(new Assignment() { Start = new DateTime(2003, 04, 4), End = new DateTime(2004, 05, 5), Status = Status.Waiting_for_parts, HoursWorked = 4 });
            //assignments.Add(new Assignment() { Start = new DateTime(2004, 05, 5), End = new DateTime(2005, 06, 6), Status = Status.Done, HoursWorked = 5 });

            //using (ShopContext shopContext = new ShopContext())
            //{
            //    shopContext.Assignments.AddRange(assignments);
            //    shopContext.SaveChanges();
            //}

        }
    }
}
