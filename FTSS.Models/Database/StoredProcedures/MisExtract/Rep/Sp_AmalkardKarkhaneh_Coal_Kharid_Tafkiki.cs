using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.MisExtract.Rep
{
	public class Sp_AmalkardKarkhaneh_Coal_Kharid_Tafkiki
	{
		public class Inputs : BaseDataModelWithToken.WAPIUserToken
		{
			public string version_code { get; set; }
			public int? PeymankarEstekhrajId { get; set; }
			public int? MadanId { get; set; }
			public string FromDate { get; set; }
			public string ToDate { get; set; }
		}
		public class Outputs
		{
			public int? RowNo { get; set; }
			public string Date_ { get; set; }
			public string SahebBar { get; set; }
			public float? Tonaj { get; set; }
			public float? Khakestar { get; set; }
			public float? RotabatZaheri { get; set; }
			public float? Farrar { get; set; }
			public float? Googerd { get; set; }
			public float? Plastometri { get; set; }
			public float? RotabatAnaltic { get; set; }
		}
	}
}
