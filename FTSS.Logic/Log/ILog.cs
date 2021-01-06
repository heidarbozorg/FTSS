using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Log
{
    public interface ILog
    {
        void Add(string msg, Exception e);

        void Add(string msg);
    }
}
