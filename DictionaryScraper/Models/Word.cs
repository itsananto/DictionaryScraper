using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryScraper.Models
{
    class Word
    {
        [Key]
        public int ID { get; set; }
        public string Wrd { get; set; }
    }
}
