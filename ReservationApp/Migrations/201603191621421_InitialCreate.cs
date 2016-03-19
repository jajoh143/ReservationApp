namespace ReservationApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Phone = c.String(),
                        StrAdd = c.String(),
                        City = c.String(),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        ReservationID = c.Int(nullable: false, identity: true),
                        ClientID = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                        ClientName = c.String(),
                        EmployeeName = c.String(),
                    })
                .PrimaryKey(t => t.ReservationID)
                .ForeignKey("dbo.Client", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Occupation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservation", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Reservation", "ClientID", "dbo.Client");
            DropIndex("dbo.Reservation", new[] { "EmployeeID" });
            DropIndex("dbo.Reservation", new[] { "ClientID" });
            DropTable("dbo.Employee");
            DropTable("dbo.Reservation");
            DropTable("dbo.Client");
        }
    }
}
