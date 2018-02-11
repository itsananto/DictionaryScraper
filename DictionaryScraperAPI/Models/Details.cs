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
        public IEnumerable<Examples> Examples { get; set; }
    }
}