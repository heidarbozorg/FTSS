using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTSS.API.Controllers.Rep
{
	[Route("api/rep/[controller]/[action]")]
	[Authorize]
	[Filters.AuthAttribute]
	[ApiController]
	public class ConsantreController : BaseController
	{
		readonly IHttpContextAccessor _iHttpContextAccessor;
		/// <summary>
		/// Read JWT key from appsettings.json
		/// </summary>
		public string JWTKey
		{
			get
			{
				var rst = this._configuration.GetValue<string>("JWT:Key");
				return (rst);
			}
		}
		public string JWTIssuer
		{
			get
			{
				var rst = this._configuration.GetValue<string>("JWT:Issuer");
				return (rst);
			}
		}
		/// <summary>
		/// Access to appsettings.json
		/// </summary>
		public readonly IConfiguration _configuration;
		private readonly AutoMapper.IMapper _mapper;


		public ConsantreController(
			Logic.Database.IDatabaseContext_MisExtract dbCTX,
			IConfiguration configuration,
			AutoMapper.IMapper mapper, IHttpContextAccessor iHttpContextAccessor)
			: base(dbCTX)
		{
			_configuration = configuration;
			_mapper = mapper;
			_iHttpContextAccessor = iHttpContextAccessor;
		}
		/// <summary>
		/// تست توضیحات
		/// </summary>
		/// <param name="inputs"></param>
		/// <returns></returns>
		[HttpPut]
		public IActionResult GetAllErsal([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalkardKarkhaneh_Consantre_Ersal.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhaneh_Consantre_Ersal(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}
		[HttpPut]
		public IActionResult GetAllErsalMahiane([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalkardKarkhaneh_Consantre_ErsalMahiane.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhaneh_Consantre_ErsalMahiane(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}
		public IActionResult GetAllMojudi([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalkardKarkhaneh_Consantre_Mojudi.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhaneh_Consantre_Mojudi(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}
		public IActionResult GetAllMojudiMahiane([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalkardKarkhaneh_Consantre_MojudiMahiane.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhaneh_Consantre_MojudiMahiane(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}
		[HttpPut]
		public IActionResult GetAllTolid([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalkardKarkhaneh_Consantre_Tolid.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhaneh_Consantre_Tolid(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}
		[HttpPut]
		public IActionResult GetAllTolidMahiane([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalkardKarkhaneh_Consantre_TolidMahiane.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhaneh_Consantre_TolidMahiane(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}
	}
}
