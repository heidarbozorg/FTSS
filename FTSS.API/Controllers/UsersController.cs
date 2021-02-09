using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FTSS.API.Extensions;
using FTSS.API.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FTSS.API.Controllers
{
    [Route("/api/[controller]/[action]")]
    [APILogger]
    public class UsersController : BaseController
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
        private readonly ILogger _logger2;

        public UsersController(Logic.Database.IDatabaseContext dbCTX, Logic.Log.ILog logger, IConfiguration configuration, 
            ILogger<UsersController> logger2) 
            : base(dbCTX, logger)
        {
            _configuration = configuration;
            _logger2 = logger2;
        }

        /// <summary>
        /// Login and get database token
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login([FromBody] Models.Database.StoredProcedures.SP_Login.Inputs inputs)
        {
            try
            {
                var rst = _ctx.SP_Login(inputs);
                //Generate JWT
                if (rst.ErrorCode == 200)
                {
                    var jwt = new Logic.Security.UserJWT(rst, JWTKey, JWTIssuer);
                    rst = new Models.Database.DBResult(200, "", jwt);
                }

                return FromDatabase(rst);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UsersController.Login(filterParams)");
                return Problem(e.Message, e.StackTrace, 500, "Error in Login");
            }
        }

        /// <summary>
        /// Search between all users by filter parameters
        /// </summary>
        /// <param name="data">
        /// Filter parameters
        /// </param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Filters.Auth]
        public IActionResult GetAll([FromBody] Models.Database.StoredProcedures.SP_Users_GetAll.Inputs filterParams)
        {
            try
            {
                _logger2.LogWarning("Warning! Get all start", filterParams);
                if (!filterParams.IsValid())
                    return StatusCode(400, filterParams.ValidationResults);     //Bad request

                filterParams.Token = User.GetToken();
                var dbResult = _ctx.SP_Users_GetAll(filterParams);
                return FromDatabase(dbResult);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UsersController.GetAll(filterParams)");
                return Problem(e.Message, e.StackTrace, 500, "Error in GetAll");
            }
        }

        /// <summary>
        /// Add new user to database
        /// </summary>
        /// <param name="data">
        /// User info
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Filters.Auth]
        [Authorize]
        public IActionResult Insert([FromBody] Models.Database.StoredProcedures.SP_User_Insert.Inputs data)
        {
            try
            {
                data.Token = User.GetToken();
                var rst = _ctx.SP_User_Insert(data);
                return FromDatabase(rst);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UsersController.Insert(data)");
                return Problem(e.Message, e.StackTrace, 500, "Error in Insert");
            }
        }

        /// <summary>
        /// Update a user info by admin
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        [Filters.Auth]
        [Authorize]
        public IActionResult Update([FromBody] Models.Database.StoredProcedures.SP_User_Update.Inputs data)
        {
            try
            {
                data.Token = User.GetToken();
                var rst = _ctx.SP_User_Update(data);
                return FromDatabase(rst);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UsersController.Update(data)");
                return Problem(e.Message, e.StackTrace, 500, "Error in Update");
            }
        }

        /// <summary>
        /// Delete a user by admin
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpDelete]
        [Filters.Auth]
        [Authorize]
        public IActionResult Delete([FromBody] Models.Database.StoredProcedures.SP_User_Delete.Inputs data)
        {
            try
            {
                data.Token = User.GetToken();
                var rst = _ctx.SP_User_Delete(data);
                return FromDatabase(rst);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UsersController.Delete(data)");
                return Problem(e.Message, e.StackTrace, 500, "Error in Update");
            }
        }

        /// <summary>
        /// Change password by admin
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        [Filters.Auth]
        [Authorize]
        public IActionResult SetPassword([FromBody] Models.Database.StoredProcedures.SP_User_SetPassword.Inputs data)
        {
            try
            {
                data.Token = User.GetToken();
                var rst = _ctx.SP_User_SetPassword(data);
                return FromDatabase(rst);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UsersController.SetPassword(data)");
                return Problem(e.Message, e.StackTrace, 500, "Error in SetPassword");
            }
        }

        /// <summary>
        /// Change password by own user
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        [Filters.Auth]
        [Authorize]
        public IActionResult ChangePassword([FromBody] Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs data)
        {
            try
            {
                data.Token = User.GetToken();
                var rst = _ctx.SP_User_ChangePassword(data);
                return FromDatabase(rst);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UsersController.SP_User_ChangePassword(data)");
                return Problem(e.Message, e.StackTrace, 500, "Error in SP_User_ChangePassword");
            }
        }

        /// <summary>
        /// Update profile user info by itself
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        [Filters.Auth]
        [Authorize]
        public IActionResult UpdateProfile([FromBody] Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs data)
        {
            try
            {
                data.Token = User.GetToken();
                var rst = _ctx.SP_User_UpdateProfile(data);
                return FromDatabase(rst);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UsersController.UpdateProfile(data)");
                return Problem(e.Message, e.StackTrace, 500, "Error in UpdateProfile");
            }
        }
    }
}