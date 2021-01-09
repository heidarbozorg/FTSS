using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTSS.API.Extensions
{
    public static class Services
    {
        /// <summary>
        /// Add Logger service to services pool
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <remarks>
        /// This logger will save logs into database
        /// </remarks>
        public static void AddDBLogger(this IServiceCollection services, string connectionString)
        {
            var insertLogAtDB = new DP.DapperORM.StoredProcedure.SP_Log_Insert(connectionString);
            var dbLogger = new Logic.Log.DB(insertLogAtDB);
            services.AddSingleton<Logic.Log.ILog>(dbLogger);
        }
    }
}
