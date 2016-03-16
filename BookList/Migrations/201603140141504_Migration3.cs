namespace BookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PersonBooks", "PersonBookID", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PersonBooks", "PersonBookID", c => c.Int(nullable: false));
        }
    }
}
