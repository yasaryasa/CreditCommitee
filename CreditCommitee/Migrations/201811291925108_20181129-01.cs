namespace CreditCommitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018112901 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommiteeProgrammes", "itemEndDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CommiteeProgrammes", "programmeDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CommiteeProgrammes", "programmeDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.CommiteeProgrammes", "itemEndDate");
        }
    }
}
