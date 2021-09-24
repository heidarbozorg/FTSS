
using FTSS.Logic.Security;
using FTSS.Models.Database;
using FTSS.Models.Database.StoredProcedures;
using FTSS.Models.Database.StoredProcedures.MisExtract.dbo;
using FTSS.Models.Database.StoredProcedures.MisExtract.Rep;
using Microsoft.AspNetCore.Http;
using System;

namespace FTSS.Logic.Database
{
	/// <summary>
	/// Implement IDatabaseContext by Dapper ORM
	/// </summary>
	public class DatabaseContextDapper_MisExtract : IDatabaseContext_MisExtract
	{
		#region properties
		private string _connectionString { get; set; }

		private string GetConnectionString()
		{
			return _connectionString;
		}
		#endregion properties

		private readonly DP.DapperORM.BaseSP<SP_APILog_Insert.Inputs, SP_APILog_Insert.Outputs> _SP_APILog_Insert;
		private readonly DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Coal_Kharid.Inputs, Sp_AmalkardKarkhaneh_Coal_Kharid.Outputs> _Sp_AmalkardKarkhaneh_Coal_Kharid;
		private readonly DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Coal_KharidMahiane.Inputs, Sp_AmalkardKarkhaneh_Coal_KharidMahiane.Outputs> _Sp_AmalkardKarkhaneh_Coal_KharidMahiane;
		private readonly DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Coal_Mojudi.Inputs, Sp_AmalkardKarkhaneh_Coal_Mojudi.Outputs> _Sp_AmalkardKarkhaneh_Coal_Mojudi;
		private readonly DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Coal_Kharid_Tafkiki.Inputs, Sp_AmalkardKarkhaneh_Coal_Kharid_Tafkiki.Outputs> _Sp_AmalkardKarkhaneh_Coal_Kharid_Tafkiki;
		private readonly DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Coal_MojudiMahiane.Inputs, Sp_AmalkardKarkhaneh_Coal_MojudiMahiane.Outputs> _Sp_AmalkardKarkhaneh_Coal_MojudiMahiane;
		private readonly DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Consantre_Ersal.Inputs, Sp_AmalkardKarkhaneh_Consantre_Ersal.Outputs> _Sp_AmalkardKarkhaneh_Consantre_Ersal;
		private readonly DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Consantre_ErsalMahiane.Inputs, Sp_AmalkardKarkhaneh_Consantre_ErsalMahiane.Outputs> _Sp_AmalkardKarkhaneh_Consantre_ErsalMahiane;
		private readonly DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Consantre_Mojudi.Inputs, Sp_AmalkardKarkhaneh_Consantre_Mojudi.Outputs> _Sp_AmalkardKarkhaneh_Consantre_Mojudi;
		private readonly DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Consantre_MojudiMahiane.Inputs, Sp_AmalkardKarkhaneh_Consantre_MojudiMahiane.Outputs> _Sp_AmalkardKarkhaneh_Consantre_MojudiMahiane;
		private readonly DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Consantre_Tolid.Inputs, Sp_AmalkardKarkhaneh_Consantre_Tolid.Outputs> _Sp_AmalkardKarkhaneh_Consantre_Tolid;
		private readonly DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Consantre_TolidMahiane.Inputs, Sp_AmalkardKarkhaneh_Consantre_TolidMahiane.Outputs> _Sp_AmalkardKarkhaneh_Consantre_TolidMahiane;
		private readonly DP.DapperORM.BaseSP<Sp_AmalkardKarkhanehStop5.Inputs, Sp_AmalkardKarkhanehStop5.Outputs> _Sp_AmalkardKarkhanehStop5;
		private readonly DP.DapperORM.BaseSP<Sp_AmalkardKarkhanehStopMahiane5.Inputs, Sp_AmalkardKarkhanehStopMahiane5.Outputs> _Sp_AmalkardKarkhanehStopMahiane5;
		private readonly DP.DapperORM.BaseSP<Sp_AmalkardKarkhanehVorodi5.Inputs, Sp_AmalkardKarkhanehVorodi5.Outputs> _Sp_AmalkardKarkhanehVorodi5;
		private readonly DP.DapperORM.BaseSP<Sp_AmalkardKarkhanehVorodiMahiane5.Inputs, Sp_AmalkardKarkhanehVorodiMahiane5.Outputs> _Sp_AmalkardKarkhanehVorodiMahiane5;
		private readonly DP.DapperORM.BaseSP<Sp_AmalKardKarkhaneh.Inputs, Sp_AmalKardKarkhaneh.Outputs> _Sp_AmalkardKarkhaneh;
		private readonly DP.DapperORM.BaseSP<Sp_AmalKardKarkhaneh2_1.Inputs, Sp_AmalKardKarkhaneh2_1.Outputs> _Sp_AmalkardKarkhaneh2_1;
		private readonly DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh2_4.Inputs, Sp_AmalkardKarkhaneh2_4.Outputs> _Sp_AmalkardKarkhaneh2_4;
		private readonly DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh2_5.Inputs, Sp_AmalkardKarkhaneh2_5.Outputs> _Sp_AmalkardKarkhaneh2_5;
		private readonly DP.DapperORM.BaseSP<SP_madan_iden_List.Inputs, SP_madan_iden_List.Outputs> _SP_madan_iden_List;
		private readonly DP.DapperORM.BaseSP<SP_SalersHead_List.Inputs, SP_SalersHead_List.Outputs> _SP_SalersHead_List;
		private readonly DP.DapperORM.BaseSP<SP_group_List.Inputs, SP_group_List.Outputs> _SP_group_Lis;
		private readonly DP.DapperORM.BaseSP<SP_version_List.Inputs, SP_version_List.Outputs> _SP_version_List;
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ConnectionString"></param>
		public DatabaseContextDapper_MisExtract(string ConnectionString, DP.DapperORM.ISqlExecuter executer = null)
		{
			if (string.IsNullOrWhiteSpace(ConnectionString))
				throw new ArgumentNullException("رشته اتصال یافت نشد!");

			_connectionString = ConnectionString;
			if (executer == null)
				executer = new DP.DapperORM.SqlExecuter(GetConnectionString());
			_SP_APILog_Insert = new DP.DapperORM.BaseSP<SP_APILog_Insert.Inputs, SP_APILog_Insert.Outputs>("SP_APILog_Insert", executer);
			_Sp_AmalkardKarkhaneh_Coal_Kharid = new DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Coal_Kharid.Inputs, Sp_AmalkardKarkhaneh_Coal_Kharid.Outputs>("Rep.Sp_AmalkardKarkhaneh_Coal_Kharid", executer);
			_Sp_AmalkardKarkhaneh_Coal_KharidMahiane = new DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Coal_KharidMahiane.Inputs, Sp_AmalkardKarkhaneh_Coal_KharidMahiane.Outputs>("Rep.Sp_AmalkardKarkhaneh_Coal_KharidMahiane", executer);
			_Sp_AmalkardKarkhaneh_Coal_MojudiMahiane = new DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Coal_MojudiMahiane.Inputs, Sp_AmalkardKarkhaneh_Coal_MojudiMahiane.Outputs>("Rep.Sp_AmalkardKarkhaneh_Coal_MojudiMahiane", executer);
			_Sp_AmalkardKarkhaneh_Coal_Mojudi = new DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Coal_Mojudi.Inputs, Sp_AmalkardKarkhaneh_Coal_Mojudi.Outputs>("Rep.Sp_AmalkardKarkhaneh_Coal_Mojudi", executer);
			_Sp_AmalkardKarkhaneh_Coal_Kharid_Tafkiki= new DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Coal_Kharid_Tafkiki.Inputs, Sp_AmalkardKarkhaneh_Coal_Kharid_Tafkiki.Outputs>("Rep.Sp_AmalkardKarkhaneh_Coal_Kharid_Tafkiki", executer);
			_Sp_AmalkardKarkhaneh_Consantre_Ersal = new DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Consantre_Ersal.Inputs, Sp_AmalkardKarkhaneh_Consantre_Ersal.Outputs>("Rep.Sp_AmalkardKarkhaneh_Consantre_Ersal", executer);
			_Sp_AmalkardKarkhaneh_Consantre_ErsalMahiane = new DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Consantre_ErsalMahiane.Inputs, Sp_AmalkardKarkhaneh_Consantre_ErsalMahiane.Outputs>("Rep.Sp_AmalkardKarkhaneh_Consantre_ErsalMahiane", executer);
			_Sp_AmalkardKarkhaneh_Consantre_Mojudi = new DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Consantre_Mojudi.Inputs, Sp_AmalkardKarkhaneh_Consantre_Mojudi.Outputs>("Rep.Sp_AmalkardKarkhaneh_Consantre_Mojudi", executer);
			_Sp_AmalkardKarkhaneh_Consantre_MojudiMahiane = new DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Consantre_MojudiMahiane.Inputs, Sp_AmalkardKarkhaneh_Consantre_MojudiMahiane.Outputs>("Rep.Sp_AmalkardKarkhaneh_Consantre_MojudiMahiane", executer);
			_Sp_AmalkardKarkhaneh_Consantre_Tolid = new DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Consantre_Tolid.Inputs, Sp_AmalkardKarkhaneh_Consantre_Tolid.Outputs>("Rep.Sp_AmalkardKarkhaneh_Consantre_Tolid", executer);
			_Sp_AmalkardKarkhaneh_Consantre_TolidMahiane = new DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh_Consantre_TolidMahiane.Inputs, Sp_AmalkardKarkhaneh_Consantre_TolidMahiane.Outputs>("Rep.Sp_AmalkardKarkhaneh_Consantre_TolidMahiane", executer);
			_Sp_AmalkardKarkhanehStop5 = new DP.DapperORM.BaseSP<Sp_AmalkardKarkhanehStop5.Inputs, Sp_AmalkardKarkhanehStop5.Outputs>("Rep.Sp_AmalkardKarkhanehStop5", executer);
			_Sp_AmalkardKarkhanehStopMahiane5 = new DP.DapperORM.BaseSP<Sp_AmalkardKarkhanehStopMahiane5.Inputs, Sp_AmalkardKarkhanehStopMahiane5.Outputs>("Rep.Sp_AmalkardKarkhanehStopMahiane5", executer);
			_Sp_AmalkardKarkhanehVorodi5 = new DP.DapperORM.BaseSP<Sp_AmalkardKarkhanehVorodi5.Inputs, Sp_AmalkardKarkhanehVorodi5.Outputs>("Rep.Sp_AmalkardKarkhanehVorodi5", executer);
			_Sp_AmalkardKarkhanehVorodiMahiane5 = new DP.DapperORM.BaseSP<Sp_AmalkardKarkhanehVorodiMahiane5.Inputs, Sp_AmalkardKarkhanehVorodiMahiane5.Outputs>("Rep.Sp_AmalkardKarkhanehVorodiMahiane5", executer);
			_Sp_AmalkardKarkhaneh = new DP.DapperORM.BaseSP<Sp_AmalKardKarkhaneh.Inputs, Sp_AmalKardKarkhaneh.Outputs>("Rep.Sp_AmalkardKarkhaneh", executer);
			_Sp_AmalkardKarkhaneh2_1 = new DP.DapperORM.BaseSP<Sp_AmalKardKarkhaneh2_1.Inputs, Sp_AmalKardKarkhaneh2_1.Outputs>("Rep.Sp_AmalkardKarkhaneh2_1_2", executer);
			_Sp_AmalkardKarkhaneh2_4 = new DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh2_4.Inputs, Sp_AmalkardKarkhaneh2_4.Outputs>("Rep.Sp_AmalkardKarkhaneh2_4", executer);
			_Sp_AmalkardKarkhaneh2_5 = new DP.DapperORM.BaseSP<Sp_AmalkardKarkhaneh2_5.Inputs, Sp_AmalkardKarkhaneh2_5.Outputs>("Rep.Sp_AmalkardKarkhaneh2_5", executer);
			_SP_madan_iden_List = new DP.DapperORM.BaseSP<SP_madan_iden_List.Inputs, SP_madan_iden_List.Outputs>("dbo.SP_madan_iden_List", executer);
			_SP_SalersHead_List = new DP.DapperORM.BaseSP<SP_SalersHead_List.Inputs, SP_SalersHead_List.Outputs>("dbo.SP_SalersHead_List", executer);
			_SP_group_Lis = new DP.DapperORM.BaseSP<SP_group_List.Inputs, SP_group_List.Outputs>("dbo.SP_group_List", executer);
			_SP_version_List = new DP.DapperORM.BaseSP<SP_version_List.Inputs, SP_version_List.Outputs>("dbo.SP_version_List", executer);
		}
		#region SPs
		public DBResult SP_APILog_Insert(SP_APILog_Insert.Inputs inputs)
		{
			var rst = _SP_APILog_Insert.Single(inputs);
			return rst;
		}
		public DBResult Sp_AmalkardKarkhaneh_Coal_Kharid(Sp_AmalkardKarkhaneh_Coal_Kharid.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhaneh_Coal_Kharid.Query(inputs);
			return rst;
		}

		public DBResult Sp_AmalkardKarkhaneh_Coal_KharidMahiane(Sp_AmalkardKarkhaneh_Coal_KharidMahiane.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhaneh_Coal_KharidMahiane.Query(inputs);
			return rst;
		}
		public DBResult Sp_AmalkardKarkhaneh_Coal_Mojudi(Sp_AmalkardKarkhaneh_Coal_Mojudi.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhaneh_Coal_Mojudi.Query(inputs);
			return rst;
		}
		public DBResult Sp_AmalkardKarkhaneh_Coal_Kharid_Tafkiki(Sp_AmalkardKarkhaneh_Coal_Kharid_Tafkiki.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhaneh_Coal_Kharid_Tafkiki.Query(inputs);
			return rst;
		}
		public DBResult Sp_AmalkardKarkhaneh_Coal_MojudiMahiane(Sp_AmalkardKarkhaneh_Coal_MojudiMahiane.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhaneh_Coal_MojudiMahiane.Query(inputs);
			return rst;
		}
		public DBResult Sp_AmalkardKarkhaneh_Consantre_Ersal(Sp_AmalkardKarkhaneh_Consantre_Ersal.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhaneh_Consantre_Ersal.Query(inputs);
			return rst;
		}
		public DBResult Sp_AmalkardKarkhaneh_Consantre_ErsalMahiane(Sp_AmalkardKarkhaneh_Consantre_ErsalMahiane.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhaneh_Consantre_ErsalMahiane.Query(inputs);
			return rst;
		}
		public DBResult Sp_AmalkardKarkhaneh_Consantre_Mojudi(Sp_AmalkardKarkhaneh_Consantre_Mojudi.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhaneh_Consantre_Mojudi.Query(inputs);
			return rst;
		}
		public DBResult Sp_AmalkardKarkhaneh_Consantre_MojudiMahiane(Sp_AmalkardKarkhaneh_Consantre_MojudiMahiane.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhaneh_Consantre_MojudiMahiane.Query(inputs);
			return rst;
		}

		public DBResult Sp_AmalkardKarkhaneh_Consantre_Tolid(Sp_AmalkardKarkhaneh_Consantre_Tolid.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhaneh_Consantre_Tolid.Query(inputs);
			return rst;
		}
		public DBResult Sp_AmalkardKarkhaneh_Consantre_TolidMahiane(Sp_AmalkardKarkhaneh_Consantre_TolidMahiane.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhaneh_Consantre_TolidMahiane.Query(inputs);
			return rst;
		}
		public DBResult Sp_AmalkardKarkhanehStop5(Sp_AmalkardKarkhanehStop5.Inputs inputs,string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhanehStop5.Query(inputs);
			return rst;
		}
		public DBResult Sp_AmalkardKarkhanehStopMahiane5(Sp_AmalkardKarkhanehStopMahiane5.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhanehStopMahiane5.Query(inputs);
			return rst;
		}
		public DBResult Sp_AmalkardKarkhanehVorodi5(Sp_AmalkardKarkhanehVorodi5.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhanehVorodi5.Query(inputs);
			return rst;
		}
		public DBResult Sp_AmalkardKarkhanehVorodiMahiane5(Sp_AmalkardKarkhanehVorodiMahiane5.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhanehVorodiMahiane5.Query(inputs);
			return rst;
		}
		public DBResult Sp_AmalkardKarkhaneh(Sp_AmalKardKarkhaneh.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhaneh.Query(inputs);
			return rst;
		}
		public DBResult Sp_AmalkardKarkhaneh2_1(Sp_AmalKardKarkhaneh2_1.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhaneh2_1.Query(inputs);
			return rst;
		}
		public DBResult Sp_AmalkardKarkhaneh2_4(Sp_AmalkardKarkhaneh2_4.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhaneh2_4.Query(inputs);
			return rst;
		}
		public DBResult Sp_AmalkardKarkhaneh2_5(Sp_AmalkardKarkhaneh2_5.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _Sp_AmalkardKarkhaneh2_5.Query(inputs);
			return rst;

		}
		public DBResult SP_madan_iden_List(SP_madan_iden_List.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _SP_madan_iden_List.Query(inputs);
			return rst;

		}
		public DBResult SP_SalersHead_List(SP_SalersHead_List.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _SP_SalersHead_List.Query(inputs);
			return rst;

		}
		public DBResult SP_group_List(SP_group_List.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _SP_group_Lis.Query(inputs);
			return rst;

		}
		public DBResult SP_version_List(SP_version_List.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
		{
			inputs.UserToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).Token;
			inputs.WAPIToken = User.GetUserModel(key, issuer, _IHttpContextAccessor).WAPIToken;
			var rst = _SP_version_List.Query(inputs);
			return rst;

		}
		#endregion SPs
	}
}