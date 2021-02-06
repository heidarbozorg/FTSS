using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.SP_User_AccessToAPI
{
    public class Inputs : BaseDataModelWithToken
    {
        public string APIAddress { get; set; }
    }

    public class Outputs
    {
        public bool Result { get; set; }
    }
}
