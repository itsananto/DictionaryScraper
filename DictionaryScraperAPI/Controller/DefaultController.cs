using DictionaryScraperAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;

namespace DictionaryScraperAPI.Controller
{
    public class DefaultController : ApiController
    {
        IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnKey"].ConnectionString);
        // GET: api/Default
        [HttpGet]
        public IHttpActionResult Get()
        {
            var words = db.GetList<Words>();
            var details = db.GetList<Details>();
            var examples = db.GetList<Examples>();

            foreach (var w in words)
            {
                w.Details = details.Where(x => x.WordID == w.ID);
                foreach (var d in w.Details)
                {
                    d.Examples = examples.Where(x => x.DetailsID == d.ID);
                }
            }

            
            return Ok(words);
        }
    }
}
