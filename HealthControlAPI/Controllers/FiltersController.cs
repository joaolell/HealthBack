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

        // GET: api/Users
        [Route("all2")]
        [HttpGet]
        public List<ResultSetMySql> getTop10Fabricantes()
        {

            string query = "SELECT NOME_PAIS_FABRIC, COUNT(NOME_PAIS_FABRIC) FROM anvisa_dados " +
                           " GROUP BY NOME_PAIS_FABRIC ORDER BY COUNT(NOME_PAIS_FABRIC) DESC limit 10 ";
            string config = "server=;username=;password=;database=;SslMode=none";

            MySqlConnection connection = new MySqlConnection(config);
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            MySqlDataReader Reader = command.ExecuteReader();

            List<ResultSetMySql> lista = new List<ResultSetMySql>();

            while (Reader.Read())
            {
                ResultSetMySql obj = new ResultSetMySql();
                obj.Ret1 = Reader[0].ToString();
                obj.Ret2 = Reader[1].ToString();

                lista.Add(obj);

            }
            connection.Close();
            return lista;
        }


        // GET: api/Users
        [Route("all2")]
        [HttpGet]
        public List<ResultSetMySql> getCountLaboratorios()
        {

            string query = "SELECT DETENTOR_REGISTRO_CADASTRO, COUNT(DETENTOR_REGISTRO_CADASTRO)" +
                           "FROM ANVISA_DADOS  GROUP BY DETENTOR_REGISTRO_CADASTRO ORDER BY COUNT(DETENTOR_REGISTRO_CADASTRO)DESC limit 10";
            string config = "server=;username=;password=;database=;SslMode=none";

            MySqlConnection connection = new MySqlConnection(config);
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            MySqlDataReader Reader = command.ExecuteReader();

            List<ResultSetMySql> lista = new List<ResultSetMySql>();

            while (Reader.Read())
            {
                ResultSetMySql obj = new ResultSetMySql();
                obj.Ret1 = Reader[0].ToString();
                obj.Ret2 = Reader[1].ToString();

                lista.Add(obj);

            }
            connection.Close();
            return lista;
        }

        // GET: api/Users
        [Route("all2")]
        [HttpGet]
        public List<ResultSetMySql> getRiscosPorVigencia()
        {

            string query = " SELECT VALIDADE_REGISTRO_CADASTRO, CLASSE_RISCO, COUNT(CLASSE_RISCO)" +
                           "FROM ANVISA_DADOS WHERE VALIDADE_REGISTRO_CADASTRO= 'VIGENTE' GROUP BY CLASSE_RISCO" +
                           "ORDER BY COUNT(CLASSE_RISCO)DESC  ";
            string config = "server=;username=;password=;database=;SslMode=none";

            MySqlConnection connection = new MySqlConnection(config);
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            MySqlDataReader Reader = command.ExecuteReader();

            List<ResultSetMySql> lista = new List<ResultSetMySql>();

            while (Reader.Read())
            {
                ResultSetMySql obj = new ResultSetMySql();
                obj.Ret1 = Reader[0].ToString();
                obj.Ret2 = Reader[1].ToString();

                lista.Add(obj);

            }
            connection.Close();
            return lista;
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