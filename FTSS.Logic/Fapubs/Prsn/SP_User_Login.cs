using FTSS.Logic.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Fapubs.Prsn
{
	public class SP_User_Login : Models.Database.StoredProcedures.Fapubs.Prsn.SP_User_Login.Inputs
	{
        public Models.Database.DBResult Login(
                 Database.IDatabaseContextDapper_Fapubs ctx,
                 AutoMapper.IMapper mapper,
                 string key,
                 string issuer)
        {
            //Call DB
            var dbResult = ctx.SP__User_Login(this);
            if (dbResult.StatusCode != 200)
                return dbResult;

            //Convert Teacher to User object
            var user = mapper.Map<User>(dbResult.Data);

            //Generate JWT and return as the result
            return user.GetJWT(key, issuer);
        }
    }
}
