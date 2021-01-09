using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.Interfaces
{
    public interface ISP
    {
        DBResult Call(params object[] param);
    }
}
