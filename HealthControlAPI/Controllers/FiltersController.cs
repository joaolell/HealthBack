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
        string config = "server=108.167.132.205;username=kutzkeod_rafael;password=pedrobial0800;database=kutzkeod_anvisa;SslMode=none";

        // GET: api/Filters/getRegistroCount
        [Route("getRegistroCount")]
        [HttpGet]
        public string getRegistroCount()
        {
            string query = "select count(numero_registro_cadastro) from anvisa_dados";
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

        // GET: api/Filters/getTopFabricantes
        [Route("getTopFabricantes")]
        [HttpGet]
        public string getTop10Fabricantes()
        {

            string query = "SELECT NOME_PAIS_FABRIC, COUNT(NOME_PAIS_FABRIC) FROM anvisa_dados " +
                           " GROUP BY NOME_PAIS_FABRIC ORDER BY COUNT(NOME_PAIS_FABRIC) DESC limit 10 ";

            MySqlConnection connection = new MySqlConnection(config);
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            MySqlDataReader Reader = command.ExecuteReader();

            var json = new List<FabricanteJSON>();
            while (Reader.Read())
            {
                FabricanteJSON obj = new FabricanteJSON();
                obj.Nome = Reader[0].ToString();
                obj.Count = Reader[1].ToString();

                json.Add(obj);

            }
            connection.Close();
            string jsonstring = JsonConvert.SerializeObject(json, Formatting.Indented);
            return jsonstring;
        }


        // GET: api/Filters/getLabCount
        [Route("getLabCount")]
        [HttpGet]
        public string getCountLaboratorios()
        {

            string query = "SELECT DETENTOR_REGISTRO_CADASTRO, COUNT(DETENTOR_REGISTRO_CADASTRO)" +
                           "FROM anvisa_dados  GROUP BY DETENTOR_REGISTRO_CADASTRO ORDER BY COUNT(DETENTOR_REGISTRO_CADASTRO)DESC limit 10";

            MySqlConnection connection = new MySqlConnection(config);
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            MySqlDataReader Reader = command.ExecuteReader();

            var json = new List<LabJSON>();

            while (Reader.Read())
            {
                LabJSON obj = new LabJSON();
                obj.Nome = Reader[0].ToString();
                obj.Count = Reader[1].ToString();
                json.Add(obj);

            }
            connection.Close();
            string jsonstring = JsonConvert.SerializeObject(json, Formatting.Indented);
            return jsonstring;
        }

        // GET: api/Filters/getRiscos
        [Route("getRiscos")]
        [HttpGet]
        public string getRiscosPorVigencia()
        {

            string query = " SELECT VALIDADE_REGISTRO_CADASTRO, CLASSE_RISCO, COUNT(CLASSE_RISCO)" +
                           "FROM anvisa_dados WHERE VALIDADE_REGISTRO_CADASTRO= 'VIGENTE' GROUP BY CLASSE_RISCO" +
                           "ORDER BY COUNT(CLASSE_RISCO)DESC  ";

            MySqlConnection connection = new MySqlConnection(config);
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            MySqlDataReader Reader = command.ExecuteReader();

            var json = new List<FabricanteJSON>();
            while (Reader.Read())
            {
                FabricanteJSON obj = new FabricanteJSON();
                obj.Nome = Reader[0].ToString();
                obj.Count = Reader[1].ToString();

                json.Add(obj);

            }
            connection.Close();
            string jsonstring = JsonConvert.SerializeObject(json, Formatting.Indented);
            return jsonstring;
        }

        // GET: api/Filters/getDescartaveis
        [Route("getDescartaveis")]
        [HttpGet]
        public object getDescartaveis()
        {

            string query = " SELECT NOME_PAIS_FABRIC, COUNT(NOME_PAIS_FABRIC) " +
                           " FROM anvisa_dados WHERE nome_comercial like '%descarta%' GROUP BY NOME_PAIS_FABRIC " +
                           " ORDER BY COUNT(NOME_PAIS_FABRIC) DESC limit 10";

            MySqlConnection connection = new MySqlConnection(config);
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            MySqlDataReader Reader = command.ExecuteReader();
            var json = new List<FabricanteJSON>();
            while (Reader.Read())
            {
                FabricanteJSON obj = new FabricanteJSON();
                obj.Nome = Reader[0].ToString();
                obj.Count = Reader[1].ToString();

                json.Add(obj);

            }
            connection.Close();
            string jsonstring = JsonConvert.SerializeObject(json, Formatting.Indented);
            return jsonstring;
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