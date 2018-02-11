using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DictionaryScraper.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using Dapper.Contrib.Extensions;

namespace DictionaryScraper.DAL
{
    class WordRespository : IWordRespository
    {
        private IDbConnection Connection;
        public WordRespository()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["cnKey"].ConnectionString);
        }
        public void DeleteWord(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Word> GetWords()
        {
            return Connection.GetAll<Word>();
        }

        public void InsertWord(Word word)
        {
            throw new NotImplementedException();
        }

        public void UpdateWord(Word word)
        {
            throw new NotImplementedException();
        }
    }
}
