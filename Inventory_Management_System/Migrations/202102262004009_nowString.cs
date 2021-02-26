namespace Inventory_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nowString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Hops", "HopProductionDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Hops", "HopProductionDate", c => c.DateTime(nullable: false));
        }
    }
}
