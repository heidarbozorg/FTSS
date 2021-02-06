using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_User_ChangePassword : BaseDataModelWithToken
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
