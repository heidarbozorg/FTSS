using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database
{
    public class BaseSearchParams
    {
        /// <summary>
        /// Database token
        /// </summary>
        public string Token { get; set; }

        public int StartIndex { get; set; }
        
        public int PageSize { get; set; }
    }
}
