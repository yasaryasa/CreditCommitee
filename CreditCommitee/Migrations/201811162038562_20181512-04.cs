namespace CreditCommitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018151204 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CommiteeItems", "itemName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CommiteeItems", "itemName", c => c.String());
        }
    }
}
