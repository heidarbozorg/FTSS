using System;
using System.ComponentModel.DataAnnotations;

namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_Admin_Login
    {
        public class Inputs
        {
            [Required(ErrorMessage = "Email is a required field.")]
            [MinLength(3, ErrorMessage = "Email should has at least 3 characters")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is a required field.")]
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