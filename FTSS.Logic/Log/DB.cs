﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Log
{
    /// <summary>
    /// Implement ILog for saving log at database
    /// </summary>
    public class DB : ILog
    {
        Models.Database.Interfaces.ISP _DBLogger;

        public DB(Models.Database.Interfaces.ISP DBLogger)
        {
            _DBLogger = DBLogger;
        }

        /// <summary>
        /// Log an Exception with custom message
        /// </summary>
        /// <param name="customMessage"></param>
        /// <param name="e"></param>
        public void Add(Exception e, string customMessage = null)
        {
            string text = string.Format("{0}\nException: {1}\nStackTrace: {2}\n",
                customMessage ?? "", e.Message, e.StackTrace);
            this.Add(text);
        }

        /// <summary>
        /// Log a simple text at database
        /// </summary>
        /// <param name="msg"></param>
        public void Add(string msg)
        {
            string text = string.Format("{0}: {1}\n", DateTime.Now, msg);
            _DBLogger.Call(text);
        }
    }
}
