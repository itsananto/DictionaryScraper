using DictionaryScraperAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryScraperAPI.Models
{
    public class Subsenses
    {
        [JsonIgnore]
        public int ID { get; set; }
        [JsonIgnore]
        public int DetailsID { get; set; }
        public string Definition { get; set; }
        [JsonIgnore]
        public IEnumerable<Examples> Examples { get; set; }
        public string[] ExampleList { get; set; }
    }
}
