using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.MisExtract.dbo
{
	public class SP_madan_iden_List
	{
		public class Inputs : BaseDataModelWithToken.WAPIUserToken
		{
			public int? madan_id { get; set; }
		}
		public class Outputs
		{
			public int? madan_id { get; set; }
			public string date_sabt { get; set; }
			public int? sabt_id { get; set; }
			public string band_code { get; set; }
			public int? TypeMadan { get; set; }
			public string name_madan { get; set; }
			public float? ZaribeHafari { get; set; }
			public string GeoX { get; set; }
			public string GeoY { get; set; }
			public string GeoZ { get; set; }
			public int? Distance { get; set; }
			public string Roosta { get; set; }
			public string KashfYear { get; set; }
			public string StartYear { get; set; }
			public int? NoeZoghal { get; set; }
			public float? width { get; set; }
			public float? height { get; set; }
			public string rain { get; set; }
			public int? Layerskashf { get; set; }
			public int? StoreType { get; set; }
			public float? TotalStore { get; set; }
			public float? ExtractableStore { get; set; }
			public int? LayersExtract { get; set; }
			public float? Abdehi { get; set; }
			public float? Layerszekhamat { get; set; }
			public float? LayersShib { get; set; }
			public float? Gazkhizi { get; set; }
			public string Type_Openmadan { get; set; }
			public string type_madan { get; set; }
			public string Mantaghe_Id { get; set; }
		}
		
	}
}
