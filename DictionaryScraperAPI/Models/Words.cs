using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DictionaryScraperAPI.Models
{
    public class Words
    {
        [JsonIgnore]
        public int ID { get; set; }
        public string Wrd { get; set; }

        public IEnumerable<Details> Details { get; set; }
    }
}