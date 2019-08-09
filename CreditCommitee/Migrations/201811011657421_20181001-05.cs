namespace CreditCommitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018100105 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommiteeItems", "itemStartTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CommiteeItems", "itemStartTime");
        }
    }
}
