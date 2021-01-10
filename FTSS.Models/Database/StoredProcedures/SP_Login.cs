using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_Login
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        /// <summary>
        /// Database token
        /// </summary>
        public string Token { get; set; }
        
        /// <summary>
        /// Database token expiration
        /// </summary>
        public DateTime ExpireDate { get; set; }
    }
}
