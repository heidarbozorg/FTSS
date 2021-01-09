using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
    public interface ISP
    {
        ISPResult Call(ISPParameters Param = null);
    }
}
