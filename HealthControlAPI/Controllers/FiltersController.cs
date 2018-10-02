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
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace HealthControlAPI.Controllers
{
    [RoutePrefix("api/Filters")]
    public class FiltersController : ApiController
    {
        private HealthControlAPIContext db = new HealthControlAPIContext();
        string config = "server=localhost;username=root;password=joaofelipedev;database=anvisa;SslMode=none";
        string query = "select count(numero_registro_cadastro) from anvisa_dados";

        // GET: api/Filters/getRegistroCount
        [Route("getRegistroCount")]
        [HttpGet]
        public string getRegistroCount()
        {

            MySqlConnection connection = new MySqlConnection(config);
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            MySqlDataReader Reader = command.ExecuteReader();

            string retorno = null;

            while (Reader.Read())
            {
                retorno = Reader[0].ToString();
            }

            connection.Close();
            return JsonConvert.SerializeObject(retorno);
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