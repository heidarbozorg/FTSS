using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_Log_Insert_Result : ISPResult
    {
    }

    public class SP_Log_Insert : ISP
    {
        public ISPResult Call(params object[] param)
        {
            //var rst = new SP_Log_Insert_Result();
            //return rst;
            throw new NotImplementedException();
        }
    }
}
