namespace BookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.WishLists");
            AddColumn("dbo.WishLists", "Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.WishLists", "BookReadID", c => c.Int(nullable: false));
            AddColumn("dbo.WishLists", "BookDateEntered", c => c.DateTime(nullable: false));
            AlterColumn("dbo.WishLists", "WishListID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.WishLists", "Id");
            //DropColumn("dbo.PersonBooks", "WishListID");
            DropColumn("dbo.WishLists", "WishListBookName");
            DropColumn("dbo.WishLists", "WishListBookAuthor");
            DropColumn("dbo.WishLists", "WishListBookGenre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WishLists", "WishListBookGenre", c => c.String());
            AddColumn("dbo.WishLists", "WishListBookAuthor", c => c.String());
            AddColumn("dbo.WishLists", "WishListBookName", c => c.String());
            AddColumn("dbo.PersonBooks", "WishListID", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.WishLists");
            AlterColumn("dbo.WishLists", "WishListID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.WishLists", "BookDateEntered");
            DropColumn("dbo.WishLists", "BookReadID");
            DropColumn("dbo.WishLists", "Id");
            AddPrimaryKey("dbo.WishLists", "WishListID");
        }
    }
}
