using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_Login
    {
        public class Inputs
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Outputs
        {
            public string Token { get; set; }
            public int UserId { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            /// <summary>
            /// User role definition in database
            /// </summary>
            public string RoleTitle { get; set; }


            /// <summary>
            /// Database token expiration
            /// </summary>
            public DateTime ExpireDate { get; set; }
        }
    }
}