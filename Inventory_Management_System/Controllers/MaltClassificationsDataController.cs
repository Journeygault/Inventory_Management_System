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
    public class MaltClassificationsDataController : ApiController
    {
        //This variable is our database access point
        private InventoryDataContext db = new InventoryDataContext();

        


        /// <summary>
        /// Gets a list or MaltClassifications in the database alongside a status code (200 OK).
        /// </summary>
        /// <returns>A list of MaltClassifications including their ID, name, and URL.</returns>
        /// <example>
        /// GET: api/MaltClassificationData/GetMaltClassifications
        /// </example>
        [ResponseType(typeof(IEnumerable<MaltClassificationDto>))]
        public IHttpActionResult GetMaltClassifications()
        {
            List<MaltClassification> MaltClassifications = db.MaltClassifications.ToList();
            List<MaltClassificationDto> MaltClassificationDtos = new List<MaltClassificationDto> { };

            //Here you can choose which information is exposed to the API
            foreach (var MaltClassification in MaltClassifications)
            {
                MaltClassificationDto NewMaltClassification = new MaltClassificationDto
                {
                    MaltClassificationID = MaltClassification.MaltClassificationID,
                    MaltClassificationType = MaltClassification.MaltClassificationType
,

                };
                MaltClassificationDtos.Add(NewMaltClassification);
            }

            return Ok(MaltClassificationDtos);
        }


        /// <summary>
        /// Gets a list of MaltClassifications in the database alongside a status code (200 OK).
        /// </summary>
        /// <param name="id">The input teamid</param>
        /// <returns>A list of MaltClassifications associated with the team</returns>
        /// <example>
        /// GET: api/MaltClassificationData/GetPlayersForMaltClassification
        /// </example>
        [ResponseType(typeof(IEnumerable<MaltDTO>))]
        public IHttpActionResult GetMaltsForMaltClassification(int id)
        {
            List<Malt> Malts = db.Malts.Where(p => p.MaltID == id)
                .ToList();
            List<MaltDTO> MaltDTOs = new List<MaltDTO> { };

            //Here you can choose which information is exposed to the API
            foreach (var Malt in Malts)
            {
                MaltDTO NewMalt = new MaltDTO
                {
                    MaltID = Malt.MaltID,
                    MaltName = Malt.MaltName,
                    MaltProducer = Malt.MaltProducer,
                    MaltProductionDate = Malt.MaltProductionDate,
                    MaltSerialNumber = Malt.MaltSerialNumber,
                    MaltVolume = Malt.MaltVolume,
                    DiasticPower = Malt.DiasticPower,
                    SRM = Malt.SRM,
                };
                MaltDTOs.Add(NewMalt);
            }

            return Ok(MaltDTOs);
        }

        /// <summary>
        /// Finds a particular MaltClassification in the database with a 200 status code. If the MaltClassification is not found, return 404.
        /// </summary>
        /// <param name="id">The MaltClassification id</param>
        /// <returns>Information about the MaltClassification, including MaltClassification id, bio, first and last name, and teamid</returns>
        // <example>
        // GET: api/MaltClassificationData/FindMaltClassification/5
        // </example>
        [HttpGet]
        [ResponseType(typeof(MaltClassificationDto))]
        public IHttpActionResult FindMalt(int id)
        {
            //Find the data
            MaltClassification MaltClassification = db.MaltClassifications.Find(id);
            //if not found, return 404 status code.
            if (MaltClassification == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format'
            MaltClassificationDto MaltClassificationDto = new MaltClassificationDto
            {
                MaltClassificationID = MaltClassification.MaltClassificationID,
                MaltClassificationType = MaltClassification.MaltClassificationType,

            };


            //pass along data as 200 status code OK response
            return Ok(MaltClassificationDto);
        }

        /// <summary>
        /// Updates a MaltClassification in the database given information about the MaltClassification.
        /// </summary>
        /// <param name="id">The MaltClassification id</param>
        /// <param name="MaltClassification">A MaltClassification object. Received as POST data.</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/MaltClassificationData/UpdateMaltClassification/5
        /// FORM DATA: MaltClassification JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult MaltClassification(int id, [FromBody] MaltClassification MaltClassification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != MaltClassification.MaltClassificationID)
            {
                return BadRequest();
            }

            db.Entry(MaltClassification).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)//What in the heck is this, check
            {
                if (!MaltClassificationExists(id))
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
        /// Adds a MaltClassification to the database.
        /// </summary>
        /// <param name="MaltClassification">A MaltClassification object. Sent as POST form data.</param>
        /// <returns>status code 200 if successful. 400 if unsuccessful</returns>
        /// <example>
        /// POST: api/MaltClassificationData/AddMaltClassification
        ///  FORM DATA: MaltClassification JSON Object
        /// </example>
        [ResponseType(typeof(MaltClassification))]
        [HttpPost]
        public IHttpActionResult AddMaltClassification([FromBody] MaltClassification MaltClassification)
        {
            //Will Validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MaltClassifications.Add(MaltClassification);
            db.SaveChanges();

            return Ok(MaltClassification.MaltClassificationID);
        }

        /// <summary>
        /// Deletes a MaltClassification in the database
        /// </summary>
        /// <param name="id">The id of the MaltClassification to delete.</param>
        /// <returns>200 if successful. 404 if not successful.</returns>
        /// <example>
        /// POST: api/MaltClassificationData/DeleteMaltClassification/5
        /// </example>
        [HttpPost]
        public IHttpActionResult DeleteMaltClassification(int id)
        {
            MaltClassification MaltClassification = db.MaltClassifications.Find(id);
            if (MaltClassification == null)
            {
                return NotFound();
            }

            db.MaltClassifications.Remove(MaltClassification);
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
        /// Finds a MaltClassification in the system. Internal use only.
        /// </summary>
        /// <param name="id">The MaltClassification id</param>
        /// <returns>TRUE if the MaltClassification exists, false otherwise.</returns>
        private bool MaltClassificationExists(int id)
        {
            return db.MaltClassifications.Count(e => e.MaltClassificationID == id) > 0;
        }
    }
}