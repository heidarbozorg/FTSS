using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.SP_APILog_Insert
{
    public class Inputs
    {
        public string APIAddress { get; set; }
        public string UserToken { get; set; }
        public string Params { get; set; }
        public string Results { get; set; }
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }
    }

    public class Outputs : SingleId
    {
    }
}
