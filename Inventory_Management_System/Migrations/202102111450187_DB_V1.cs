namespace Inventory_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB_V1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HopClassifications",
                c => new
                    {
                        HopClassificationID = c.Int(nullable: false, identity: true),
                        HopClassificationType = c.String(),
                    })
                .PrimaryKey(t => t.HopClassificationID);
            
            CreateTable(
                "dbo.Hops",
                c => new
                    {
                        HopID = c.Int(nullable: false, identity: true),
                        HopName = c.String(),
                        HopProducer = c.String(),
                        HopProductionDate = c.DateTime(nullable: false),
                        HopSerialNumber = c.String(),
                        HopVolume = c.String(),
                        AlphaAcid = c.Int(nullable: false),
                        BetaAcid = c.Int(nullable: false),
                        HopNotes = c.String(),
                        HopClassificationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HopID)
                .ForeignKey("dbo.HopClassifications", t => t.HopClassificationID, cascadeDelete: true)
                .Index(t => t.HopClassificationID);
            
            CreateTable(
                "dbo.MaltClassifications",
                c => new
                    {
                        MaltClassificationID = c.Int(nullable: false, identity: true),
                        MaltClassificationType = c.String(),
                    })
                .PrimaryKey(t => t.MaltClassificationID);
            
            CreateTable(
                "dbo.Malts",
                c => new
                    {
                        MaltID = c.Int(nullable: false, identity: true),
                        MaltName = c.String(),
                        MaltProducer = c.String(),
                        MaltProductionDate = c.DateTime(nullable: false),
                        MaltSerialNumber = c.String(),
                        MaltVolume = c.String(),
                        DiasticPower = c.Int(nullable: false),
                        SRM = c.Int(nullable: false),
                        MaltClassificationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaltID)
                .ForeignKey("dbo.MaltClassifications", t => t.MaltClassificationID, cascadeDelete: true)
                .Index(t => t.MaltClassificationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Malts", "MaltClassificationID", "dbo.MaltClassifications");
            DropForeignKey("dbo.Hops", "HopClassificationID", "dbo.HopClassifications");
            DropIndex("dbo.Malts", new[] { "MaltClassificationID" });
            DropIndex("dbo.Hops", new[] { "HopClassificationID" });
            DropTable("dbo.Malts");
            DropTable("dbo.MaltClassifications");
            DropTable("dbo.Hops");
            DropTable("dbo.HopClassifications");
        }
    }
}
