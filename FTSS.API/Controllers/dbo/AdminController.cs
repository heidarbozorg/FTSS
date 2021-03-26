using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FTSS.API.Controllers.dbo
{
    [Route("/api/dbo/[controller]/[action]")]
    [ApiController]
    public class AdminController : BaseController
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


        public AdminController(Logic.Database.IDatabaseContext dbCTX, IConfiguration configuration,
            AutoMapper.IMapper mapper)
            : base(dbCTX)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// Login and get database token
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login([FromBody] Models.Database.StoredProcedures.SP_Admin_Login.Inputs inputs)
        {
            var rst = _ctx.SP_Admin_Login(inputs);
            //Generate JWT
            if (rst.ErrorCode == 200)
            {
                var jwt = Logic.Security.UserJWT.Get(rst, JWTKey, JWTIssuer, _mapper);
                rst = new Models.Database.DBResult(200, "", jwt);
            }

            return FromDatabase(rst);
        }
    }
}
