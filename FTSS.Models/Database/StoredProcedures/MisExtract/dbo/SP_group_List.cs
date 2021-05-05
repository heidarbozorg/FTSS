using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.MisExtract.dbo
{
	public class SP_group_List
	{
		public class Inputs : BaseDataModelWithToken.WAPIUserToken
		{
			public int? group_id { get; set; }
		}
		public class Outputs
		{
			public int? GId { get; set; }
			public int? ParamId { get; set; }
			public string GroupId { get; set; }
			public string PId { get; set; }
			public string GroupName { get; set; }
			public Int16 LstLvl { get; set; }
			public string PGroupId { get; set; }
			public string RegisterDate { get; set; }
			public int? RegisterPrsn { get; set; }
			public string Version_Code { get; set; }
		}
	
	}
}
