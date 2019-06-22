namespace AcademicStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Publication : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Publications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Abstract = c.String(),
                        ProfileId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.ProfileId)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.PublicationRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PublicationId = c.Int(),
                        ProfileId = c.Int(),
                        Name = c.String(),
                        Email = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.ProfileId)
                .ForeignKey("dbo.Publications", t => t.PublicationId)
                .Index(t => t.PublicationId)
                .Index(t => t.ProfileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PublicationRequests", "PublicationId", "dbo.Publications");
            DropForeignKey("dbo.PublicationRequests", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.Publications", "ProfileId", "dbo.Profiles");
            DropIndex("dbo.PublicationRequests", new[] { "ProfileId" });
            DropIndex("dbo.PublicationRequests", new[] { "PublicationId" });
            DropIndex("dbo.Publications", new[] { "ProfileId" });
            DropTable("dbo.PublicationRequests");
            DropTable("dbo.Publications");
        }
    }
}
