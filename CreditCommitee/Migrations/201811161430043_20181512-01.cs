namespace CreditCommitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018151201 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CommiteeProgrammes", "programmeName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CommiteeProgrammes", "programmeName", c => c.String());
        }
    }
}
