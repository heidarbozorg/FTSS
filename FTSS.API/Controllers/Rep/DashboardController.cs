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
	public class DashboardController : BaseController
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

		public DashboardController(
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
		public IActionResult GetAll([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalKardKarkhaneh.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhaneh(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}
		[HttpPut]
		public IActionResult GetAll2_1([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalKardKarkhaneh2_1.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhaneh2_1(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}
		[HttpPut]
		public IActionResult GetAll2_4([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalkardKarkhaneh2_4.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhaneh2_4(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}
		[HttpPut]
		public IActionResult GetAll2_5([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalkardKarkhaneh2_5.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhaneh2_5(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}
	}
}
