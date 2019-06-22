namespace AcademicStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventcorrection : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "End", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "End", c => c.DateTime());
        }
    }
}
