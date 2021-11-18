namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Rentals", name: "Movie_Id", newName: "Book_Id");
            RenameIndex(table: "dbo.Rentals", name: "IX_Movie_Id", newName: "IX_Book_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Rentals", name: "IX_Book_Id", newName: "IX_Movie_Id");
            RenameColumn(table: "dbo.Rentals", name: "Book_Id", newName: "Movie_Id");
        }
    }
}
