using FTSS.Logic.CommonOperations;
using FTSS.Logic.Security;
using Microsoft.AspNetCore.Http;

namespace FTSS.Logic.Fapubs.Prsn
{
	public class SP_User_Login : Models.Database.StoredProcedures.Fapubs.Prsn.Login
    {
        public Models.Database.DBResult Login(
                 Database.IDatabaseContextDapper_Fapubs ctx,
                 AutoMapper.IMapper mapper,
                  IHttpContextAccessor _IHttpContextAccessor,
                 string key,
                 string issuer)
        {
             string IpAddress = _IHttpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var model = new Models.Database.StoredProcedures.Fapubs.Prsn.Sp_WAPIToken_Insert.Inputs()
            {
                Username=this.WAPIUserName,
                Password=this.WAPIPassword,
                IpAddress=IpAddress
            };
            var dbResultWAPIToken = ctx.Sp_WAPIToken_Insert(model);
            if (dbResultWAPIToken.StatusCode != 200)
                return dbResultWAPIToken;
            var modelLogin = new Models.Database.StoredProcedures.Fapubs.Prsn.SP_User_Login.Inputs()
            {
                Username = this.Username,
                Password = this.Password,
                IpAddress = IpAddress,
                WAPIToken= ObjectHelper.getValue<string>("Token", dbResultWAPIToken.Data),
                FCMId=this.FCMId,
                Tozihat=this.Tozihat
            };
            var dbResult = ctx.SP__User_Login(modelLogin);
            if (dbResult.StatusCode != 200)
                return dbResult;
            //Convert Teacher to User object
            var user = mapper.Map<User>(dbResult.Data);

            //Generate JWT and return as the result
            return user.GetJWT(key, issuer, modelLogin.WAPIToken);
        }
    }
}
