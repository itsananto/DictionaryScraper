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
    class DetailRepository : IDetailRepository
    {
        private IDbConnection Connection;
        public DetailRepository()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["cnKey"].ConnectionString);
        }

        public int InsertDetail(Detail detail)
        {
            return (int)Connection.Insert(detail);
        }        

        public void UpdateDetail(Detail detail)
        {
            throw new NotImplementedException();
        }
    }
}
