using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Database
{
    public class Ctx : IDBCTX
    {
        private string _connectionString { get; set; }
        private SqlConnection _sqlConnection { get; set; }

        public Ctx(string ConnectionString)
        {
            _connectionString = ConnectionString;
            _sqlConnection = new SqlConnection(_connectionString);
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public SqlConnection GetSqlConnection()
        {
            return _sqlConnection;
        }
    }
}
