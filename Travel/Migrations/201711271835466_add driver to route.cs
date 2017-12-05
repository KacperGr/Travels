namespace Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddrivertoroute : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUserRoutes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserRoutes", "Route_RouteId", "dbo.Routes");
            DropIndex("dbo.ApplicationUserRoutes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserRoutes", new[] { "Route_RouteId" });
            AddColumn("dbo.Routes", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Routes", "Driver_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Route_RouteId", c => c.Int());
            CreateIndex("dbo.Routes", "ApplicationUser_Id");
            CreateIndex("dbo.Routes", "Driver_Id");
            CreateIndex("dbo.AspNetUsers", "Route_RouteId");
            AddForeignKey("dbo.Routes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Routes", "Driver_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Route_RouteId", "dbo.Routes", "RouteId");
            DropTable("dbo.ApplicationUserRoutes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserRoutes",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Route_RouteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Route_RouteId });
            
            DropForeignKey("dbo.AspNetUsers", "Route_RouteId", "dbo.Routes");
            DropForeignKey("dbo.Routes", "Driver_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Routes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Route_RouteId" });
            DropIndex("dbo.Routes", new[] { "Driver_Id" });
            DropIndex("dbo.Routes", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "Route_RouteId");
            DropColumn("dbo.Routes", "Driver_Id");
            DropColumn("dbo.Routes", "ApplicationUser_Id");
            CreateIndex("dbo.ApplicationUserRoutes", "Route_RouteId");
            CreateIndex("dbo.ApplicationUserRoutes", "ApplicationUser_Id");
            AddForeignKey("dbo.ApplicationUserRoutes", "Route_RouteId", "dbo.Routes", "RouteId", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserRoutes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
