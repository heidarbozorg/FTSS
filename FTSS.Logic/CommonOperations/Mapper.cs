using AutoMapper;

namespace FTSS.Logic.CommonOperations
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Models.API.Log, Models.Database.StoredProcedures.SP_APILog_Insert.Inputs>();

            //Map Admin to User.
            CreateMap<Models.Database.StoredProcedures.Fapubs.Prsn.SP_User_Login.Outputs, Logic.Security.User>();
        }
    }
}
