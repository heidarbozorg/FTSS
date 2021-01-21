using Microsoft.Data.SqlClient;
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
        public static void AddDBCTX(this IServiceCollection services, string connectionString)
        {
            //Create a storedProcedure instance for saving log on database
            var ctx = new Logic.Database.Ctx(connectionString);

            //Add dbLogger as a service to the service pool
            services.AddSingleton<Logic.Database.IDBCTX>(ctx);
        }


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
            //Create a storedProcedure instance for saving log on database
            var storedProcedure = new DP.DapperORM.StoredProcedure.SP_Log_Insert(connectionString);

            //Pass the storedProcedure to the dbLogger constructor
            var dbLogger = new Logic.Log.DB(storedProcedure);

            //Add dbLogger as a service to the service pool
            services.AddSingleton<Logic.Log.ILog>(dbLogger);
        }
    }
}
