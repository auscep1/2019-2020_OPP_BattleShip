namespace KronoBattleship.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class playerMementoState : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "State");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "State", c => c.String());
        }
    }
}
