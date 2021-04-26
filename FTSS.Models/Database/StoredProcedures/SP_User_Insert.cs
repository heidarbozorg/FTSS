using System.ComponentModel.DataAnnotations;

namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_User_Insert
    {
        public class Inputs : BaseDataModelWithToken
        {
            [Required]
            [MinLength(3)]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
            public string FirstName { get; set; }

            [Required]
            [MinLength(3)]
            public string LastName { get; set; }
        }

        public class Outputs : SingleId
        { }
    }
}