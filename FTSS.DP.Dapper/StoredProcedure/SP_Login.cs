using Dapper;
using System;
using Microsoft.Data.SqlClient;
using System.Linq;
using FTSS.Models.Database;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_Login : ISP<Models.Database.StoredProcedures.SP_Login.Inputs>
    {
        private readonly string _cns;

        public SP_Login(string cns)
        {
            _cns = cns;
        }

        public DBResult Call(Models.Database.StoredProcedures.SP_Login.Inputs filterParams)
        {
            if (filterParams == null)
                throw new Exception("SP_Login.Call can not be call without passing username and password");

            string sql = "dbo.SP_Login";
            DBResult rst = null;

            using (var connection = new SqlConnection(_cns))
            {
                var p = Common.GetSearchParams();
                p.Add("@Email", filterParams.Email);
                p.Add("@Password", filterParams.Password);

                var dbResult = connection.Query<Models.Database.StoredProcedures.SP_Login.Outputs>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                rst = Common.GetResult(p, dbResult);                
            }

            return rst;
        }
    }
}