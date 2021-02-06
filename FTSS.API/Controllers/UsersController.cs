using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FTSS.API.Controllers
{
    [Route("/api/[controller]/[action]")]
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


        public UsersController(Logic.Database.IDatabaseContext dbCTX, Logic.Log.ILog logger, IConfiguration configuration) 
            : base(dbCTX, logger)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Login and get database token
        /// </summary>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login([FromBody] Models.Database.StoredProcedures.SP_Login.Inputs filterParams)
        {
            try
            {
                var rst = Logic.Database.StoredProcedure.SP_Login.Call(_ctx, filterParams);
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
        [Filters.Auth]
        public IActionResult GetAll([FromBody] Models.Database.StoredProcedures.SP_Users_GetAll_Params filterParams)
        {
            try
            {
                var u = User;
                filterParams.Token = HttpContext.Request.Headers["Token"];
                var dbResult = Logic.Database.StoredProcedure.SP_Users_GetAll.Call(_ctx, filterParams);
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
        public IActionResult Insert([FromBody] Models.Database.Tables.Users data)
        {
            try
            {
                data.Token = HttpContext.Request.Headers["Token"];
                var rst = Logic.Database.StoredProcedure.SP_User_Insert.Call(_ctx, data);
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
        public IActionResult Update([FromBody] Models.Database.Tables.Users data)
        {
            try
            {
                data.Token = HttpContext.Request.Headers["Token"];
                var rst = Logic.Database.StoredProcedure.SP_User_Update.Call(_ctx, data);
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
        public IActionResult Delete([FromBody] Models.Database.Tables.Users data)
        {
            try
            {
                data.Token = HttpContext.Request.Headers["Token"];
                var rst = Logic.Database.StoredProcedure.SP_User_Delete.Call(_ctx, data);
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
        public IActionResult SetPassword([FromBody] Models.Database.Tables.Users data)
        {
            try
            {
                data.Token = HttpContext.Request.Headers["Token"];
                var rst = Logic.Database.StoredProcedure.SP_User_SetPassword.Call(_ctx, data);
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
        public IActionResult ChangePassword([FromBody] Models.Database.StoredProcedures.SP_User_ChangePassword data)
        {
            try
            {
                data.Token = HttpContext.Request.Headers["Token"];
                var rst = Logic.Database.StoredProcedure.SP_User_ChangePassword.Call(_ctx, data);
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
        public IActionResult UpdateProfile([FromBody] Models.Database.Tables.Users data)
        {
            try
            {
                data.Token = HttpContext.Request.Headers["Token"];
                var rst = Logic.Database.StoredProcedure.SP_User_UpdateProfile.Call(_ctx, data);
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