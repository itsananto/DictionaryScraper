using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryScraper.Models
{
    class Detail
    {
        [Key]
        public int ID { get; set; }
        public int WordID { get; set; }
        public string Pos { get; set; }
        public string Definition { get; set; }
    }
}
