using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace Inventory_Management_System.Models
{//NOTE YOU WILL NEED TO ADD INFO FOR LOG IN HERE
    public class InventoryDataContext : DbContext
    {
        public InventoryDataContext() : base("name=InventoryDataContext")
            {
            }
        public static InventoryDataContext Create()
        {
            return new InventoryDataContext();
        }
        public DbSet<Hop> Hops { get; set; }
        public DbSet<Malt> Malts { get; set; }
        public DbSet<HopClassification> HopClassifications { get; set; }

        public DbSet<MaltClassification> MaltClassifications { get; set; }


    }
}