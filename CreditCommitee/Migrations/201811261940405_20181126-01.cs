namespace CreditCommitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018112601 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommiteeProgrammes", "programmeStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CommiteeProgrammes", "programmeStatus");
        }
    }
}
