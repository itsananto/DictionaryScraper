using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DictionaryScraperAPI.Models
{
    public class Details
    {
        [JsonIgnore]
        public int ID { get; set; }
        [JsonIgnore]
        public int WordID { get; set; }
        public string POS { get; set; }
        public string Definition { get; set; }
        public string[] ExampleList { get; set; }
        public string[] SynonymList { get; set; }
        [JsonIgnore]
        public IEnumerable<Examples> Examples { get; set; }
        [JsonIgnore]
        public IEnumerable<Synonyms> Synonyms { get; set; }
        public IEnumerable<Subsenses> Subsenses { get; set; }
    }
}