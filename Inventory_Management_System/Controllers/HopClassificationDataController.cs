using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using System.Web.Http.Description;
using Inventory_Management_System.Models;
using System.Diagnostics;

namespace Inventory_Management_System.Controllers
{
    public class HopClassificationDataController : ApiController
    {
        //This variable is our database access point
        private InventoryDataContext db = new InventoryDataContext();
/// <summary>
/// Creates a list for Hopclassification and its DTO
/// </summary>
        [ResponseType(typeof(IEnumerable<HopClassificationDto>))]
        public IHttpActionResult GetHopClassifications()
        {
            List<HopClassification> HopClassifications = db.HopClassifications.ToList();
            List<HopClassificationDto> HopClassificationDtos = new List<HopClassificationDto> { };

            //Here you can choose which information is exposed to the API
            foreach (var HopClassification in HopClassifications)
            {
                HopClassificationDto NewHopClassification = new HopClassificationDto
                {
                    HopClassificationID = HopClassification.HopClassificationID,
                    HopClassificationType = HopClassification.HopClassificationType,
                    
                };
                HopClassificationDtos.Add(NewHopClassification);
            }

            return Ok(HopClassificationDtos);
        }


        /// <summary>
        /// Gets a list of players in the database alongside a status code (200 OK).
        /// </summary>
        /// <param name="id">The input teamid</param>
        /// <returns>A list of players associated with the team</returns>
        /// <example>
        /// GET: api/TeamData/GetPlayersForTeam
        /// </example>
        [ResponseType(typeof(IEnumerable<HopDTO>))]
        public IHttpActionResult GetHopsForHopClassification(int id)
        {
            List<Hop> Hops = db.Hops.Where(p => p.HopID == id)
                .ToList();
            List<HopDTO> HopDTOs = new List<HopDTO> { };

            //Here you can choose which information is exposed to the API
            foreach (var Hop in Hops)
            {
                HopDTO NewHop = new HopDTO
                {
                    HopID = Hop.HopID,
                    HopName = Hop.HopName,
                    HopProducer = Hop.HopProducer,
                    HopProductionDate = Hop.HopProductionDate,
                    HopSerialNumber = Hop.HopSerialNumber,
                    HopVolume = Hop.HopVolume,
                    AlphaAcid = Hop.AlphaAcid,
                    BetaAcid = Hop.BetaAcid,
                    HopNotes = Hop.HopNotes,
                };
                HopDTOs.Add(NewHop);
            }

            return Ok(HopDTOs);
        }

        

        /// <summary>
        /// Finds a specific hopclassification
        /// </summary>
        /// <param name="id">The Hopclassification id</param>
        /// <returns>Returns name of the hop classifcation</returns>
        // <example>
        // GET: api/TeamData/FindTeam/5
        // </example>
        [HttpGet]
        [ResponseType(typeof(HopClassificationDto))]
        public IHttpActionResult FindHop(int id)
        {
            //Find the data
            HopClassification HopClassification = db.HopClassifications.Find(id);
            //if not found, return 404 status code.
            if (HopClassification == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format'
            HopClassificationDto HopClassificationDto = new HopClassificationDto
            {
                HopClassificationID = HopClassification.HopClassificationID,
                HopClassificationType = HopClassification.HopClassificationType,

            };


            //pass along data as 200 status code OK response
            return Ok(HopClassificationDto);
        }

        /// <summary>
        /// Updates a hopclassifcation in the database
        /// </summary>
        /// <param name="id">The ID of the HopClassification</param>
        /// <param name="HopClassification">The type of HopClassification</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult HopClassification(int id, [FromBody] HopClassification HopClassification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != HopClassification.HopClassificationID)
            {
                return BadRequest();
            }

            db.Entry(HopClassification).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)//What in the heck is this, check
            {
                if (!HopClassificationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Sends a hopclassifcation as a post request, 
        /// </summary>
        /// <param name="HopClassification"></param>
        /// <returns>Hop classifciation or an error message</returns>
        [ResponseType(typeof(HopClassification))]
        [HttpPost]
        public IHttpActionResult AddHopClassification([FromBody] HopClassification HopClassification)
        {
            //Will Validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HopClassifications.Add(HopClassification);
            db.SaveChanges();

            return Ok(HopClassification.HopClassificationID);
        }

        /// <summary>
        /// Deletes a Team in the database
        /// </summary>
        /// <param name="id">The id of the Team to delete.</param>
        /// <returns>200 if successful. 404 if not successful.</returns>
        /// <example>
        /// POST: api/TeamData/DeleteTeam/5
        /// </example>
        [HttpPost]
        public IHttpActionResult DeleteHopClassification(int id)
        {
            HopClassification HopClassification = db.HopClassifications.Find(id);
            if (HopClassification == null)
            {
                return NotFound();
            }

            db.HopClassifications.Remove(HopClassification);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Finds a Hop classification in the system. Internal use only.
        /// </summary>
        /// <param name="id">The hopclassification id</param>
        /// <returns>TRUE if the classification exists, false otherwise.</returns>
        private bool HopClassificationExists(int id)
        {
            return db.HopClassifications.Count(e => e.HopClassificationID == id) > 0;
        }
    }
}
