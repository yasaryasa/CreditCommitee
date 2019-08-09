namespace CreditCommitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018111501 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Workers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Position = c.String(),
                        Office = c.String(),
                        Extn = c.String(),
                        Salary = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
