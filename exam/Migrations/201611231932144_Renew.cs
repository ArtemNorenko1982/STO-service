namespace exam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Renew : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Orders", "customerId", "dbo.Customers");
            //DropIndex("dbo.Orders", new[] { "customerId" });
            //DropColumn("dbo.Orders", "customerId");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Orders", "customerId", c => c.Int(nullable: false));
            //CreateIndex("dbo.Orders", "customerId");
            //AddForeignKey("dbo.Orders", "customerId", "dbo.Customers", "ID", cascadeDelete: true);
        }
    }
}
