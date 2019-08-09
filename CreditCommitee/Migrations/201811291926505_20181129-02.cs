namespace CreditCommitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018112902 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommiteeProgrammes", "programmeEndDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("dbo.CommiteeProgrammes", "itemEndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CommiteeProgrammes", "itemEndDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("dbo.CommiteeProgrammes", "programmeEndDate");
        }
    }
}
