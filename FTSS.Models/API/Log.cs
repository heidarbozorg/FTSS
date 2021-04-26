using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.API
{
    /// <summary>
    /// Model for logging APIs
    /// </summary>
    public class Log
    {
        /// <summary>
        /// The API full address
        /// </summary>
        public string APIAddress { get; set; }

        /// <summary>
        /// Database user's token who call the API
        /// </summary>
        public string UserToken { get; set; }

        /// <summary>
        /// API's input parameters
        /// </summary>
        public string Params { get; set; }

        /// <summary>
        /// API's output results
        /// </summary>
        public string Results { get; set; }

        /// <summary>
        /// API error message (if exists)
        /// </summary>
        public string ErrorMessage_ { get; set; }

        /// <summary>
        /// RESTFul http code result
        /// </summary>
        public int StatusCode { get; set; }
    }
}
