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
        public readonly Logic.Database.IDatabaseContext_MisExtract _ctx_MisExtract;
        public readonly Logic.Database.IDatabaseContextDapper_Fapubs _ctx_Fapubs;

        public BaseController(Logic.Database.IDatabaseContext_MisExtract dbCTX_MisExtract)
        {
            _ctx_MisExtract = dbCTX_MisExtract;
        }
        public BaseController(Logic.Database.IDatabaseContextDapper_Fapubs dbCTX_Fapubs)
        {
            _ctx_Fapubs = dbCTX_Fapubs;
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

            if (dbResult.StatusCode >= 200 && dbResult.StatusCode < 300)
                return Ok(dbResult);

            return StatusCode(dbResult.StatusCode, dbResult.ErrorMessage ?? "Unhandled error");
        }
    }
}
