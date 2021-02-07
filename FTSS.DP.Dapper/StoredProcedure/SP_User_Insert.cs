using Dapper;
using FTSS.Models.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_User_Insert : ISP<Models.Database.StoredProcedures.SP_User_Insert.Inputs>
    {
        private readonly ISQLExecuter _executer;

        public SP_User_Insert(string connectionString, ISQLExecuter executer = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Could not create a new SP_User_Insert instance with empty connectionString.");

            if (executer == null)
                _executer = new SQLExecuter(connectionString);
            else
                _executer = executer;
        }


        public DBResult Call(Models.Database.StoredProcedures.SP_User_Insert.Inputs data)
        {
            if (data == null)
                throw new Exception("SP_User_Insert.Call can not be call without passing Data");
            
            return Execute(data);
        }

        /// <summary>
        /// Execute SP_User_Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DBResult Execute(Models.Database.StoredProcedures.SP_User_Insert.Inputs data)
        {
            string sql = "dbo.SP_User_Insert";

            var p = Common.GetDataParams(data);
            p.Add("@Email", data.Email);
            p.Add("@Password", data.Password);
            p.Add("@FirstName", data.FirstName);
            p.Add("@LastName", data.LastName);

            var dbResult = _executer.Query<Models.Database.StoredProcedures.SP_User_Insert.Outputs>(
                sql, p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

            var rst = Common.GetResult(p, dbResult);

            return rst;
        }
    }
}
