using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DictionaryScraper.Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace DictionaryScraper.DAL
{
    class ExampleRepository : IExampleRepository
    {
        private IDbConnection Connection;
        public ExampleRepository()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["cnKey"].ConnectionString);
        }
        public int InsertExample(Example example)
        {
            return (int)Connection.Insert(example);
        }
    }
}
