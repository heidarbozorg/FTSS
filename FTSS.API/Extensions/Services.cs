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
        /// With this service we could easily access to the database objects like Stored-Procedure
        /// </remarks>
        public static void AddDatabaseContext(this IServiceCollection services, Logic.Database.IDatabaseContext ctx)
        {
            //Add DatabaseContext as a service to the service pool
            services.AddSingleton<Logic.Database.IDatabaseContext>(ctx);
        }


        /// <summary>
        /// Add Logger service to services pool
        /// </summary>
        /// <param name="services"></param>
        /// <param name="logger">The default logger</param>
        public static void AddDBLogger(this IServiceCollection services, Logic.Log.ILog logger)
        {
            //Add logger as a service to the service pool
            services.AddSingleton<Logic.Log.ILog>(logger);
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
