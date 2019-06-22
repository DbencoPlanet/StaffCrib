namespace AcademicStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deptandschimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "Image", c => c.String());
            AddColumn("dbo.Schools", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schools", "Image");
            DropColumn("dbo.Departments", "Image");
        }
    }
}
