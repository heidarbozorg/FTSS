using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.MisExtract.Rep
{
	public class Sp_AmalKardKarkhaneh2_1
	{
		public class Inputs : BaseDataModelWithToken.WAPIUserToken
		{
			public string ToDate { get; set; }
			public string version_code { get; set; }
		}
		public class Outputs
		{
			public string name_madan { get; set; }
			public int? TedadRooz { get; set; }
			public int? Rooz { get; set; }
			public int? TedadMah { get; set; }
			public int? Mah { get; set; }
			public int? TedadSal { get; set; }
			public int? Sal { get; set; }
		}
		
	}
}
