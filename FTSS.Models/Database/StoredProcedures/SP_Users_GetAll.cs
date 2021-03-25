namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_Users_GetAll
    {
        public class Inputs : BaseSearchParams
        {
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }


        public class Outputs
        {
            public int UserId { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

        }
    }
}