using AutoMapper;

namespace FTSS.Logic.CommonOperations
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Models.API.Log, Models.Database.StoredProcedures.SP_APILog_Insert.Inputs>();
        }
    }
}
