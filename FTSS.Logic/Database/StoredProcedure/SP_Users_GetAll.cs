using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Database.StoredProcedure
{
    public class SP_Users_GetAll
    {
        public static Models.Database.DBResult Call(IDBCTX ctx,
            Models.Database.StoredProcedures.SP_Users_GetAll_Params filterParams)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_Users_GetAll(connectionString);
            var rst = sp.Call(filterParams);
            return rst;
        }
    }
}
