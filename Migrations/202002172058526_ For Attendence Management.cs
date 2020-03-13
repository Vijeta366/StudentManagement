namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForAttendenceManagement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendences",
                c => new
                    {
                        AtendenceId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        IsPresent = c.Boolean(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AtendenceId)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
            AddColumn("dbo.Students", "ClassId", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "ClassId");
            AddForeignKey("dbo.Students", "ClassId", "dbo.Classes", "ClassId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendences", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "ClassId", "dbo.Classes");
            DropIndex("dbo.Students", new[] { "ClassId" });
            DropIndex("dbo.Attendences", new[] { "StudentId" });
            DropColumn("dbo.Students", "ClassId");
            DropTable("dbo.Attendences");
        }
    }
}
