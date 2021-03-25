using System;
using System.ComponentModel.DataAnnotations;

namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_User_Delete
    {
        public class Inputs : BaseDataModelWithToken
        {
            [Required]
            [Range(1, int.MaxValue)]
            public int UserId { get; set; }
        }

        public class Outputs : SingleId
        { }

    }
}