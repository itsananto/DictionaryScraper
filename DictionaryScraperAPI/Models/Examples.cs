﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DictionaryScraperAPI.Models
{
    public class Examples
    {
        [JsonIgnore]
        public int ID { get; set; }
        [JsonIgnore]
        public int DetailsID { get; set; }
        [JsonIgnore]
        public int? SubsenseID { get; set; }
        public string Ex { get; set; }
    }
}