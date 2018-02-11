using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DictionaryScraper.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper.Contrib.Extensions;

namespace DictionaryScraper.DAL
{
    class SynonymRepository : ISynonymRepository
    {
        private IDbConnection Connection;
        public SynonymRepository()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["cnKey"].ConnectionString);
        }

        public int InsertSynonym(Synonym synonym)
        {
            return (int)Connection.Insert(synonym);
        }
    }
}
