using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Database.StoredProcedure
{
    public class SP_Users_GetAll
    {
        public static Models.Database.DBResult Call(IDatabaseContext ctx,
            Models.Database.StoredProcedures.SP_Users_GetAll.Inputs filterParams,
            Models.Database.ISP<Models.Database.StoredProcedures.SP_Users_GetAll.Inputs> sp = null)
        {


            var connectionString = ctx.GetConnectionString();
            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_Users_GetAll(connectionString);

            var rst = sp.Call(filterParams);
            return rst;
        }
    }
}
