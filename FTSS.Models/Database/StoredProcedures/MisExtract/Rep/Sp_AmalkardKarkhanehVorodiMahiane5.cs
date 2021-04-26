using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.MisExtract.Rep
{
	public class Sp_AmalkardKarkhanehVorodiMahiane5
	{
		public class Inputs : BaseDataModelWithToken.WAPIUserToken
		{
		public string Version_code { get; set; }
		}
		public class Outputs
		{
			public int? RowNo { get; set; }
			public char? Date_ { get; set; }
			public float? Tonaj { get; set; }
			public float? Khakestar { get; set; }
			public float? RotabatZaheri { get; set; }
			public float? Farrar { get; set; }
			public float? Googerd { get; set; }
			public float? Plastometri { get; set; }
			public float? RotabatAnaltic { get; set; }
			public float? Tavarom { get; set; }
			public float? PreTonaj { get; set; }
			public float? PreKhakestar { get; set; }
			public float? PreRotabatZaheri { get; set; }
			public float? PreFarrar { get; set; }
			public float? PreGoogerd { get; set; }
			public float? PrePlastometri { get; set; }
			public float? PreRotabatAnaltic { get; set; }
			public float? PreTavarom { get; set; }
		}
		
	}
}
