﻿using System.Collections.Generic;
using Dapper;
using System;
using Microsoft.Data.SqlClient;
using System.Linq;
using FTSS.Models.Database.Interfaces;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_Login : ISP<FTSS.Models.Database.Tables.Users>
    {
        private readonly string _cns;

        public SP_Login(string cns)
        {
            _cns = cns;
        }

        public DBResult Call(FTSS.Models.Database.Tables.Users Data)
        {
            if (Data == null)
                throw new Exception("SP_Login.Call can not be call without passing username and password");

            int ErrorCode = 0;
            string ErrorMessage = "";

            string sql = "dbo.SP_Login";
            DBResult rst = null;

            using (var connection = new SqlConnection(_cns))
            {
                var p = new DynamicParameters();
                p.Add("@Email", Data.Email);
                p.Add("@Password", Data.Password);
                p.Add("@ErrorCode", ErrorCode, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                p.Add("@ErrorMessage", ErrorMessage, System.Data.DbType.String, System.Data.ParameterDirection.Output);

                var dbResult = connection.Query<Models.Database.StoredProcedures.SP_Login>(
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