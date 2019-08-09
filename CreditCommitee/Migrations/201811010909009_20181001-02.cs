namespace CreditCommitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018100102 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CommiteeItems", "itemDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CommiteeItems", "createDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CommiteeItems", "createDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CommiteeItems", "itemDate", c => c.DateTime(nullable: false));
        }
    }
}
