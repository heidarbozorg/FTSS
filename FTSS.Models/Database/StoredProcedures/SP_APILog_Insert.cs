using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.SP_APILog_Insert
{
    public class Inputs
    {
        public int APILogId_Parent { get; set; }
        public string APIAddress { get; set; }
        public string UserToken { get; set; }
        public string DataJSON { get; set; }
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }
    }

    public class Outputs : SingleId
    {
    }
}
