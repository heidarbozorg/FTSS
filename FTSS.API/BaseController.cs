using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FTSS.API
{
    public class BaseController : Controller
    {
        public readonly Logic.Database.IDBCTX _ctx;
        public readonly Logic.Log.ILog _logger;

        public BaseController(Logic.Database.IDBCTX dbCTX, Logic.Log.ILog logger)
        {
            _ctx = dbCTX;
            _logger = logger;
        }

        /// <summary>
        /// Convert database result to rest standard result
        /// </summary>
        /// <param name="dbResult"></param>
        /// <returns></returns>
        public IActionResult FromDatabase(Models.Database.DBResult dbResult)
        {
            if (dbResult == null)
                return StatusCode(500, "Unhandled internal server error");

            if (dbResult.Data == null && dbResult.ErrorCode == 200)
                dbResult.ErrorCode = 500;

            if (dbResult.ErrorCode >= 200 && dbResult.ErrorCode < 300)
                return Ok(dbResult);

            return StatusCode(dbResult.ErrorCode, dbResult.ErrorMessage ?? "Unhandled error");
        }
    }
}
