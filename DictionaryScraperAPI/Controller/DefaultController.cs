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
using System.Web.Http.Cors;

namespace DictionaryScraperAPI.Controller
{
    public class DefaultController : ApiController
    {
        IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnKey"].ConnectionString);

        /// <summary>
        /// word list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("WordList")]
        public IHttpActionResult GetWordList()
        {
            var words = db.GetList<Words>();        
            return Ok(words.Select(x=>x.Wrd));
        }

        /// <summary>
        /// Word details
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("Word/{word}")]
        public IHttpActionResult GetWordList(string word)
        {
            var w = db.GetList<Words>().First(x=>x.Wrd==word);
            var details = db.GetList<Details>();
            var examples = db.GetList<Examples>();
            var subsenses = db.GetList<Subsenses>();
            var synonyms = db.GetList<Synonyms>();

            w.Details = details.Where(x => x.WordID == w.ID);
            foreach (var d in w.Details)
            {
                d.Examples = examples.Where(x => x.DetailsID == d.ID && x.SubsenseID == null);
                d.ExampleList = d.Examples.Select(x => x.Ex).ToArray();

                d.Synonyms = synonyms.Where(x => x.DetailsID == d.ID);
                d.SynonymList = d.Synonyms.Select(x => x.Syn).ToArray();

                d.Subsenses = subsenses.Where(x => x.DetailsID == d.ID);
                foreach (var s in d.Subsenses)
                {
                    s.Examples = examples.Where(x => x.SubsenseID == s.ID);
                    s.ExampleList = s.Examples.Select(x => x.Ex).ToArray();
                }
            }

            return Ok(w);
        }

        /// <summary>
        /// Offline data generator for dictionary
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
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
                }
            }

            return Ok(words);
        }

        /// <summary>
        /// All scraped data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("WordList/More")]
        public IHttpActionResult GetWordListWithAllData()
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
                    d.ExampleList = d.Examples.Select(x => x.Ex).ToArray();

                    d.Synonyms = synonyms.Where(x => x.DetailsID == d.ID);
                    d.SynonymList = d.Synonyms.Select(x => x.Syn).ToArray();

                    d.Subsenses = subsenses.Where(x => x.DetailsID == d.ID);
                    foreach (var s in d.Subsenses)
                    {
                        s.Examples = examples.Where(x => x.SubsenseID == s.ID);
                        s.ExampleList = s.Examples.Select(x => x.Ex).ToArray();
                    }
                }
            }

            return Ok(words);
        }
    }
}
