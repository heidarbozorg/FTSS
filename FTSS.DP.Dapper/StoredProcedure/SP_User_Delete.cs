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

        public SP_User_Delete(string cns)
        {
            _cns = cns;
        }

        public DBResult Call(Models.Database.StoredProcedures.SP_User_Delete.Inputs Data)
        {
            if (Data == null)
                throw new Exception("SP_User_Delete.Call can not be call without passing Data");

            string sql = "dbo.SP_User_Delete";
            DBResult rst = null;

            using (var connection = new SqlConnection(_cns))
            {
                var p = Common.GetDataParams(Data);

                p.Add("@UserId", Data.UserId);

                var dbResult = connection.Query<Models.Database.StoredProcedures.SP_User_Delete.Outputs>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                rst = Common.GetResult(p, dbResult);
            }

            return rst;
        }
    }
}
