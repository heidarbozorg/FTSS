using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Database.StoredProcedure
{
    public class SP_User_GetAllMenu
    {
        public static Models.Database.Interfaces.DBResult Call(IDBCTX ctx,
            Models.Database.StoredProcedures.SP_Login userInfo)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_GetAllMenu(connectionString);
            var rst = sp.Call(userInfo);
            return rst;
        }
    }
}
