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
    [Authorize]
    [Filters.AuthAttribute]
    [ApiController]
    public class UsersController : BaseController
    {

        public UsersController(Logic.Database.IDatabaseContext dbCTX)
            : base(dbCTX)
        {
        }


        /// <summary>
        /// Search between all users by filter parameters
        /// </summary>
        /// <param name="data">
        /// Filter parameters
        /// </param>
        /// <returns></returns>
        [HttpGet]        
        public IActionResult GetAll([FromBody] Models.Database.StoredProcedures.SP_Users_GetAll.Inputs filterParams)
        {
            filterParams.Token = User.GetToken();

            var dbResult = _ctx.SP_Users_GetAll(filterParams);
            return FromDatabase(dbResult);
        }

        /// <summary>
        /// Add new user to database
        /// </summary>
        /// <param name="data">
        /// User info
        /// </param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Insert([FromBody] Models.Database.StoredProcedures.SP_User_Insert.Inputs data)
        {
            data.Token = User.GetToken();
            var rst = _ctx.SP_User_Insert(data);
            return FromDatabase(rst);
        }

        /// <summary>
        /// Update a user info by admin
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update([FromBody] Models.Database.StoredProcedures.SP_User_Update.Inputs data)
        {
            data.Token = User.GetToken();
            var rst = _ctx.SP_User_Update(data);
            return FromDatabase(rst);
        }

        /// <summary>
        /// Delete a user by admin
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete([FromBody] Models.Database.StoredProcedures.SP_User_Delete.Inputs data)
        {
            data.Token = User.GetToken();
            var rst = _ctx.SP_User_Delete(data);
            return FromDatabase(rst);
        }

        /// <summary>
        /// Change password by admin
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult SetPassword([FromBody] Models.Database.StoredProcedures.SP_User_SetPassword.Inputs data)
        {
            data.Token = User.GetToken();
            var rst = _ctx.SP_User_SetPassword(data);
            return FromDatabase(rst);
        }

        /// <summary>
        /// Change password by own user
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult ChangePassword([FromBody] Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs data)
        {
            data.Token = User.GetToken();
            var rst = _ctx.SP_User_ChangePassword(data);
            return FromDatabase(rst);
        }

        /// <summary>
        /// Update profile user info by itself
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateProfile([FromBody] Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs data)
        {
            data.Token = User.GetToken();
            var rst = _ctx.SP_User_UpdateProfile(data);
            return FromDatabase(rst);
        }
    }
}