using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.Fapubs.dbo
{
	public class SP_CheckToken
	{
		public class Inputs : BaseDataModelWithToken.WAPIUserToken
		{

		}
		public class Outputs
		{
			public int? WAPIUserId { get; set; }
			public int? PersonelId { get; set; }
		}
	}
}
