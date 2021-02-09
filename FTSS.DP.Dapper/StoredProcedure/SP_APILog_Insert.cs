using System;
using System.Linq;
using FTSS.Models.Database;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_APILog_Insert : ISP<Models.Database.StoredProcedures.SP_APILog_Insert.Inputs>
    {
        private readonly ISQLExecuter _executer;

        public SP_APILog_Insert(string connectionString, ISQLExecuter executer = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Could not create a new SP_APILog_Insert instance with empty connectionString");

            if (executer == null)
                _executer = new SQLExecuter(connectionString);
            else
                _executer = executer;
        }

        /// <summary>
        /// Calling 'SP_APILog_Insert' stored procedure
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public DBResult Call(Models.Database.StoredProcedures.SP_APILog_Insert.Inputs data)
        {
            if (data == null)
                throw new ArgumentNullException("SP_APILog_Insert.Call can not be call without passing data.");            

            return Execute(data);
        }

        /// <summary>
        /// Execute SP_APILog_Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DBResult Execute(Models.Database.StoredProcedures.SP_APILog_Insert.Inputs data)
        {
            string sql = "dbo.SP_APILog_Insert";
            var p = Common.GetEmptyParams();
            p.Add("@APIAddress", data.APIAddress);
            p.Add("@UserToken", data.UserToken);
            p.Add("@Params", data.Params);
            p.Add("@Results", data.Results);
            p.Add("@ErrorMessage", data.ErrorMessage);
            p.Add("@StatusCode", data.StatusCode);
            
            var dbResult = _executer.Query<Models.Database.StoredProcedures.SP_APILog_Insert.Outputs>(
                sql, data, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

            var rst = new DBResult(200, "", dbResult);
            return rst;
        }
    }
}
