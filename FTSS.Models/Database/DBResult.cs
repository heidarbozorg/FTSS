using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database
{
    /// <summary>
    /// Result of calling stored procedures
    /// </summary>
    public class DBResult
    {
        /// <summary>
        /// The query result, can be a list or just a simple Id
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// All record count by the current condition
        /// </summary>
        public int ActualSize { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public DBResult()
        {
        }

        public DBResult(int errorCode, string errorMessage, object data = null, int actualSize = 0)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;

            this.Data = data;
            if (actualSize >= 0)
                this.ActualSize = actualSize;
            else
                this.ActualSize = data == null ? 0 : 1;
        }        
    }
}