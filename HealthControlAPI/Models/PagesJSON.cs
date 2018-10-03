using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthControlAPI.Models
{
    public class PagesJSON
    {
        public List<ResultSetMySql> Result;

        public PagesJSON()
        {
            Result = new List<ResultSetMySql>();
        }
    }
}