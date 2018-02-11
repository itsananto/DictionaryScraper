using DictionaryScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryScraper.DAL
{
    interface IWordRespository
    {
        IEnumerable<Word> GetWords();
        void InsertWord(Word word);
        void UpdateWord(Word word);
        void DeleteWord(int id);
    }
}
