using System.Collections.Generic;
using Dapper;
using System;
using Microsoft.Data.SqlClient;
using System.Linq;
using FTSS.Models.Database.Interfaces;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_Log_Insert : ISP
    {
        private readonly string _cns;

        /// <summary>
        /// Maybe delete in the future
        /// </summary>
        private readonly SqlConnection _connection;

        public SP_Log_Insert(string cns)
        {
            _cns = cns;
        }

        public SP_Log_Insert(SqlConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Calling 'SP_Log_Insert' stored procedure
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public DBResult Call(params object[] param)
        {
            if (param == null || param.Length == 0)
                throw new Exception("SP_Log_Insert.Call need a text message as parameter");

            string sql = "dbo.SP_Log_Insert";
            int PersonelId = -1;
            string IPAddress = "test";
            string msg = param[0] as string;

            using (var connection = new SqlConnection(_cns))
            {
                connection.Execute(sql,
                    new 
                    {
                        PersonelId,
                        IPAddress,
                        MSG = msg 
                    }, 
                    commandType: System.Data.CommandType.StoredProcedure);
            }

            return new DBResult(0, "");
        }
    }
}
