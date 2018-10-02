using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HealthControlAPI.Models;
using System.Web.Http.Cors;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace HealthControlAPI.Controllers
{
    //[EnableCors(origins: "http://localhost:3000/", headers: "*", methods: "*")]
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private HealthControlAPIContext db = new HealthControlAPIContext();

        // GET: api/Users
        public IQueryable<UserDTO> GetUsers()
        {
            var users = from user in db.Users
                        select new UserDTO()
                        {
                            Id = user.Id,
                            Nome = user.Nome
                        };
            return users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(UserDTO))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            var user = await db.Users.Select(u =>
        new UserDTO()
        {
            Id = u.Id,
            Nome = u.Nome
        }).SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Route("{email}/login")]
        [HttpGet]

        public async Task<IHttpActionResult> Login(string email)
        {
            var user = await (from u in db.Users
                              where u.Email == email
                              select new UserDTO
                              {
                                  Id = u.Id
                              }).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}