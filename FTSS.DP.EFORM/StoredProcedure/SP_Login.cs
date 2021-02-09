using FTSS.Models.Database;
using FTSS.Models.Database.StoredProcedures.SP_Login;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;

namespace FTSS.DP.EFORM.StoredProcedure
{
    public class SP_Login : ISP<Models.Database.StoredProcedures.SP_Login.Inputs>
    {
        private readonly FTSSDBContext _dbContext;

        public SP_Login(FTSSDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DBResult Call(Inputs data)
        {
            SqlParameter[] parameters =
            {
                    new SqlParameter("@Email", data.Email),
                    new SqlParameter("@Password", data.Password),
                    new SqlParameter
                    {
                        ParameterName = "@ErrorMessage",
                        SqlDbType = SqlDbType.VarChar,
                        Size = 4000,
                        Direction = ParameterDirection.Output,
                        Value = ""
                    },
                    new SqlParameter
                    {
                        ParameterName = "@ErrorCode",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output,
                    }
                };

            var result = _dbContext.SP_Login.FromSql(
                @"[dbo].[SP_Login]
		            @Email,
		            @Password,		                    
		            @ErrorCode OUT,
                    @ErrorMessage OUT
                ",
                parameters).FirstOrDefault();

            //کد خطا و پیغام خطا را از نتیجه اجرای ای پی واکشی میکنیم
            var msg = Common.getMessage(parameters);
            var code = Common.getCode(parameters);
            var dataLen = result == null ? 0 : 1;
            var actualLen = dataLen;
            var rst = new Models.Database.DBResult(code, msg, result, actualLen);
            return (rst);
        }
    }
}
