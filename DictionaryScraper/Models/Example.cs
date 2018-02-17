using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryScraper.Models
{
    class Example
    {
        [Key]
        public int ID { get; set; }
        public int DetailsID { get; set; }
        public int? SubsenseID { get; set; }
        public string Ex { get; set; }
    }
}
