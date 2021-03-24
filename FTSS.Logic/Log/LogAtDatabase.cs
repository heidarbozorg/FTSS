using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Log
{
    /// <summary>
    /// Implement ILog for saving log at database
    /// </summary>
    public class LogAtDatabase : ILog
    {
        private readonly Logic.Database.IDatabaseContext _ctx;        

        public LogAtDatabase(Logic.Database.IDatabaseContext ctx)
        {
            if (ctx == null)
                throw new ArgumentNullException("Invalid DatabaseContext.");
            _ctx = ctx;           
        }

        /// <summary>
        /// Log an Exception with custom message
        /// </summary>
        /// <param name="customMessage"></param>
        /// <param name="e"></param>
        public void Add(Exception e, string customMessage = null)
        {
            if (e == null)
                throw new ArgumentNullException("Invalid exception.");

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
            if (string.IsNullOrEmpty(msg))
                throw new ArgumentNullException("Message could not be empty.");

            string text = string.Format("{0}", msg);
            var data = new Models.Database.StoredProcedures.SP_Log_Insert.Inputs()
            {
                MSG = text
            };
            //_ctx.SP_Log_Insert(data);
        }

        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Information(string message)
        {
            throw new NotImplementedException();
        }

        public void Warning(string message)
        {
            throw new NotImplementedException();
        }
    }
}
