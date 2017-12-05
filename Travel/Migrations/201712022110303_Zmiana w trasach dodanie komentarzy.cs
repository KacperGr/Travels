namespace Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zmianawtrasachdodaniekomentarzy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        RouteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .Index(t => t.RouteId);
            
            AddColumn("dbo.Routes", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "RouteId", "dbo.Routes");
            DropIndex("dbo.Comments", new[] { "RouteId" });
            DropColumn("dbo.Routes", "Description");
            DropTable("dbo.Comments");
        }
    }
}
