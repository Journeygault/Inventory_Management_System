using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_Management_System.Models
{
    public class HopClassification
    {
            //Thus coud be a problem
            public int HopClassificationID { get; set; }
            public string HopClassificationType { get; set; }
            //A Malt classification can have many types
            //Ask christine about a Hop total volume
            public ICollection<Hop> Hops { get; set; }
    }
    public class HopClassificationDto
    {
        public int HopClassificationID { get; set; }

        //[DisplayName("Team Name")]
        public string HopClassificationType { get; set; }
    }
}