using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Database.StoredProcedure
{
    public class SP_User_ChangePassword
    {
        public static Models.Database.DBResult Call(IDatabaseContext ctx,
            Models.Database.StoredProcedures.SP_User_ChangePassword Data)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_ChangePassword(connectionString);
            var rst = sp.Call(Data);
            return rst;
        }
    }
}
