namespace AcademicStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageadjust : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "ImageUrl", c => c.String());
            AddColumn("dbo.Schools", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schools", "ImageUrl");
            DropColumn("dbo.Departments", "ImageUrl");
        }
    }
}
