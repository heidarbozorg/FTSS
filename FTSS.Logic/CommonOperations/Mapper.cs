using AutoMapper;

namespace FTSS.Logic.CommonOperations
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Models.API.Log, Models.Database.StoredProcedures.SP_APILog_Insert.Inputs>();

            //Map Admin to User and set the Role as Admin.
            CreateMap<Models.Database.StoredProcedures.SP_Admin_Login.Outputs, Logic.Security.User>()
                    .AfterMap((s, d) =>
                    {
                        d.Role = Security.Roles.Admin;
                        d.DbToken = s.Token;
                    });
        }
    }
}
