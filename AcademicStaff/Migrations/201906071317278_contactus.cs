namespace AcademicStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ProfileId = c.Int(),
                        Name = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Subject = c.String(nullable: false),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.ProfileId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ProfileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactUs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContactUs", "ProfileId", "dbo.Profiles");
            DropIndex("dbo.ContactUs", new[] { "ProfileId" });
            DropIndex("dbo.ContactUs", new[] { "UserId" });
            DropTable("dbo.ContactUs");
        }
    }
}
