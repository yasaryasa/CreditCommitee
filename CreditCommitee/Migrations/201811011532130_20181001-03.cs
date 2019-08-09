namespace CreditCommitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018100103 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommiteeItems", "itemOrder", c => c.Int(nullable: false));
            AddColumn("dbo.CommiteeItems", "itemStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CommiteeItems", "itemStatus");
            DropColumn("dbo.CommiteeItems", "itemOrder");
        }
    }
}
