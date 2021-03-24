namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_User_AccessToAPI
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
}