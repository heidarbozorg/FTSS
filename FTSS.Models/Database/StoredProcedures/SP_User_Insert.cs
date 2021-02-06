using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.SP_User_Insert
{
    public class Inputs : Models.Database.BaseDataModelWithToken
    {
        public string Email { get; set; }
        public string Password { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Outputs : SingleId
    { }
}
