﻿using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryScraper.Models
{
    class Synonym
    {
        [Key]
        public int ID { get; set; }
        public int DetailsID { get; set; }
        public string Syn { get; set; }
    }
}