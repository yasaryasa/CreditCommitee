namespace CreditCommitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018112702 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommiteeItems", "itemEndDate", c => c.DateTime());
            AddColumn("dbo.CommiteeItems", "itemStartDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CommiteeItems", "itemStartDate");
            DropColumn("dbo.CommiteeItems", "itemEndDate");
        }
    }
}
