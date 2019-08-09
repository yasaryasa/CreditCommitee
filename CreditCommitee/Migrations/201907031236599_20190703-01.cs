namespace CreditCommitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2019070301 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CommiteeItems", new[] { "programmeId" });
            AlterColumn("dbo.CommiteeItems", "itemName", c => c.String(nullable: false, maxLength: 450, unicode: false));
            CreateIndex("dbo.CommiteeItems", new[] { "programmeId", "itemName" }, unique: true, name: "IX_ItemName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CommiteeItems", "IX_ItemName");
            AlterColumn("dbo.CommiteeItems", "itemName", c => c.String(nullable: false));
            CreateIndex("dbo.CommiteeItems", "programmeId");
        }
    }
}
