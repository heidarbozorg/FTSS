using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.MisExtract.Rep
{
	public class Sp_AmalKardKarkhaneh
	{
		public class Inputs : BaseDataModelWithToken.WAPIUserToken
		{
			public string ToDate { get; set; }
			public string version_code { get; set; }
		}
		public class Outputs
		{
			public int? Id { get; set; }
			public string Sharh { get; set; }
			public decimal? Rooz { get; set; }
			public decimal? Sal { get; set; }
			public decimal? AvgMah { get; set; }
			public decimal? AvgSal { get; set; }
			public decimal? Mah { get; set; }
			public decimal? MaxMah { get; set; }
			public decimal? MaxSal { get; set; }
			public decimal? MinMah { get; set; }
			public decimal? MinSal { get; set; }
			public decimal? SalGhablMah { get; set; }
			public decimal? SalGhablSal { get; set; }
		}
		
	}
}
