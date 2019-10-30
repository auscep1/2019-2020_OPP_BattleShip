namespace KronoBattleship.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class planecoordinates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShipCoordinates", "Plane_PlaneId", "dbo.Planes");
            DropIndex("dbo.ShipCoordinates", new[] { "Plane_PlaneId" });
            CreateTable(
                "dbo.PlaneCoordinates",
                c => new
                    {
                        PlaneCoordinatesId = c.Int(nullable: false, identity: true),
                        x = c.Int(nullable: false),
                        y = c.Int(nullable: false),
                        Alive = c.Boolean(nullable: false),
                        PlaneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlaneCoordinatesId)
                .ForeignKey("dbo.Planes", t => t.PlaneId, cascadeDelete: true)
                .Index(t => t.PlaneId);
            
            DropColumn("dbo.ShipCoordinates", "Plane_PlaneId");
           // RenameColumn(table: "dbo.PlaneCoordinates", name: "Plane_PlaneId", newName: "PlaneId");

        }

        public override void Down()
        {
            AddColumn("dbo.ShipCoordinates", "Plane_PlaneId", c => c.Int());
            DropForeignKey("dbo.PlaneCoordinates", "PlaneId", "dbo.Planes");
            DropIndex("dbo.PlaneCoordinates", new[] { "Plane_PlaneId" });
            DropTable("dbo.PlaneCoordinates");
           // RenameColumn(table: "dbo.PlaneCoordinates", name: "PlaneId", newName: "Plane_PlaneId");
            CreateIndex("dbo.ShipCoordinates", "Plane_PlaneId");
            AddForeignKey("dbo.ShipCoordinates", "Plane_PlaneId", "dbo.Planes", "PlaneId");
        }
    }
}
