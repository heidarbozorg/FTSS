using Dapper;
using System;
using Microsoft.Data.SqlClient;
using System.Linq;
using FTSS.Models.Database;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_User_AccessToAPI : ISP<Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs>
    {
        private readonly ISQLExecuter _executer;

        public SP_User_AccessToAPI(string connectionString, ISQLExecuter executer = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Could not create a new SP_User_AccessToAPI instance with empty connectionString");

            if (executer == null)
                _executer = new SQLExecuter(connectionString);
            else
                _executer = executer;
        }

        public DBResult Call(Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs data)
        {
            if (data == null)
                throw new Exception("SP_User_AccessToAPI.Call can not be call without passing Token or APIAddress.");

            return Execute(data);

            
        }

        /// <summary>
        /// Execute SP_User_AccessToAPI
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DBResult Execute(Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs data)
        {
            string sql = "dbo.SP_User_AccessToAPI";

            var p = Common.GetErrorCodeAndErrorMessageParams();
            p.Add("@Token", data.Token);
            p.Add("@APIAddress", data.APIAddress);

            var dbResult = _executer.Query<Models.Database.StoredProcedures.SP_User_AccessToAPI.Outputs>(
                sql, p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

            var rst = Common.GetResult(p, dbResult);

            return rst;
        }
    }
}