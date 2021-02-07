using System;
using System.Linq;
using FTSS.Models.Database;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_Login : ISP<Models.Database.StoredProcedures.SP_Login.Inputs>
    {
        private readonly ISQLExecuter _executer;

        public SP_Login(string connectionString, ISQLExecuter executer = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Could not create a new SP_Login instance with empty connectionString");

            if (executer == null)
                _executer = new SQLExecuter(connectionString);
            else
                _executer = executer;
        }

        public DBResult Call(Models.Database.StoredProcedures.SP_Login.Inputs data)
        {
            if (data == null)
                throw new Exception("SP_Login.Call can not be call without passing username and password");

            return Execute(data);            
        }

        /// <summary>
        /// Execute SP_Login
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DBResult Execute(Models.Database.StoredProcedures.SP_Login.Inputs data)
        {
            string sql = "dbo.SP_Login";

            var p = Common.GetErrorCodeAndErrorMessageParams();
            p.Add("@Email", data.Email);
            p.Add("@Password", data.Password);

            var dbResult = _executer.Query<Models.Database.StoredProcedures.SP_Login.Outputs>(
                sql, p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

            var rst = Common.GetResult(p, dbResult);

            return rst;
        }
    }
}