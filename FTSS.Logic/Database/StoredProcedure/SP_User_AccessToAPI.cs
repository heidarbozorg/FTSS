using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Database.StoredProcedure
{
    public class SP_User_AccessToAPI
    {
        /// <summary>
        /// Calling SP_User_AccessToAPI stored procedure in database
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Models.Database.DBResult Call(IDatabaseContext ctx,
            Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs data,
            Models.Database.ISP<Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs> sp = null)
        {
            var connectionString = ctx.GetConnectionString();
            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_AccessToAPI(connectionString);
            var rst = sp.Call(data);
            return rst;
        }
    }
}
