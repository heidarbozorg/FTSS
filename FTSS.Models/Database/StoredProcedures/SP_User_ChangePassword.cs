using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.SP_User_ChangePassword
{
    public class Inputs : BaseDataModelWithToken
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class Outputs : SingleId
    { }
}
