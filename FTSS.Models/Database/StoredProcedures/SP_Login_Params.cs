using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_Login_Params : BaseSearchParams
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
