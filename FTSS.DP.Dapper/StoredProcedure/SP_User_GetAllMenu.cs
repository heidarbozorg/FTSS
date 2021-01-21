using System.Collections.Generic;
using Dapper;
using System;
using Microsoft.Data.SqlClient;
using System.Linq;
using FTSS.Models.Database.Interfaces;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_User_GetAllMenu : ISP<Models.Database.StoredProcedures.SP_Login>
    {
        private readonly string _cns;

        public SP_User_GetAllMenu(string cns)
        {
            _cns = cns;
        }

        public DBResult Call(Models.Database.StoredProcedures.SP_Login Data)
        {
            if (Data == null || string.IsNullOrEmpty(Data.Token))
                throw new Exception("SP_User_GetAllMenu.Call can not be call without passing userInfo");

            int ErrorCode = 0;
            string ErrorMessage = "";

            string sql = "dbo.SP_User_GetAllMenu";
            DBResult rst = null;

            using (var connection = new SqlConnection(_cns))
            {
                var p = new DynamicParameters();
                p.Add("@Token", Data.Token);
                p.Add("@ErrorCode", ErrorCode, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                p.Add("@ErrorMessage", ErrorMessage, System.Data.DbType.String, System.Data.ParameterDirection.Output);

                var dbResult = connection.Query<Models.Database.StoredProcedures.SP_User_GetAllMenu>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                ErrorMessage = p.Get<string>("ErrorMessage");
                ErrorCode = p.Get<int>("ErrorCode");

                rst = new DBResult(ErrorCode, ErrorMessage, dbResult);
                rst.ActualLength = dbResult == null ? 0 : 1;
            }

            return rst;
        }
    }
}
