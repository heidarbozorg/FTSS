using System;
using System.Linq;
using FTSS.Models.Database;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_User_GetAccessMenu : ISP<Models.Database.StoredProcedures.SP_User_GetAccessMenu.Inputs>
    {
        private readonly ISQLExecuter _executer;

        public SP_User_GetAccessMenu(string connectionString, ISQLExecuter executer = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Could not create a new SP_User_GetAccessMenu instance with empty connectionString.");

            if (executer == null)
                _executer = new SQLExecuter(connectionString);
            else
                _executer = executer;
        }

        public DBResult Call(Models.Database.StoredProcedures.SP_User_GetAccessMenu.Inputs data)
        {
            if (data == null || string.IsNullOrEmpty(data.Token))
                throw new Exception("Invalid data for getting user access menu");

            return Execute(data);
        }

        /// <summary>
        /// Execute SP_User_GetAccessMenu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DBResult Execute(Models.Database.StoredProcedures.SP_User_GetAccessMenu.Inputs data)
        {
            string sql = "dbo.SP_User_GetAccessMenu";

            var p = Common.GetToken(data.Token);

            var dbResult = _executer.Query<Models.Database.StoredProcedures.SP_User_GetAccessMenu.Outputs>(
                sql, p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

            var rst = Common.GetResult(p, dbResult);

            return rst;
        }
    }
}