using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FTSS.API.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class UsersController : BaseController
    {
        public UsersController(Logic.Database.IDBCTX dbCTX, Logic.Log.ILog logger) 
            : base(dbCTX, logger)
        {
        }

        /// <summary>
        /// Login and get database token
        /// </summary>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login([FromBody] Models.Database.StoredProcedures.SP_Login_Params filterParams)
        {
            try
            {
                var rst = Logic.Security.UserInfo.Login(_ctx, filterParams);
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

    }
}