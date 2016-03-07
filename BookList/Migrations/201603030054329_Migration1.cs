namespace BookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonBooks", "BookDateEntered", c => c.DateTime(nullable: false));
            DropColumn("dbo.BooksReads", "BookEnteredDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BooksReads", "BookEnteredDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.PersonBooks", "BookDateEntered");
        }
    }
}
