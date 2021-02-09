using FTSS.Models.Database;
using System;
using System.Linq;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_User_ChangePassword : ISP<Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs>
    {
        private readonly ISQLExecuter _executer;

        public SP_User_ChangePassword(string connectionString, ISQLExecuter executer = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Could not create a new SP_User_ChangePassword instance with empty connectionString");

            if (executer == null)
                _executer = new SQLExecuter(connectionString);
            else
                _executer = executer;
        }

        public DBResult Call(Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs data)
        {
            if (data == null)
                throw new Exception("SP_User_ChangePassword.Call can not be call without passing Data");

            return Execute(data);
        }

        /// <summary>
        /// Execute SP_User_UpdateProfile
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DBResult Execute(Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs data)
        {
            string sql = "dbo.SP_User_ChangePassword";

            var p = Common.GetDataParams(data);
            p.Add("@OldPassword", data.OldPassword);
            p.Add("@NewPassword", data.NewPassword);

            var dbResult = _executer.Query<Models.Database.StoredProcedures.SP_User_ChangePassword.Outputs>(
                sql, p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

            var rst = Common.GetResult(p, dbResult);

            return rst;
        }
    }
}
