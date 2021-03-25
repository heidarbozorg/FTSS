using System;
using System.ComponentModel.DataAnnotations;

namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_User_Update
    {
        public class Inputs : BaseDataModelWithToken
        {
            [Required]
            [Range(1, int.MaxValue)]
            public int UserId { get; set; }

            [Required]
            [MinLength(3)]
            public string Email { get; set; }
            public string FirstName { get; set; }

            [Required]
            [MinLength(3)]
            public string LastName { get; set; }
        }

        public class Outputs : SingleId
        { }
    }
}