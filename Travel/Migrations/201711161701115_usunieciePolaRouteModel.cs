namespace Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usunieciePolaRouteModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Routes", "PassengerCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Routes", "PassengerCount", c => c.Int(nullable: false));
        }
    }
}
