using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;


namespace FTSS.DP.DapperORM
{
    public class SQLExecuter : ISQLExecuter
    {
        string _connectionString;

        public SQLExecuter(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, System.Data.CommandType? commandType = null)
        {
            IEnumerable<T> rst = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                //Execute query
                rst = connection.Query<T>(sql, param, commandType: commandType);
            }

            return rst;
        }
    }
}
