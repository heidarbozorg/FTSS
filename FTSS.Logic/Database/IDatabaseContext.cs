using FTSS.Models.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Database
{
    public interface IDatabaseContext
    {
        DBResult SP_APILog_Insert(Models.Database.StoredProcedures.SP_APILog_Insert.Inputs inputs);
        DBResult SP_Login(Models.Database.StoredProcedures.SP_Login.Inputs inputs);
        DBResult SP_User_AccessToAPI(Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs inputs);
        DBResult SP_Users_GetAll(Models.Database.StoredProcedures.SP_Users_GetAll.Inputs inputs);
        DBResult SP_User_ChangePassword(Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs inputs);
        DBResult SP_User_UpdateProfile(Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs inputs);
        DBResult SP_User_SetPassword(Models.Database.StoredProcedures.SP_User_SetPassword.Inputs inputs);
        DBResult SP_User_Delete(Models.Database.StoredProcedures.SP_User_Delete.Inputs inputs);
        DBResult SP_User_Update(Models.Database.StoredProcedures.SP_User_Update.Inputs inputs);
        DBResult SP_User_Insert(Models.Database.StoredProcedures.SP_User_Insert.Inputs inputs);
    }
}
