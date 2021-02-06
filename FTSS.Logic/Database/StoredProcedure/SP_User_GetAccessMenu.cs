using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Database.StoredProcedure
{
    public class SP_User_GetAccessMenu
    {
        /// <summary>
        /// Get list of menu a user access
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Models.Database.DBResult Call(IDatabaseContext ctx,
            Models.Database.StoredProcedures.SP_Login.Outputs data)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_GetAccessMenu(connectionString);
            var spData = new Models.Database.StoredProcedures.SP_User_GetAccessMenu.Inputs()
            {
                Token = data.Token
            };
            var rst = sp.Call(spData);
            return rst;
        }
    }
}
