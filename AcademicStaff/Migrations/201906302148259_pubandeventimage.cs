namespace AcademicStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pubandeventimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "Image", c => c.String());
            AddColumn("dbo.Events", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Image");
            DropColumn("dbo.Publications", "Image");
        }
    }
}
