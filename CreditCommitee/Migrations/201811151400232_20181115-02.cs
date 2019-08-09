namespace CreditCommitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018111502 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommiteePrograms",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        programName = c.String(),
                        programDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        createdBy = c.String(),
                        createDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CommiteePrograms");
        }
    }
}
