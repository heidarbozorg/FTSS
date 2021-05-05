using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.MisExtract.dbo
{
	public class SP_version_List
	{
		public class Inputs : BaseDataModelWithToken.WAPIUserToken
		{
		}
		public class Outputs
		{
			public string version_code { get; set; }
			public string version_name { get; set; }
		}

	}
}
