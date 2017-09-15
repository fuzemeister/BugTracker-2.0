namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBugModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bugs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Details = c.String(),
                        PriorityId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Priorities", t => t.PriorityId, cascadeDelete: true)
                .Index(t => t.PriorityId);
            
            CreateTable(
                "dbo.Priorities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            Sql("INSERT INTO PRIORITIES (NAME) VALUES ('Low');");
            Sql("INSERT INTO PRIORITIES (NAME) VALUES ('Medium');");
            Sql("INSERT INTO PRIORITIES (NAME) VALUES ('High');");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bugs", "PriorityId", "dbo.Priorities");
            DropIndex("dbo.Bugs", new[] { "PriorityId" });
            DropTable("dbo.Priorities");
            DropTable("dbo.Bugs");
        }
    }
}
