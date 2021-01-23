using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
    /// <summary>
    /// Filter fields
    /// </summary>
    public class SP_Users_GetAll_Params : BaseSearchParams
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
