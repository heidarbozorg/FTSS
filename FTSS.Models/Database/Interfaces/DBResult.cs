using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.Interfaces
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
        public int ActualLength { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public DBResult()
        {
        }

        public DBResult(int errorCode, string errorMessage, object data = null)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;

            this.Data = data;
        }
    }
}