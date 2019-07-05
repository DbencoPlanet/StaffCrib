namespace AcademicStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageupdatede : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Departments", "ImageUrl");
            DropColumn("dbo.Schools", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schools", "ImageUrl", c => c.String());
            AddColumn("dbo.Departments", "ImageUrl", c => c.String());
        }
    }
}
