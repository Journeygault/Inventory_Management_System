using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Http.Description;
using Inventory_Management_System.Models;
using System.Diagnostics;


namespace Inventory_Management_System.Controllers
{
    public class HopDataController : ApiController
    {
        /// <summary>
        /// This Controller allows the rest of the .NET application to access the data from the
        /// database
        /// </summary>
        //This variable is our database access point
        private InventoryDataContext db = new InventoryDataContext();



        // GET: api/Hops/GetHops
        // TODO: Searching Logic?
        [ResponseType(typeof(IEnumerable<HopDTO>))]
        public IHttpActionResult GetHops()
        {
            List<Hop> Hops = db.Hops.ToList();
            List<HopDTO> HopDTOS = new List<HopDTO> { };

            //Here you can choose which information is exposed to the API
            foreach (var Hop in Hops)
            {
                HopDTO NewHop = new HopDTO
                {
                    HopClassificationID=Hop.HopClassificationID,
                    HopID = Hop.HopID,
                    HopName = Hop.HopName,
                    HopProducer = Hop.HopProducer,
                    HopProductionDate = Hop.HopProductionDate,
                    HopSerialNumber = Hop.HopSerialNumber,
                    HopVolume = Hop.HopVolume,
                    AlphaAcid = Hop.AlphaAcid,
                    BetaAcid = Hop.BetaAcid,
                    HopNotes = Hop.HopNotes


                };
                HopDTOS.Add(NewHop);
            }

            return Ok(HopDTOS);
        }
        /// <summary>
        /// The following Finds a hop NOTE: its finding its ID from the DTO object
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A specific Hop and its relevent information </returns>
        // GET: api/Hops/FindHop/5
        [HttpGet]
        [ResponseType(typeof(HopDTO))]
       
        public IHttpActionResult FindHop(int id)
        {
            //Find the data
            Hop Hop = db.Hops.Find(id);
            //if not found, return 404 status code.
            if (Hop == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format'
            HopDTO HopDTO = new HopDTO
            {
                HopClassificationID = Hop.HopClassificationID,
                HopID = Hop.HopID,
                HopName = Hop.HopName,
                HopProducer = Hop.HopProducer,
                HopProductionDate = Hop.HopProductionDate,
                HopSerialNumber = Hop.HopSerialNumber,
                HopVolume = Hop.HopVolume,
                AlphaAcid = Hop.AlphaAcid,
                BetaAcid = Hop.BetaAcid,
                HopNotes = Hop.HopNotes
            };


            //pass along data as 200 status code OK response
            return Ok(HopDTO);
        }
        /// <summary>
        /// The following finds a specific hop fop and updates its value
        /// </summary>
        /// <param name="id"The hops ID></param>
        /// <param name="hop">The hop and its infomation </param>
        /// <returns></returns>
        // POST: api/Hops/UpdateHop/5
        // FORM DATA: Hop JSON Object
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateHop(int id, [FromBody] Hop hop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hop.HopID)
            {
                return BadRequest();
            }

            db.Entry(hop).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HopExists(id))
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

        // POST: api/Hops/AddHop
        // FORM DATA: Hop JSON Object
        [ResponseType(typeof(Hop))]
        [HttpPost]
        public IHttpActionResult AddHop([FromBody] Hop hop)
        {
            //Will Validate according to data annotations specified on model, the isValid basicaly tests to see if its a valid state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hops.Add(hop);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hop.HopID }, hop);//This is failing
        }

        // POST: api/Hops/DeleteHop/5
        /// <summary>
        /// Used to delete a specific hop
        /// </summary>
        /// <param name="id">Hop ID</param>
        /// <returns>A specific Hop</returns>
        [HttpPost]
        public IHttpActionResult DeleteHop(int id)
        {
            Hop hop = db.Hops.Find(id);
            if (hop == null)
            {
                return NotFound();
            }

            db.Hops.Remove(hop);
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

        private bool HopExists(int id)
        {
            return db.Hops.Count(e => e.HopID == id) > 0;
        }
    }
}