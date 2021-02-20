using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Inventory_Management_System.Models
{
    /// <summary>
    /// The following defines what classifications malt fals under
    /// </summary>
    public class MaltClassification
    {
        public int MaltClassificationID { get; set; }
        public string MaltClassificationType { get; set; }
        //AMalt classification can have many types
        //Ask christine about a malt total volume
        public ICollection<Malt>Malts { get; set; }

    }
    //DTO is used to safely transfer data
    public class MaltClassificationDto
    {
        public int MaltClassificationID { get; set; }

        //[DisplayName("Malt Name")]
        public string MaltClassificationType { get; set; }
    }


}