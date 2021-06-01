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
	public class CoalController : BaseController
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


		public CoalController(
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
		public IActionResult GetAllKharid([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalkardKarkhaneh_Coal_Kharid.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhaneh_Coal_Kharid(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}
		[HttpPut]
		public IActionResult GetAllKharidMahiane([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalkardKarkhaneh_Coal_KharidMahiane.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhaneh_Coal_KharidMahiane(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}
		[HttpPut]
		public IActionResult GetAllMojudi([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalkardKarkhaneh_Coal_Mojudi.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhaneh_Coal_Mojudi(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}
		[HttpPut]
		public IActionResult GetAllMojudiMahiane([FromBody] FTSS.Models.Database.StoredProcedures.MisExtract.Rep.Sp_AmalkardKarkhaneh_Coal_MojudiMahiane.Inputs inputs)
		{
			var rst = _ctx_MisExtract.Sp_AmalkardKarkhaneh_Coal_MojudiMahiane(inputs, JWTKey, JWTIssuer, _iHttpContextAccessor);
			return FromDatabase(rst);
		}
	}
}
