using System.ComponentModel.DataAnnotations;

namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_User_ChangePassword
    {
        public class Inputs : BaseDataModelWithToken
        {
            [Required]
            public string OldPassword { get; set; }

            [Required]
            [Compare("OldPassword")]
            public string NewPassword { get; set; }
        }

        public class Outputs : SingleId
        { }
    }
}