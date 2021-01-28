using Dapper;
using FTSS.Models.Database;
using FTSS.Models.Database.Tables;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_User_UpdateProfile : ISP<Models.Database.Tables.Users>
    {
        private readonly string _cns;

        public SP_User_UpdateProfile(string cns)
        {
            _cns = cns;
        }

        public DBResult Call(Users Data)
        {
            if (Data == null)
                throw new Exception("SP_User_UpdateProfile.Call can not be call without passing Data");

            string sql = "dbo.SP_User_UpdateProfile";
            DBResult rst = null;

            using (var connection = new SqlConnection(_cns))
            {
                var p = Common.GetDataParams(Data);

                p.Add("@FirstName", Data.FirstName);
                p.Add("@LastName", Data.LastName);

                var dbResult = connection.Query<Models.Database.StoredProcedures.SingleId>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                rst = Common.GetResult(p, dbResult);
            }

            return rst;
        }
    }
}
