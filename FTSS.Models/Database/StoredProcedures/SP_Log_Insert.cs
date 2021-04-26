namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_Log_Insert
    {
        public class Inputs : IInputs
        {
            public string MSG { get; set; }
            public string IPAddress { get; set; }
        }

        public class Outputs : SingleId
        {
        }
    }
}