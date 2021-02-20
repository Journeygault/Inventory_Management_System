using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory_Management_System.Models.ViewModels
{
    /// <summary>
    /// The following Allows the views easy access to all DTO's relating to hops,
    /// for the purposes of updating information
    /// </summary>
    public class UpdateHop
    {
        //Information about the player
        public HopDTO hop { get; set; }
        //Needed for a dropdownlist which presents the player with a choice of teams to play for
        //The dropdown did not work properly, I had to make it a manual input
        public IEnumerable<HopClassificationDto> allhopclassifications { get; set; }
    }
}