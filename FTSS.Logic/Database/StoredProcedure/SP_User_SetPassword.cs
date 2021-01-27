using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Database.StoredProcedure
{
    public class SP_User_SetPassword
    {
        public static Models.Database.DBResult Call(IDBCTX ctx,
            Models.Database.Tables.Users Data)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_SetPassword(connectionString);
            var rst = sp.Call(Data);
            return rst;
        }
    }
}
