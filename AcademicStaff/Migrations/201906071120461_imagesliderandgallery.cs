namespace AcademicStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imagesliderandgallery : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageGalleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        ContentType = c.String(),
                        Content = c.Binary(),
                        GalleryAlt = c.String(),
                        CurrentGallery = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImageModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        ContentType = c.String(),
                        ImageContent = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImageSliders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        ContentType = c.String(),
                        Content = c.Binary(),
                        SliderAlt = c.String(),
                        TextOne = c.String(),
                        TextTwo = c.String(),
                        TextThree = c.String(),
                        CurrentSlider = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImageSliders");
            DropTable("dbo.ImageModels");
            DropTable("dbo.ImageGalleries");
        }
    }
}
