using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;//ASK
using System.ComponentModel.DataAnnotations.Schema;


namespace Inventory_Management_System.Models
{
    /// <summary>
    /// The following defines a Hop
    /// </summary>
    public class Hop
    {
        [Key]//Capital?
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HopID { get; set; }
        public string HopName { get; set; }
        public string HopProducer { get; set; }
        public string HopProductionDate { get; set; }//Cant get date time working, switching to string
        public string HopSerialNumber { get; set; }
        public string HopVolume { get; set; }
        public int AlphaAcid { get; set; }
        public int BetaAcid { get; set; }
        public string HopNotes { get; set; }
        //Forign Key
        [ForeignKey("HopClassification")]//LOOK UP 
        
        public int HopClassificationID { get; set; }

        public virtual HopClassification HopClassification { get; set; }//LOOK UP
    }
//The following Is a DTO *data transfer object) for safer sending of data
    public class HopDTO
    {
        public int HopClassificationID { get; set; }
        public int HopID { get; set; }
        public string HopName { get; set; }
        public string HopProducer { get; set; }
        public string HopProductionDate { get; set; }
        public string HopSerialNumber { get; set; }
        public string HopVolume { get; set; }
        public int AlphaAcid { get; set; }
        public int BetaAcid { get; set; }
        public string HopNotes { get; set; }

    }



}