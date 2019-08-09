namespace CreditCommitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018151105 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommiteeProgrammes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        programmeName = c.String(),
                        programmeDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        createdBy = c.String(),
                        createDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.CommiteeItems", "commiteeProgramme_id", c => c.Int());
            CreateIndex("dbo.CommiteeItems", "commiteeProgramme_id");
            AddForeignKey("dbo.CommiteeItems", "commiteeProgramme_id", "dbo.CommiteeProgrammes", "id");
            DropTable("dbo.CommiteePrograms");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CommiteePrograms",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        programName = c.String(),
                        programDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        createdBy = c.String(),
                        createDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.id);
            
            DropForeignKey("dbo.CommiteeItems", "commiteeProgramme_id", "dbo.CommiteeProgrammes");
            DropIndex("dbo.CommiteeItems", new[] { "commiteeProgramme_id" });
            DropColumn("dbo.CommiteeItems", "commiteeProgramme_id");
            DropTable("dbo.CommiteeProgrammes");
        }
    }
}
