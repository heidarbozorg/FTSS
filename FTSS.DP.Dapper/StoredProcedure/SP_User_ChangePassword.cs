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
    public class SP_User_ChangePassword : ISP<Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs>
    {
        private readonly string _cns;

        public SP_User_ChangePassword(string cns)
        {
            _cns = cns;
        }

        public DBResult Call(Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs Data)
        {
            if (Data == null)
                throw new Exception("SP_User_ChangePassword.Call can not be call without passing Data");

            string sql = "dbo.SP_User_ChangePassword";
            DBResult rst = null;

            using (var connection = new SqlConnection(_cns))
            {
                var p = Common.GetDataParams(Data);

                p.Add("@OldPassword", Data.OldPassword);
                p.Add("@NewPassword", Data.NewPassword);

                var dbResult = connection.Query<Models.Database.StoredProcedures.SP_User_ChangePassword.Outputs>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                rst = Common.GetResult(p, dbResult);
            }

            return rst;
        }
    }
}
