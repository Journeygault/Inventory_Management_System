using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_Management_System.Models
{
    /// <summary>
    /// The following defines what a malt is, its basic atributes and volume
    /// </summary>
    public class Malt
    {
        [Key]//Capital?
        public int MaltID { get; set; }
        public string MaltName { get; set; }
        public string MaltProducer { get; set; }
        public DateTime MaltProductionDate { get; set; }
        public string MaltSerialNumber { get; set; }
        public string MaltVolume { get; set; }
        public int DiasticPower { get; set; }
        public int SRM { get; set; }
        //Forign Key
        [ForeignKey("MaltClassification")]//LOOK UP 
        public int MaltClassificationID { get; set; }
        //This allows access to the Malt Classificatons table
        public virtual MaltClassification MaltClassification { get; set; }//LOOK UP
    }
    /// <summary>
    /// The following is a data transfer object, its a 
    /// safer way to move data, I think its 
    /// because when you deal with the DTO it 
    /// prevents a user from interacting directly
    /// with the database
    /// </summary>
    public class MaltDTO
    {
        public int MaltClassificationID { get; set; }
        public int MaltID { get; set; }
        public string MaltName { get; set; }
        public string MaltProducer { get; set; }
        public DateTime MaltProductionDate { get; set; }
        public string MaltSerialNumber { get; set; }
        public string MaltVolume { get; set; }
        public int DiasticPower { get; set; }
        public int SRM { get; set; }
    }
}
