using FTSS.Models.Database;
using System;
using System.Linq;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_User_UpdateProfile : ISP<Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs>
    {
        private readonly ISQLExecuter _executer;

        public SP_User_UpdateProfile(string connectionString, ISQLExecuter executer = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Could not create a new SP_User_UpdateProfile instance with empty connectionString");

            if (executer == null)
                _executer = new SQLExecuter(connectionString);
            else
                _executer = executer;
        }

        public DBResult Call(Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs data)
        {
            if (data == null)
                throw new ArgumentNullException("UpdateProfile can not be execute without passing Data.");

            return Execute(data);
        }

        /// <summary>
        /// Execute SP_User_UpdateProfile
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DBResult Execute(Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs data)
        {
            string sql = "dbo.SP_User_UpdateProfile";

            var p = Common.GetDataParams(data);
            p.Add("@FirstName", data.FirstName);
            p.Add("@LastName", data.LastName);

            var dbResult = _executer.Query<Models.Database.StoredProcedures.SP_User_UpdateProfile.Outputs>(
                sql, p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

            var rst = Common.GetResult(p, dbResult);

            return rst;
        }
    }
}
