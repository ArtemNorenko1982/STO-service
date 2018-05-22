namespace exam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cars", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "MyProperty", c => c.Int(nullable: false));
        }
    }
}
