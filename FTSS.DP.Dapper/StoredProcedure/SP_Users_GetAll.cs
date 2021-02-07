using FTSS.Models.Database;
using System;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_Users_GetAll : ISP<Models.Database.StoredProcedures.SP_Users_GetAll.Inputs>
    {
        private readonly ISQLExecuter _executer;

        public SP_Users_GetAll(string connectionString, ISQLExecuter executer = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Could not create a new SP_Users_GetAll instance with empty connectionString");

            if (executer == null)
                _executer = new SQLExecuter(connectionString);
            else
                _executer = executer;
        }

        public DBResult Call(Models.Database.StoredProcedures.SP_Users_GetAll.Inputs data)
        {
            if (data == null)
                throw new Exception("SP_Users_GetAll.Call can not be call without passing data");
            return Execute(data);
        }

        /// <summary>
        /// Execute SP_Users_GetAll
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DBResult Execute(Models.Database.StoredProcedures.SP_Users_GetAll.Inputs data)
        {
            string sql = "dbo.SP_Users_GetAll";

            var p = Common.GetSearchParams(data);
            p.Add("@Email", data.Email);
            p.Add("@FirstName", data.FirstName);
            p.Add("@LastName", data.LastName);

            var dbResult = _executer.Query<Models.Database.StoredProcedures.SP_Users_GetAll.Outputs>(
                sql, p, commandType: System.Data.CommandType.StoredProcedure);

            var rst = Common.GetResult(p, dbResult);

            return rst;
        }
    }
}