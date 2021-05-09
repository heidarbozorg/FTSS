using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Log
{
    public interface ILog
    {
        void Add(Exception e, string customMessage = null);

        void Add(string message);

        void Information(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(string message);
    }
}