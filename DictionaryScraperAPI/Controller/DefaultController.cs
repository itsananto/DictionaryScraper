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
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;

namespace DictionaryScraperAPI.Controller
{
    public class DefaultController : ApiController
    {
        IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnKey"].ConnectionString);

        [HttpGet]
        [Route("WordList/less")]
        public IHttpActionResult GetWordListWithLessData()
        {
            var words = db.GetList<Words>();
            var details = db.GetList<Details>();
            var examples = db.GetList<Examples>();
            var subsenses = db.GetList<Subsenses>();
            var synonyms = db.GetList<Synonyms>();

            foreach (var w in words)
            {
                w.Details = details.Where(x => x.WordID == w.ID);
                foreach (var d in w.Details)
                {
                    d.Examples = examples.Where(x => x.DetailsID == d.ID && x.SubsenseID == null);
                    d.ExampleList = d.Examples.Select(x => x.Ex).Take(2).ToArray();
                    //d.Synonyms = synonyms.Where(x => x.DetailsID == d.ID);
                    //d.Subsenses = subsenses.Where(x => x.DetailsID == d.ID);
                    //foreach (var s in d.Subsenses)
                    //{
                    //    s.Examples = examples.Where(x => x.SubsenseID == s.ID);
                    //}
                }
            }

            return Ok(words);
        }
    }
}
