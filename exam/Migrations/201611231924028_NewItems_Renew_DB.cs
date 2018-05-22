namespace exam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewItems_Renew_DB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AutoParts", "CarModelID", "dbo.CarModels");
            DropForeignKey("dbo.Orders", "servicesId", "dbo.Services");
            DropIndex("dbo.AutoParts", new[] { "CarModelID" });
            DropIndex("dbo.Orders", new[] { "servicesId" });
            CreateTable(
                "dbo.Goods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CarModelID = c.Int(nullable: false),
                        GoodsTypeID = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CarModels", t => t.CarModelID, cascadeDelete: true)
                .ForeignKey("dbo.GoodsTypes", t => t.GoodsTypeID, cascadeDelete: true)
                .Index(t => t.CarModelID)
                .Index(t => t.GoodsTypeID);
            
            CreateTable(
                "dbo.GoodsTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderCotnexts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        goodsID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Suma = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Goods", t => t.goodsID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: false)
                .Index(t => t.OrderId)
                .Index(t => t.goodsID);
            
            //AddColumn("dbo.Orders", "customerId", c => c.Int(nullable: false));
            //CreateIndex("dbo.Orders", "customerId");
            //AddForeignKey("dbo.Orders", "customerId", "dbo.Customers", "ID", cascadeDelete: true);
            DropColumn("dbo.Orders", "servicesId");
            DropColumn("dbo.Orders", "Count");
            DropColumn("dbo.Orders", "Suma");
            DropTable("dbo.AutoParts");
            DropTable("dbo.Services");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AutoParts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CarModelID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Orders", "Suma", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Count", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "servicesId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderCotnexts", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderCotnexts", "goodsID", "dbo.Goods");
            DropForeignKey("dbo.Orders", "customerId", "dbo.Customers");
            DropForeignKey("dbo.Goods", "GoodsTypeID", "dbo.GoodsTypes");
            DropForeignKey("dbo.Goods", "CarModelID", "dbo.CarModels");
            DropIndex("dbo.OrderCotnexts", new[] { "goodsID" });
            DropIndex("dbo.OrderCotnexts", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "customerId" });
            DropIndex("dbo.Goods", new[] { "GoodsTypeID" });
            DropIndex("dbo.Goods", new[] { "CarModelID" });
            DropColumn("dbo.Orders", "customerId");
            DropTable("dbo.OrderCotnexts");
            DropTable("dbo.GoodsTypes");
            DropTable("dbo.Goods");
            CreateIndex("dbo.Orders", "servicesId");
            CreateIndex("dbo.AutoParts", "CarModelID");
            AddForeignKey("dbo.Orders", "servicesId", "dbo.Services", "ID", cascadeDelete: true);
            AddForeignKey("dbo.AutoParts", "CarModelID", "dbo.CarModels", "ID", cascadeDelete: true);
        }
    }
}
