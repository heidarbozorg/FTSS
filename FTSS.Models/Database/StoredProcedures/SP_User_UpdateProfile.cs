using System.ComponentModel.DataAnnotations;

namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_User_UpdateProfile
    {
        public class Inputs : BaseDataModelWithToken
        {
            public string FirstName { get; set; }

            [Required]
            [MinLength(3)]
            public string LastName { get; set; }            
        }

        public class Outputs : SingleId
        { }
    }
}