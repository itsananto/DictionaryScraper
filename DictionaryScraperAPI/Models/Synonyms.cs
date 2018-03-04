using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryScraperAPI.Models
{
    public class Synonyms
    {
        [JsonIgnore]
        public int ID { get; set; }
        public int DetailsID { get; set; }
        public string Syn { get; set; }
    }
}
