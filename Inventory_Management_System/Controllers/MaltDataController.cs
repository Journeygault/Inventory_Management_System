using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

using Inventory_Management_System.Models;


namespace Inventory_Management_System.Controllers
{
    public class MaltDataController : ApiController
    {
        /// <summary>
        /// The following is a malt data controller, It makes the data acessible to the rest of the .net application
        /// </summary>
        //This variable is our database access point
        private InventoryDataContext db = new InventoryDataContext();

        
        public IEnumerable<MaltDTO> GetMalts()
        {

            List<Malt> Malts = db.Malts.ToList();
            List<MaltDTO> MaltDTOs = new List<MaltDTO> { };

            //Here you can choose which information is exposed to the API
            foreach (var Malt in Malts)
            {
                MaltDTO NewMalt = new MaltDTO
                {
                    MaltID = Malt.MaltID,
                    MaltName = Malt.MaltName,
                    MaltProducer = Malt.MaltProducer,
                    MaltProductionDate= Malt.MaltProductionDate,
                    MaltSerialNumber= Malt.MaltSerialNumber,
                    MaltVolume= Malt.MaltVolume,
                    DiasticPower= Malt.DiasticPower,
                    SRM = Malt.SRM

                };
                MaltDTOs.Add(NewMalt);
            }

            return MaltDTOs;
        }

        // GET: api/Malts/FindMalt/5
        [ResponseType(typeof(MaltDTO))]
        [HttpGet]
        public IHttpActionResult FindMalt(int id)
        {
            //Find the data
            Malt Malt = db.Malts.Find(id);
            //if not found, return 404 status code.
            if (Malt == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format'
            MaltDTO MaltDTO = new MaltDTO
            {
                MaltID = Malt.MaltID,
                MaltName = Malt.MaltName,
                MaltProducer = Malt.MaltProducer,
                MaltProductionDate = Malt.MaltProductionDate,
                MaltSerialNumber = Malt.MaltSerialNumber,
                MaltVolume = Malt.MaltVolume,
                DiasticPower = Malt.DiasticPower,
                SRM = Malt.SRM
            };


            //pass along data as 200 status code OK response
            return Ok(MaltDTO);
        }
        ///The following allows for an update of a specific Malt
        // POST: api/Malts/UpdateMalt/5
        // FORM DATA: Player JSON Object
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePlayer(int id, [FromBody] Malt Malt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Malt.MaltID)
            {
                return BadRequest();
            }

            db.Entry(Malt).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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
        ///The following allows you to add a malt

        /// </summary>
        // POST: api/Malts/AddMalt
        // FORM DATA: Player JSON Object

        [ResponseType(typeof(Malt))]
        [HttpPost]
        
        public IHttpActionResult AddMalt([FromBody] Malt malt)
        {

            //Will Validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Malts.Add(malt);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = malt.MaltID }, malt);
        }

        // POST: api/Malts/DeleteMalt/5
        [HttpPost]
        public IHttpActionResult DeleteMalt(int id)
        {
            Malt malt = db.Malts.Find(id);
            if (malt == null)
            {
                return NotFound();
            }

            db.Malts.Remove(malt);
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

        private bool PlayerExists(int id)
        {
            return db.Malts.Count(e => e.MaltID == id) > 0;
        }

    }
}
