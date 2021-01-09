using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Log
{
    public interface ILog
    {
        void Add(Exception e, string msg = null);

        void Add(string msg);
    }
}
