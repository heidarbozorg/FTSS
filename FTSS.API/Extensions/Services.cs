using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.API.Extensions
{
    public static class Services
    {
        /// <summary>
        /// Add DatabaseContext as a service to the service pool
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <remarks>
        /// With this service we could easily access to the database connection string
        /// </remarks>
        public static void AddDatabaseContext(this IServiceCollection services, string connectionString)
        {
            //Create a storedProcedure instance for saving log on database
            var ctx = new Logic.Database.DatabaseContext(connectionString);

            //Add DatabaseContext as a service to the service pool
            services.AddSingleton<Logic.Database.IDatabaseContext>(ctx);
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


        /// <summary>
        /// Setup JWT validator
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddJWT(this IServiceCollection services, IConfiguration configuration)
        {
            string key = configuration.GetValue<string>("JWT:Key");
            string issuer = configuration.GetValue<string>("JWT:Issuer");

            //When a request receive, this operations check the JWT and set User object
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
