namespace Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zmianawtrasie : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Routes", new[] { "Driver_Id" });
            DropColumn("dbo.Routes", "DriverId");
            RenameColumn(table: "dbo.Routes", name: "Driver_Id", newName: "DriverId");
            AlterColumn("dbo.Routes", "DriverId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Routes", "DriverId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Routes", new[] { "DriverId" });
            AlterColumn("dbo.Routes", "DriverId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Routes", name: "DriverId", newName: "Driver_Id");
            AddColumn("dbo.Routes", "DriverId", c => c.Int(nullable: false));
            CreateIndex("dbo.Routes", "Driver_Id");
        }
    }
}
