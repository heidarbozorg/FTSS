using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Log
{
    /// <summary>
    /// Log messages at a text file
    /// </summary>
    public class LogAtFile : ILog
    {
        private FileIO.IFileOperation _file;

        public LogAtFile(FileIO.IFileOperation f)
        {
            if (f == null)
                throw new ArgumentNullException("Invalid FileOperation.");            

            _file = f;
        }

        public LogAtFile(string fileName = "FTSS.txt")
        {
            _file = new FileIO.TextFile(fileName);
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
        /// Log a simple text at a file
        /// </summary>
        /// <param name="msg"></param>
        public void Add(string msg)
        {
            string text = string.Format("{0}: {1}\n", DateTime.Now, msg);
            _file.Append(text);
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
