namespace AcademicStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailfix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Profiles", "EmailAddres");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profiles", "EmailAddres", c => c.String());
        }
    }
}
