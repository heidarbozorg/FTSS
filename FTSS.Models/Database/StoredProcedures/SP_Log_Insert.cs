using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_Log_Insert_Param : ISPParameters
    {
    }

    public class SP_Log_Insert_Result : ISPResult
    {
    }

    public class SP_Log_Insert : ISP
    {
        public ISPResult Call(ISPParameters Param = null)
        {
            throw new NotImplementedException();
        }
    }
}
