using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory_Management_System.Models.ViewModels
{
    /// <summary>
    /// The following Allows the views easy access to all DTO's relating to hops,
    /// helps keep data secure
    /// </summary>
    public class ShowHop
    {

        public HopDTO hop { get; set; }
        //information about the team the player plays for
        public HopClassificationDto hopClassification { get; set; }
    }
}