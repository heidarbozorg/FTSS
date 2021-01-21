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
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login([FromBody] Models.Database.Tables.Users data)
        {
            try
            {
                //var rst = Logic.Database.StoredProcedure.SP_Login.Call(_ctx, data);
                var rst = Logic.Security.UserInfo.Login(_ctx, data);
                return FromDatabase(rst);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UsersController.Login(data)");
                return Problem(e.Message, e.StackTrace, 500, "Error in Login");
            }
        }

        [HttpPost]
        public IActionResult GetUser([FromBody] Models.Database.Interfaces.DBResult data)
        {
            try
            {
                //var rst = Logic.Database.StoredProcedure.SP_Login.Call(_ctx, data);
                var JWT = new Logic.Security.JWT();
                string token = data.Data.ToString();
                var rst = JWT.GetData(token);
                var dbResult = new Models.Database.Interfaces.DBResult(200, "", rst);
                return FromDatabase(dbResult);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UsersController.GetUser(data)");
                return Problem(e.Message, e.StackTrace, 500, "Error in Login");
            }
        }

    }
}
