using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Log
{
    /// <summary>
    /// Log messages at a text file
    /// </summary>
    public class File : ILog
    {
        private FileIO.IFileOperation _file;

        public File(FileIO.IFileOperation f = null, string fileName = "FTSS.txt")
        {
            if (f == null)
                _file = new FileIO.TextFile(fileName);
            else
                _file = f;
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
        /// Log a simple text
        /// </summary>
        /// <param name="msg"></param>
        public void Add(string msg)
        {
            string text = string.Format("{0}: {1}\n", DateTime.Now, msg);
            _file.Append(text);
        }
    }
}
