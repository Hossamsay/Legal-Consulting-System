namespace LegalConsulting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RowVersion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CaseDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClientID = c.Int(nullable: false),
                        CaseID = c.Int(nullable: false),
                        LawyerID = c.Int(nullable: false),
                        Hours = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Case", t => t.CaseID, cascadeDelete: true)
                .ForeignKey("dbo.Client", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Lawyer", t => t.LawyerID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.CaseID)
                .Index(t => t.LawyerID);
            
            CreateTable(
                "dbo.Case",
                c => new
                    {
                        CaseID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        CaseName = c.String(),
                        Type = c.Int(nullable: false),
                        Description = c.String(),
                        StartDate = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.CaseID);
            
            CreateTable(
                "dbo.Lawyer",
                c => new
                    {
                        LawyerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        HiringDate = c.DateTime(),
                        PricePerHour = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.LawyerID);
            
            CreateTable(
                "dbo.OfficeLocation",
                c => new
                    {
                        LawyerID = c.Int(nullable: false),
                        Location = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.LawyerID)
                .ForeignKey("dbo.Lawyer", t => t.LawyerID)
                .Index(t => t.LawyerID);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        JobTitle = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CaseAdminstrator",
                c => new
                    {
                        CaseID = c.Int(nullable: false),
                        LawyerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CaseID, t.LawyerID })
                .ForeignKey("dbo.Case", t => t.CaseID, cascadeDelete: true)
                .ForeignKey("dbo.Lawyer", t => t.LawyerID, cascadeDelete: true)
                .Index(t => t.CaseID)
                .Index(t => t.LawyerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CaseDetail", "LawyerID", "dbo.Lawyer");
            DropForeignKey("dbo.CaseDetail", "ClientID", "dbo.Client");
            DropForeignKey("dbo.CaseAdminstrator", "LawyerID", "dbo.Lawyer");
            DropForeignKey("dbo.CaseAdminstrator", "CaseID", "dbo.Case");
            DropForeignKey("dbo.OfficeLocation", "LawyerID", "dbo.Lawyer");
            DropForeignKey("dbo.CaseDetail", "CaseID", "dbo.Case");
            DropIndex("dbo.CaseAdminstrator", new[] { "LawyerID" });
            DropIndex("dbo.CaseAdminstrator", new[] { "CaseID" });
            DropIndex("dbo.OfficeLocation", new[] { "LawyerID" });
            DropIndex("dbo.CaseDetail", new[] { "LawyerID" });
            DropIndex("dbo.CaseDetail", new[] { "CaseID" });
            DropIndex("dbo.CaseDetail", new[] { "ClientID" });
            DropTable("dbo.CaseAdminstrator");
            DropTable("dbo.Client");
            DropTable("dbo.OfficeLocation");
            DropTable("dbo.Lawyer");
            DropTable("dbo.Case");
            DropTable("dbo.CaseDetail");
        }
    }
}
