using DictionaryScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryScraper.DAL
{
    interface IDetailRepository
    {
        int InsertDetail(Detail detail);
        void UpdateDetail(Detail detail);
    }
}
