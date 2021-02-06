using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.SP_User_SetPassword
{
    public class Inputs : Models.Database.BaseDataModelWithToken
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }

    public class Outputs : SingleId
    { }
}