using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.MisExtract.Rep
{
	public class Sp_AmalkardKarkhaneh_Consantre_Mojudi
	{
		public class Inputs : BaseDataModelWithToken.WAPIUserToken
		{
			public string Version_code { get; set; }
		}
		public class Outputs
		{
			public int? RowNo { get; set; }
			public string Date_ { get; set; }
			public float? Tonaj { get; set; }
			public float? PreTonaj { get; set; }
		}
	}
}
