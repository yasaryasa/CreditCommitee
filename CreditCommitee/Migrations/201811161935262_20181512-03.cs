namespace CreditCommitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018151203 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommiteeItems", "itemEndDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CommiteeItems", "itemStartDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.CommiteeItems", "itemDate");
            DropColumn("dbo.CommiteeItems", "itemStartTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CommiteeItems", "itemStartTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CommiteeItems", "itemDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.CommiteeItems", "itemStartDate");
            DropColumn("dbo.CommiteeItems", "itemEndDate");
        }
    }
}
