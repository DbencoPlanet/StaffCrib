namespace AcademicStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepubs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.PublicationRequests", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Publications", "UserId");
            CreateIndex("dbo.PublicationRequests", "UserId");
            AddForeignKey("dbo.Publications", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PublicationRequests", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PublicationRequests", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Publications", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.PublicationRequests", new[] { "UserId" });
            DropIndex("dbo.Publications", new[] { "UserId" });
            DropColumn("dbo.PublicationRequests", "UserId");
            DropColumn("dbo.Publications", "UserId");
        }
    }
}
