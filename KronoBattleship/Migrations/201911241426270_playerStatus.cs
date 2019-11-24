namespace KronoBattleship.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class playerStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "State", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "State");
        }
    }
}
