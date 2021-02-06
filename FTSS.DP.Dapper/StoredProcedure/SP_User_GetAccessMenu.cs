using System.Collections.Generic;
using Dapper;
using System;
using Microsoft.Data.SqlClient;
using System.Linq;
using FTSS.Models.Database;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_User_GetAccessMenu : ISP<Models.Database.StoredProcedures.SP_User_GetAccessMenu.Inputs>
    {
        private readonly string _cns;

        public SP_User_GetAccessMenu(string cns)
        {
            _cns = cns;
        }

        public DBResult Call(Models.Database.StoredProcedures.SP_User_GetAccessMenu.Inputs data)
        {
            if (data == null || string.IsNullOrEmpty(data.Token))
                throw new Exception("Invalid data for getting user access menu");

            string sql = "dbo.SP_User_GetAccessMenu";
            DBResult rst = null;

            using (var connection = new SqlConnection(_cns))
            {
                var p = Common.GetSearchParams(data.Token);

                var dbResult = connection.Query<Models.Database.StoredProcedures.SP_User_GetAccessMenu.Outputs>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure);

                rst = Common.GetResult(p, dbResult);
            }

            return rst;
        }
    }
}