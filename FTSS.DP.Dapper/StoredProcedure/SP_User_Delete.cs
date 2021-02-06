using Dapper;
using FTSS.Models.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_User_Delete : ISP<Models.Database.StoredProcedures.SP_User_Delete.Inputs>
    {
        private readonly string _cns;
        private readonly ISQLExecuter _executer;

        public SP_User_Delete(string connectionString, ISQLExecuter executer = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Could not create a new SP_User_Delete instance with empty connectionString");

            if (executer == null)
                _executer = new SQLExecuter(connectionString);
            else
                _executer = executer;

            _cns = connectionString;
        }

        public DBResult Call(Models.Database.StoredProcedures.SP_User_Delete.Inputs data)
        {
            if (data == null)
                throw new Exception("SP_User_Delete.Call can not be call without passing Data");

            return Execute(data);
        }

        /// <summary>
        /// Execute SP_User_Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DBResult Execute(Models.Database.StoredProcedures.SP_User_Delete.Inputs data)
        {
            string sql = "dbo.SP_User_Delete";
            DBResult rst = null;

            var p = Common.GetDataParams(data);
            p.Add("@UserId", data.UserId);

            var dbResult = _executer.Query<Models.Database.StoredProcedures.SP_User_Delete.Outputs>(
                sql, p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

            rst = Common.GetResult(p, dbResult);

            return rst;
        }
    }
}
