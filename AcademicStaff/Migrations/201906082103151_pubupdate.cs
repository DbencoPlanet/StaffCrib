namespace AcademicStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pubupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publications", "DateCreated");
        }
    }
}
