namespace CreditCommitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018151202 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommiteeItems", "commiteeProgramme_id", "dbo.CommiteeProgrammes");
            DropIndex("dbo.CommiteeItems", new[] { "commiteeProgramme_id" });
            RenameColumn(table: "dbo.CommiteeItems", name: "commiteeProgramme_id", newName: "programmeId");
            AlterColumn("dbo.CommiteeItems", "programmeId", c => c.Int(nullable: false));
            CreateIndex("dbo.CommiteeItems", "programmeId");
            AddForeignKey("dbo.CommiteeItems", "programmeId", "dbo.CommiteeProgrammes", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommiteeItems", "programmeId", "dbo.CommiteeProgrammes");
            DropIndex("dbo.CommiteeItems", new[] { "programmeId" });
            AlterColumn("dbo.CommiteeItems", "programmeId", c => c.Int());
            RenameColumn(table: "dbo.CommiteeItems", name: "programmeId", newName: "commiteeProgramme_id");
            CreateIndex("dbo.CommiteeItems", "commiteeProgramme_id");
            AddForeignKey("dbo.CommiteeItems", "commiteeProgramme_id", "dbo.CommiteeProgrammes", "id");
        }
    }
}
