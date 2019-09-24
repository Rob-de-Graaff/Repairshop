namespace Reparatieshop.ASPMigrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Reparatieshop.Models;
    using System;
    using System.Data.Entity;
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
        }
    }
}
