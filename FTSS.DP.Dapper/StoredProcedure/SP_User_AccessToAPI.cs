using Dapper;
using System;
using Microsoft.Data.SqlClient;
using System.Linq;
using FTSS.Models.Database;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_User_AccessToAPI : ISP<Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs>
    {
        private readonly string _cns;

        public SP_User_AccessToAPI(string cns)
        {
            _cns = cns;
        }

        public DBResult Call(Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs data)
        {
            if (data == null)
                throw new Exception("SP_User_AccessToAPI.Call can not be call without passing Token or APIAddress.");

            string sql = "dbo.SP_User_AccessToAPI";
            DBResult rst = null;

            using (var connection = new SqlConnection(_cns))
            {
                var p = Common.GetSearchParams();
                p.Add("@Token", data.Token);
                p.Add("@APIAddress", data.APIAddress);

                var dbResult = connection.Query<Models.Database.StoredProcedures.SP_User_AccessToAPI.Outputs>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                rst = Common.GetResult(p, dbResult);
            }

            return rst;
        }
    }
}