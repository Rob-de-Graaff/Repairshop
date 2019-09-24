namespace Reparatieshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        AssignmentId = c.Int(nullable: false, identity: true),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Description = c.String(),
                        HoursWorked = c.Int(nullable: false),
                        Customer_CustomerId = c.Int(),
                        Repairer_RepairerId = c.Int(),
                    })
                .PrimaryKey(t => t.AssignmentId)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId)
                .ForeignKey("dbo.Repairers", t => t.Repairer_RepairerId)
                .Index(t => t.Customer_CustomerId)
                .Index(t => t.Repairer_RepairerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DoB = c.DateTime(nullable: false),
                        City = c.String(nullable: false),
                        Street = c.String(),
                        Zipcode = c.String(nullable: false),
                        HouseNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        Assignment_AssignmentId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Assignments", t => t.Assignment_AssignmentId)
                .Index(t => t.Assignment_AssignmentId);
            
            CreateTable(
                "dbo.Repairers",
                c => new
                    {
                        RepairerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DoB = c.DateTime(nullable: false),
                        Wage = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.RepairerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "Repairer_RepairerId", "dbo.Repairers");
            DropForeignKey("dbo.Products", "Assignment_AssignmentId", "dbo.Assignments");
            DropForeignKey("dbo.Assignments", "Customer_CustomerId", "dbo.Customers");
            DropIndex("dbo.Products", new[] { "Assignment_AssignmentId" });
            DropIndex("dbo.Assignments", new[] { "Repairer_RepairerId" });
            DropIndex("dbo.Assignments", new[] { "Customer_CustomerId" });
            DropTable("dbo.Repairers");
            DropTable("dbo.Products");
            DropTable("dbo.Customers");
            DropTable("dbo.Assignments");
        }
    }
}
