using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
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

        //GETSearch: api/Sushis
        [ResponseType(typeof(List<SushiModel>))]
        public IHttpActionResult GetSushiSearch(string fieldSearch, string textSearch, string fieldSort, string valueSort)
        {
            List<Sushi> sushis = db.Sushi.ToList();
            if(textSearch != null)
            {
                if(fieldSort != null)
                {
                    sushis = Search(sushis, fieldSearch, textSearch);
                    sushis = Sorting(sushis, fieldSort, valueSort);
                }
                else
                {
                    sushis = Search(sushis, fieldSearch, textSearch);
                }
            }
            else
            {
                if(fieldSort != null)
                {
                    sushis = Sorting(sushis, fieldSort, valueSort);
                }
            }
            return Ok( sushis);
        }

        public List<Sushi> Search (List<Sushi> sushis, string fieldSearch, string textSearch)
        {
            Regex regex = new Regex("^(" + textSearch + ")");
            switch(fieldSearch)
            {
                case ("Name"):
                    sushis=sushis.Where(x=>regex.IsMatch(x.Name)).ToList();
                    break;
                case ("Price"):
                    sushis = sushis.Where(x => regex.IsMatch(Convert.ToString(x.Price))).ToList();
                    break;
                case ("Compound"):
                    sushis = sushis.Where(x => regex.IsMatch(x.Compound)).ToList();
                    break;
            }
            return sushis;
        }

        public List<Sushi> Sorting(List<Sushi> sushis, string fieldSort, string valueSort)
        {
            switch(fieldSort)
            { 
                case("Name"):
                    if (valueSort == "По возрастанию")
                    {
                        sushis = sushis.OrderBy(x => x.Name).ToList();
                    }
                    else if (valueSort == "По убыванию")
                    {
                        sushis = sushis.OrderByDescending(x => x.Name).ToList();
                    }
                    else
                    {
                        sushis = db.Sushi.ToList();
                    }
                    break;
                case ("Price"):
                    if (valueSort == "По возрастанию")
                    {
                        sushis = sushis.OrderBy(x => x.Price).ToList();
                    }
                    else if(valueSort == "По убыванию")
                    {
                        sushis = sushis.OrderByDescending(x => x.Price).ToList();
                    }
                    else
                    {
                        sushis = db.Sushi.ToList();
                    }
                    break;
                case ("0"):
                    sushis = db.Sushi.ToList();
                    break ;
            }
            return sushis;
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
            var dbSushi= db.Sushi.FirstOrDefault(x=>x.Id.Equals(id));
            dbSushi.Image=sushi.Image;
            dbSushi.Name = sushi.Name;
            dbSushi.Compound = sushi.Compound;
            dbSushi.Price = sushi.Price;
            try
            {
                db.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
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
        // PUTNoImage: api/Sushis/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSushiNoImage(int id, Sushi sushi)
        {
            var dbSushi = db.Sushi.FirstOrDefault(x => x.Id.Equals(id));
            dbSushi.Name = sushi.Name;
            dbSushi.Compound = sushi.Compound;
            dbSushi.Price = sushi.Price;
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