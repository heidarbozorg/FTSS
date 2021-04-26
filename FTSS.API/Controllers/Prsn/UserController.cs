using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTSS.API.Controllers.Prsn
{
	[Route("api/prsn/[controller]/[action]")]
	[ApiController]
	public class UserController : BaseController
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


		public UserController(
			Logic.Database.IDatabaseContextDapper_Fapubs dbCTX,
			IConfiguration configuration,
			AutoMapper.IMapper mapper)
			: base(dbCTX)
		{
			_configuration = configuration;
			_mapper = mapper;
		}
		/// <summary>
		/// جهت ورود کاربران
		/// </summary>
		/// <param name="inputs"></param>
		/// <returns></returns>
		[HttpPut]
		public IActionResult Login([FromBody] FTSS.Logic.Fapubs.Prsn.SP_User_Login inputs)
		{
			var rst = inputs.Login(_ctx_Fapubs, _mapper, JWTKey, JWTIssuer);
			return FromDatabase(rst);
		}
	
	}
}
