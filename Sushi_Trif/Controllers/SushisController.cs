using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Sushi_Trif.Models;

namespace Sushi_Trif.Controllers
{
    public class SushisController : ApiController
    {
        private Sushi_trifEntities db = new Sushi_trifEntities();

        // GET: api/Sushis
        [ResponseType(typeof(List<SushiModel>))]
        public IHttpActionResult GetSushi()
        {
            return Ok( db.Sushi.ToList().ConvertAll(x=>new SushiModel(x)));
        }

        // GET: api/Sushis/5
        [ResponseType(typeof(Sushi))]
        public IHttpActionResult GetSushi(int id)
        {
            Sushi sushi = db.Sushi.Find(id);
            if (sushi == null)
            {
                return NotFound();
            }

            return Ok(sushi);
        }

        // PUT: api/Sushis/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSushi(int id, Sushi sushi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sushi.Id)
            {
                return BadRequest();
            }

            db.Entry(sushi).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SushiExists(id))
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

        //public IHttpActionResult PutSushi(int id, Sushi sushi)
        //{

        //        var entity = db.Sushi.FirstOrDefault(e => e.Id == id);
        //        entity.Image = sushi.Image;
        //        entity.Name = sushi.Name;
        //        entity.Compound = sushi.Compound;
        //        entity.Price = sushi.Price;
        //       db.SaveChanges();
        //    return StatusCode(HttpStatusCode.NoContent);

        //}

        // POST: api/Sushis
        [ResponseType(typeof(Sushi))]
        public IHttpActionResult PostSushi(Sushi sushi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sushi.Add(sushi);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sushi.Id }, sushi);
        }

        // DELETE: api/Sushis/5
        [ResponseType(typeof(Sushi))]
        public IHttpActionResult DeleteSushi(int id)
        {
            Sushi sushi = db.Sushi.Find(id);
            if (sushi == null)
            {
                return NotFound();
            }

            db.Sushi.Remove(sushi);
            db.SaveChanges();

            return Ok(sushi);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SushiExists(int id)
        {
            return db.Sushi.Count(e => e.Id == id) > 0;
        }
    }
}