namespace exam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_drop : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AutoParts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CarModelID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CarModels", t => t.CarModelID, cascadeDelete: true)
                .Index(t => t.CarModelID);
            
            CreateTable(
                "dbo.CarModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        carNumber = c.String(),
                        YearProduce = c.Int(nullable: false),
                        MyProperty = c.Int(nullable: false),
                        carmodelId = c.Int(nullable: false),
                        customerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CarModels", t => t.carmodelId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.customerId, cascadeDelete: true)
                .Index(t => t.carmodelId)
                .Index(t => t.customerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        carId = c.Int(nullable: false),
                        servicesId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Suma = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cars", t => t.carId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.servicesId, cascadeDelete: true)
                .Index(t => t.carId)
                .Index(t => t.servicesId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "servicesId", "dbo.Services");
            DropForeignKey("dbo.Orders", "carId", "dbo.Cars");
            DropForeignKey("dbo.Cars", "customerId", "dbo.Customers");
            DropForeignKey("dbo.Cars", "carmodelId", "dbo.CarModels");
            DropForeignKey("dbo.AutoParts", "CarModelID", "dbo.CarModels");
            DropIndex("dbo.Orders", new[] { "servicesId" });
            DropIndex("dbo.Orders", new[] { "carId" });
            DropIndex("dbo.Cars", new[] { "customerId" });
            DropIndex("dbo.Cars", new[] { "carmodelId" });
            DropIndex("dbo.AutoParts", new[] { "CarModelID" });
            DropTable("dbo.Services");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
            DropTable("dbo.Cars");
            DropTable("dbo.CarModels");
            DropTable("dbo.AutoParts");
        }
    }
}
