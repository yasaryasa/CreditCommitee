namespace CreditCommitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018100101 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommiteeItems",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        itemName = c.String(),
                        itemDate = c.DateTime(nullable: false),
                        createdBy = c.String(),
                        createDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CommiteeItems");
        }
    }
}
