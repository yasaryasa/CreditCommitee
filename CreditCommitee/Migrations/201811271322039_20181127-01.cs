namespace CreditCommitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018112701 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CommiteeItems", "itemEndDate");
            DropColumn("dbo.CommiteeItems", "itemStartDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CommiteeItems", "itemStartDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CommiteeItems", "itemEndDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
