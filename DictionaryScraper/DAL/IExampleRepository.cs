using DictionaryScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryScraper.DAL
{
    interface IExampleRepository
    {
        int InsertExample(Example example);
    }
}
