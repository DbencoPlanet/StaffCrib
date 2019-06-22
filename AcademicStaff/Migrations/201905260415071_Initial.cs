namespace AcademicStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailyReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProfileId = c.Int(),
                        UserId = c.String(maxLength: 128),
                        ReportDate = c.DateTime(),
                        DateCreated = c.DateTime(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.ProfileId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ProfileId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Photo = c.String(),
                        Nationality = c.String(),
                        StateOfOrigin = c.String(),
                        EmailAddres = c.String(),
                        LocalGov = c.String(),
                        Surname = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        OtherNames = c.String(),
                        PhoneNo = c.String(),
                        DOB = c.DateTime(),
                        CurrentAppDate = c.DateTime(),
                        Rank = c.Int(nullable: false),
                        PlaceOfBirth = c.String(),
                        Religion = c.String(),
                        Address = c.String(),
                        SchoolId = c.Int(),
                        DepartmentId = c.Int(),
                        StaffNo = c.String(),
                        Biography = c.String(),
                        MaritalStatus = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Schools", t => t.SchoolId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SchoolId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortCode = c.String(),
                        SchoolId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.SchoolId)
                .Index(t => t.SchoolId);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Surname = c.String(),
                        FirstName = c.String(),
                        OtherNames = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DocName = c.String(),
                        UserId = c.String(maxLength: 128),
                        ProfileId = c.Int(),
                        SenderId = c.Int(),
                        ReceiverId = c.Int(),
                        Remark = c.String(),
                        DocUrl = c.String(),
                        DocType = c.Int(nullable: false),
                        DocType2 = c.Int(nullable: false),
                        DateUploaded = c.DateTime(),
                        DateSent = c.DateTime(),
                        DateReceive = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.ProfileId)
                .ForeignKey("dbo.Profiles", t => t.ReceiverId)
                .ForeignKey("dbo.Profiles", t => t.SenderId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ProfileId)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        DIscription = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(),
                        Color = c.String(),
                        UserId = c.String(),
                        GeneralEvent = c.Boolean(),
                        IsFullDay = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LocalGovs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LGAName = c.String(),
                        StatesId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.States", t => t.StatesId)
                .Index(t => t.StatesId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        TaskId = c.Int(),
                        ProfileId = c.Int(),
                        Content = c.String(),
                        Comment = c.String(),
                        DateReported = c.DateTime(),
                        DateModified = c.DateTime(),
                        DateCommented = c.DateTime(),
                        TaskStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.ProfileId)
                .ForeignKey("dbo.Tasks", t => t.TaskId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TaskId)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ProfileId = c.Int(),
                        TaskTitle = c.String(),
                        TaskDiscription = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(),
                        TaskStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.ProfileId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reports", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tasks", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reports", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.Tasks", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.Reports", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.LocalGovs", "StatesId", "dbo.States");
            DropForeignKey("dbo.Documents", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Documents", "SenderId", "dbo.Profiles");
            DropForeignKey("dbo.Documents", "ReceiverId", "dbo.Profiles");
            DropForeignKey("dbo.Documents", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.DailyReports", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DailyReports", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.Profiles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Profiles", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Profiles", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "SchoolId", "dbo.Schools");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Tasks", new[] { "ProfileId" });
            DropIndex("dbo.Tasks", new[] { "UserId" });
            DropIndex("dbo.Reports", new[] { "ProfileId" });
            DropIndex("dbo.Reports", new[] { "TaskId" });
            DropIndex("dbo.Reports", new[] { "UserId" });
            DropIndex("dbo.LocalGovs", new[] { "StatesId" });
            DropIndex("dbo.Documents", new[] { "ReceiverId" });
            DropIndex("dbo.Documents", new[] { "SenderId" });
            DropIndex("dbo.Documents", new[] { "ProfileId" });
            DropIndex("dbo.Documents", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Departments", new[] { "SchoolId" });
            DropIndex("dbo.Profiles", new[] { "DepartmentId" });
            DropIndex("dbo.Profiles", new[] { "SchoolId" });
            DropIndex("dbo.Profiles", new[] { "UserId" });
            DropIndex("dbo.DailyReports", new[] { "UserId" });
            DropIndex("dbo.DailyReports", new[] { "ProfileId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Tasks");
            DropTable("dbo.Reports");
            DropTable("dbo.States");
            DropTable("dbo.LocalGovs");
            DropTable("dbo.Events");
            DropTable("dbo.Documents");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Schools");
            DropTable("dbo.Departments");
            DropTable("dbo.Profiles");
            DropTable("dbo.DailyReports");
        }
    }
}
