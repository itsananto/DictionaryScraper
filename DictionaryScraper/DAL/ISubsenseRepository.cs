using DictionaryScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryScraper.DAL
{
    interface ISubsenseRepository
    {
        int InsertSubsense(Subsense synonym);
    }
}
