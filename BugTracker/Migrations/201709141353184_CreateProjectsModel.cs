namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateProjectsModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false),
                        Url = c.String(),
                        CreatedById = c.String(nullable: false, maxLength: 128),
                        UpdatedById = c.String(maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projects", "CreatedById", "dbo.AspNetUsers");
            DropIndex("dbo.Projects", new[] { "UpdatedById" });
            DropIndex("dbo.Projects", new[] { "CreatedById" });
            DropTable("dbo.Projects");
        }
    }
}
