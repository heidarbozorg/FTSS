using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTSS.API.Controllers.dbo
{
	[Route("api/dbo/[controller]/[action]")]
	[Authorize]
	[Filters.AuthAttribute]
	[ApiController]
	public class madanIdenController : BaseController
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
		IHttpContextAccessor _IHttpContextAccessor;

		public madanIdenController(
			Logic.Database.IDatabaseContext_MisExtract dbCTX,
			IConfiguration configuration,
			AutoMapper.IMapper mapper, IHttpContextAccessor IHttpContextAccessor)
			: base(dbCTX)
		{
			_configuration = configuration;
			_mapper = mapper;
			_IHttpContextAccessor = IHttpContextAccessor;
		}
		/// <summary>
		/// ....
		/// </summary>
		/// <param name="inputs"></param>
		/// <returns></returns>
		[HttpPut]
		public IActionResult GetAll([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.dbo.SP_madan_iden_List.Inputs inputs)
		{
			var rst = _ctx_MisExtract.SP_madan_iden_List(inputs, JWTKey, JWTIssuer, _IHttpContextAccessor);
			return FromDatabase(rst);
		}

	}
}
