using System;
using FTSS.Models.Database;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_Log_Insert : ISP<Models.Database.StoredProcedures.SP_Log_Insert.Inputs>
    {
        private readonly ISQLExecuter _executer;

        public SP_Log_Insert(string connectionString, ISQLExecuter executer = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Could not create a new SP_Log_Insert instance with empty connectionString");

            if (executer == null)
                _executer = new SQLExecuter(connectionString);
            else
                _executer = executer;
        }

        /// <summary>
        /// Calling 'SP_Log_Insert' stored procedure
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public DBResult Call(Models.Database.StoredProcedures.SP_Log_Insert.Inputs data)
        {
            if (data == null)
                throw new ArgumentNullException("SP_Log_Insert.Call can not be call without passing data.");

            if (string.IsNullOrEmpty(data.MSG))
                throw new ArgumentException("SP_Log_Insert.Call need a text message as parameter");

            return Execute(data);
        }

        /// <summary>
        /// Execute SP_Log_Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DBResult Execute(Models.Database.StoredProcedures.SP_Log_Insert.Inputs data)
        {
            string sql = "dbo.SP_Log_Insert";
            var p = Common.GetEmptyParams();
            p.Add("@MSG", data.MSG);
            p.Add("@IPAddress", data.IPAddress);

            var dbResult = _executer.Query<Models.Database.StoredProcedures.SP_Log_Insert.Outputs>(
                sql, data, commandType: System.Data.CommandType.StoredProcedure);

            var rst = new DBResult(200, "", dbResult);
            return rst;
        }
    }
}
