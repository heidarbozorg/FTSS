using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.SP_User_Delete
{
    public class Inputs : Models.Database.BaseDataModelWithToken
    {
        public int UserId { get; set; }
    }

    public class Outputs : SingleId
    { }

}