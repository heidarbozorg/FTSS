using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.MisExtract.Rep
{
	public class Sp_AmalkardKarkhaneh_Coal_Mojudi
	{
		public class Inputs : BaseDataModelWithToken.WAPIUserToken
		{
			public string Version_code { get; set; }
			public char? FromDate { get; set; }
			public char? ToDate { get; set; }
		}
		public class Outputs
		{
			public int? RowNo { get; set; }
			public char? Date_ { get; set; }
			public float? Tonaj { get; set; }
			public float? PreTonaj { get; set; }
		}
	}
}
