using FTSS.Models.Database;
using FTSS.Models.Database.StoredProcedures.Fapubs.dbo;
using FTSS.Models.Database.StoredProcedures.Fapubs.Prsn;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Database
{
	public interface IDatabaseContextDapper_Fapubs
	{
		DBResult SP__User_Login(SP_User_Login.Inputs inputs);
		DBResult SP_Check_Token(SP_CheckToken.Inputs inputs);
	}
}
