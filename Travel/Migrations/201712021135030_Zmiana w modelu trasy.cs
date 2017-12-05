namespace Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zmianawmodelutrasy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "DriverId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "DriverId");
        }
    }
}
