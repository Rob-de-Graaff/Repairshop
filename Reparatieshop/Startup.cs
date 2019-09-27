using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Reparatieshop.DAL;
using Reparatieshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(Reparatieshop.Startup))]
namespace Reparatieshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createEntities();
        }

        private void createEntities()
        {
            ShopContext shopContext = new ShopContext();
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

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

            if (!context.Users.Any(u => u.UserName == "testcust2@email.nl"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "testcust2@email.nl", Email = "testcust2@email.nl" };
                userManager.Create(user, "34#$abCD");
                userManager.AddToRole(user.Id, "Customer");
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

            if (!context.Users.Any(u => u.UserName == "testcust3@email.nl"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "testcust3@email.nl", Email = "testcust3@email.nl" };
                userManager.Create(user, "45#$abCD");
                userManager.AddToRole(user.Id, "Customer");
                shopContext.Customers.Add(new Customer
                {
                    CustomerId = user.Id,
                    FirstName = "Clara",
                    LastName = "C",
                    DoB = DateTime.Parse("2000-05-01"),
                    City = "C",
                    Zipcode = "C",
                    HouseNumber = 3
                });
                shopContext.SaveChanges();
            }

            if (!context.Users.Any(u => u.UserName == "testcust4@email.nl"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "testcust4@email.nl", Email = "testcust4@email.nl" };
                userManager.Create(user, "56#$abCD");
                userManager.AddToRole(user.Id, "Customer");
                shopContext.Customers.Add(new Customer
                {
                    CustomerId = user.Id,
                    FirstName = "Dave",
                    LastName = "D",
                    DoB = DateTime.Parse("1993-07-01"),
                    City = "Den Bosch",
                    Zipcode = "D",
                    HouseNumber = 4
                });
                shopContext.SaveChanges();
            }

            if (!context.Users.Any(u => u.UserName == "testcust5@email.nl"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "testcust5@email.nl", Email = "testcust5@email.nl" };
                userManager.Create(user, "67#$abCD");
                userManager.AddToRole(user.Id, "Customer");
                shopContext.Customers.Add(new Customer
                {
                    CustomerId = user.Id,
                    FirstName = "Eef",
                    LastName = "E",
                    DoB = DateTime.Parse("1985-09-10"),
                    City = "E",
                    Zipcode = "E",
                    HouseNumber = 5
                });
                shopContext.SaveChanges();
            }

            if (!context.Users.Any(u => u.UserName == "testrep1@email.nl"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "testrep1@email.nl", Email = "testrep1@email.nl" };
                userManager.Create(user, "56#$abCD");
                userManager.AddToRole(user.Id, "Repairer");
                shopContext.Repairers.Add(new Repairer
                {
                    RepairerId = user.Id,
                    FirstName = "Frans",
                    LastName = "F",
                    DoB = DateTime.Parse("1985-11-15"),
                    Wage = 14.70
                });
                shopContext.SaveChanges();
            }

            if (!context.Users.Any(u => u.UserName == "testrep2@email.nl"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "testrep2@email.nl", Email = "testrep2@email.nl" };
                userManager.Create(user, "67#$abCD");
                userManager.AddToRole(user.Id, "Repairer");
                shopContext.Repairers.Add(new Repairer
                {
                    RepairerId = user.Id,
                    FirstName = "Gerard",
                    LastName = "G",
                    DoB = DateTime.Parse("1993-10-02"),
                    Wage = 14.70
                });
                shopContext.SaveChanges();
            }

            if (!context.Users.Any(u => u.UserName == "testrep3@email.nl"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "testrep3@email.nl", Email = "testrep3@email.nl" };
                userManager.Create(user, "78#$abCD");
                userManager.AddToRole(user.Id, "Repairer");
                shopContext.Repairers.Add(new Repairer
                {
                    RepairerId = user.Id,
                    FirstName = "Harry",
                    LastName = "H",
                    DoB = DateTime.Parse("1989-03-03"),
                    Wage = 14.70
                });
                shopContext.SaveChanges();
            }

            if (!context.Users.Any(u => u.UserName == "testrep4@email.nl"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "testrep4@email.nl", Email = "testrep4@email.nl" };
                userManager.Create(user, "89#$abCD");
                userManager.AddToRole(user.Id, "Repairer");
                shopContext.Repairers.Add(new Repairer
                {
                    RepairerId = user.Id,
                    FirstName = "Ian",
                    LastName = "I",
                    DoB = DateTime.Parse("1995-05-11"),
                    Wage = 14.70
                });
                shopContext.SaveChanges();
            }

            if (!context.Users.Any(u => u.UserName == "testrep5@email.nl"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "testrep5@email.nl", Email = "testrep5@email.nl" };
                userManager.Create(user, "90#$abCD");
                userManager.AddToRole(user.Id, "Repairer");
                shopContext.Repairers.Add(new Repairer
                {
                    RepairerId = user.Id,
                    FirstName = "Jan",
                    LastName = "J",
                    DoB = DateTime.Parse("1987-07-27"),
                    Wage = 14.70
                });
                shopContext.SaveChanges();
            }


            var custid = shopContext.Customers.ToList();
            var repId = shopContext.Repairers.ToList();

            List<Assignment> assignments = new List<Assignment>
            {
                new Assignment() { Start = new DateTime(2000, 01, 1), End = new DateTime(2001, 02, 2), Status = Status.Pending, HoursWorked = 1, Customer = custid[0], Repairer = repId[0] },
                new Assignment() { Start = new DateTime(2001, 02, 2), End = new DateTime(2002, 03, 3), Status = Status.in_progress, HoursWorked = 2, Customer = custid[1], Repairer = repId[1] },
                new Assignment() { Start = new DateTime(2002, 03, 3), End = new DateTime(2003, 04, 4), Status = Status.Done, HoursWorked = 3, Customer = custid[2], Repairer = repId[2] },
                new Assignment() { Start = new DateTime(2003, 04, 4), End = new DateTime(2004, 05, 5), Status = Status.Waiting_for_parts, HoursWorked = 4, Customer = custid[3], Repairer = repId[3] },
                new Assignment() { Start = new DateTime(2004, 05, 5), End = new DateTime(2005, 06, 6), Status = Status.Done, HoursWorked = 5, Customer = custid[4], Repairer = repId[4] }
            };

            if (shopContext.Assignments.Count() == 0)
            {
                assignments.ForEach(a => shopContext.Assignments.Add(a));
                shopContext.SaveChanges();
            }

            var assignid = shopContext.Assignments.ToList();

            List<Product> products = new List<Product>
            {
                new Product{Name="K", Price=200, Assignment = assignid[0] },
                new Product{Name="L", Price=250, Assignment = assignid[1] },
                new Product{Name="M", Price=200, Assignment = assignid[2] },
                new Product{Name="N", Price=300, Assignment = assignid[3] },
                new Product{Name="O", Price=50, Assignment = assignid[4] }
            };

            if (shopContext.Assignments.Count() == 0)
            {
                products.ForEach(p => shopContext.Products.Add(p));
                context.SaveChanges();
            }
        }
    }
}
