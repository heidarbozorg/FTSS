using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.MisExtract.dbo
{
	public class SP_SalersHead_List
	{
		public class Inputs : BaseDataModelWithToken.WAPIUserToken
		{
			public int? SalerHeadId { get; set; }
		}
		public class Outputs
		{
			public int? SalerHeadId { get; set; }
			public string Band_Code { get; set; }
			public string SalerName { get; set; }
			public string Assistant { get; set; }
			public string ModirPhone { get; set; }
			public string ModirMobil { get; set; }
			public string SalesMan { get; set; }
			public string MFrooshPhone { get; set; }
			public string MFrooshMobil { get; set; }
			public string SalerPhone1 { get; set; }
			public string SalerPhone2 { get; set; }
			public string SalerFax { get; set; }
			public string SalerEMail { get; set; }
			public string SalerAddress { get; set; }
			public string SabtDate { get; set; }
			public int? SabtOkperson { get; set; }
			public string ECode { get; set; }
			public string Sabt_date { get; set; }
			public string sabtId { get; set; }
			public string Hesab_No { get; set; }
			public string Name_HesabNo1 { get; set; }
			public string Name_HesabNo2 { get; set; }
			public string Zamine_faliyat { get; set; }
			public string PostalCode { get; set; }
			public int? Hesab_type { get; set; }
			public string ShakhsType { get; set; }
			public string Ostan { get; set; }
			public string Shahr { get; set; }
			public string HozeCode { get; set; }
			public string Melicode { get; set; }
			public string ShenaseMeli { get; set; }
		}
	
	}
}
