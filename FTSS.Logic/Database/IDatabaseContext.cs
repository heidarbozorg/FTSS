using FTSS.Models.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Database
{
    public interface IDatabaseContext
    {
        string GetConnectionString();
        SqlConnection GetSqlConnection();

        DBResult SP_Login(Models.Database.StoredProcedures.SP_Login.Inputs inputs);
        DBResult SP_User_AccessToAPI(Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs inputs);
        DBResult SP_Users_GetAll(Models.Database.StoredProcedures.SP_Users_GetAll.Inputs inputs);
        DBResult SP_User_ChangePassword(Models.Database.StoredProcedures.SP_User_ChangePassword inputs);
        DBResult SP_User_UpdateProfile(Models.Database.Tables.Users inputs);
        DBResult SP_User_SetPassword(Models.Database.Tables.Users inputs);
        DBResult SP_User_Delete(Models.Database.Tables.Users inputs);
        DBResult SP_User_Update(Models.Database.Tables.Users inputs);
        DBResult SP_User_Insert(Models.Database.Tables.Users inputs);

        

    }
}
