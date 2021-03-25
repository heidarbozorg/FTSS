using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM
{
    public class SQLExecuter : ISQLExecuter
    {
        private readonly string _connectionString;

        public SQLExecuter(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Execute database query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, object param = null, System.Data.CommandType? commandType = null)
        {
            var rst = QueryAsync<T>(sql, param, commandType);
            return rst.Result;
        }


        /// <summary>
        /// Execute database query asyncronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        private async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, System.Data.CommandType? commandType = null)
        {
            IEnumerable<T> rst = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                //Execute query
                rst = await connection.QueryAsync<T>(sql, param, commandType: commandType);
            }

            return rst;
        }
    }
}
