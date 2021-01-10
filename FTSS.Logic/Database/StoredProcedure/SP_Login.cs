using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Database.StoredProcedure
{
    public class SP_Login
    {
        public static Models.Database.Interfaces.DBResult Call(IDBCTX ctx,
            Models.Database.Tables.Users user)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_Login(connectionString);
            var rst = sp.Call(user.Email, user.Password);
            return rst;
        }
    }
}
