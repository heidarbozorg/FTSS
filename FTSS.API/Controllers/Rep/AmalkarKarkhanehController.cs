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
	public class AmalkarKarkhanehController : BaseController
	{
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
		readonly IHttpContextAccessor _iHttpContextAccessor;
	
		public AmalkarKarkhanehController(
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
		/// تست توضیحات...
		/// </summary>
		/// <param name="inputs"></param>
		/// <returns></returns>
		[HttpPut]
		public IActionResult GetAllStop([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalkardKarkhanehStop5.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhanehStop5(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}
		[HttpPut]
		public IActionResult GetAllStopMahiane([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalkardKarkhanehStopMahiane5.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhanehStopMahiane5(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}
		[HttpPut]
		public IActionResult GetAllVoroodi([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalkardKarkhanehVorodi5.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhanehVorodi5(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}
		[HttpPut]
		public IActionResult GetAllVoroodiMahiane([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalkardKarkhanehVorodiMahiane5.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhanehVorodiMahiane5(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}

	}
}
