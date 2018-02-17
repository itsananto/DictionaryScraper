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
    class SubsenseRepository : ISubsenseRepository
    {
        private IDbConnection Connection;
        public SubsenseRepository()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["cnKey"].ConnectionString);
        }

        public int InsertSubsense(Subsense subsense)
        {
            return (int)Connection.Insert(subsense);
        }
    }
}
