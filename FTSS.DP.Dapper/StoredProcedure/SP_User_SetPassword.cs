using FTSS.Models.Database;
using System;
using System.Linq;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_User_SetPassword : ISP<Models.Database.StoredProcedures.SP_User_SetPassword.Inputs>
    {
        private readonly ISQLExecuter _executer;

        public SP_User_SetPassword(string connectionString, ISQLExecuter executer = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Could not create a new SP_User_SetPassword instance with empty connectionString");

            if (executer == null)
                _executer = new SQLExecuter(connectionString);
            else
                _executer = executer;
        }

        public DBResult Call(Models.Database.StoredProcedures.SP_User_SetPassword.Inputs data)
        {
            if (data == null)
                throw new Exception("SP_User_SetPassword.Call can not be call without passing Data");
            return Execute(data);
        }

        /// <summary>
        /// Execute SP_User_SetPassword
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DBResult Execute(Models.Database.StoredProcedures.SP_User_SetPassword.Inputs data)
        {
            string sql = "dbo.SP_User_SetPassword";

            var p = Common.GetDataParams(data);
            p.Add("@UserId", data.UserId);
            p.Add("@Password", data.Password);

            var dbResult = _executer.Query<Models.Database.StoredProcedures.SP_User_SetPassword.Outputs>(
                sql, p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

            var rst = Common.GetResult(p, dbResult);

            return rst;
        }
    }
}
