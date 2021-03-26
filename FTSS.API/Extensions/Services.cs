using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSwag.Generation.Processors.Security;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.API.Extensions
{
    public static class Services
    {
        /// <summary>
        /// Add default mapper to the service pool
        /// </summary>
        /// <param name="services"></param>
        public static AutoMapper.IMapper AddMapper(this IServiceCollection services)
        {
            //Create default mapper
            var mapConfig = new AutoMapper.MapperConfiguration(mc =>
            {
                mc.AddProfile(new Logic.CommonOperations.Mapper());
            });

            AutoMapper.IMapper mapper = mapConfig.CreateMapper();
            services.AddSingleton(mapper);
            return mapper;
        }


        /// <summary>
        /// Add DatabaseContext as a service to the service pool
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString">Database connection string</param>
        /// <remarks>
        /// With this service we could easily access to the database objects like Stored-Procedure
        /// </remarks>
        public static Logic.Database.IDatabaseContext AddORM(this IServiceCollection services, string connectionString)
        {
            //Create Dapper ORM
            var ctx = new Logic.Database.DatabaseContextDapper(connectionString);

            //Add DatabaseContext as a service to the service pool
            services.AddSingleton<Logic.Database.IDatabaseContext>(ctx);
            
            return ctx;
        }


        /// <summary>
        /// Add Logger service to services pool
        /// </summary>
        /// <param name="services"></param>
        /// <param name="defaultORM">The default ORM</param>
        public static void AddLogger(this IServiceCollection services, Logic.Database.IDatabaseContext defaultORM)
        {
            //Create database logger
            var dbLogger = new Logic.Log.LogAtDatabase(defaultORM);

            //Add logger as a service to the service pool
            services.AddSingleton<Logic.Log.ILog>(dbLogger);
        }

        /// <summary>
        /// Add API logger service to service pool
        /// </summary>
        /// <param name="services"></param>
        /// <param name="defaultORM"></param>
        public static void AddAPILogger(this IServiceCollection services, 
                    Logic.Database.IDatabaseContext defaultORM,
                    AutoMapper.IMapper defaultMapper)
        {
            var APILoggerDatabase = new Logic.Log.APILoggerDatabase(defaultORM, defaultMapper);

            //Add logger as a service to the service pool
            services.AddSingleton<Logic.Log.IAPILogger>(APILoggerDatabase);
        }

        #region JWT
        /// <summary>
        /// Fetch key and issuer fom setting file and generate token validation
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static TokenValidationParameters getTokenValidationParameters(IConfiguration configuration)
        {
            //Fetch JWT key and issuer from the appsettings.json
            string key = configuration.GetValue<string>("JWT:Key");
            string issuer = configuration.GetValue<string>("JWT:Issuer");
            
            var rst = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
            
            return rst;
        }

        /// <summary>
        /// Get authentication event handler by jwt
        /// </summary>
        /// <returns></returns>
        private static JwtBearerEvents getJWTEvents()
        {
            var rst = new JwtBearerEvents
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
            
            return rst;
        }

        /// <summary>
        /// Setup JWT validator
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddJWT(this IServiceCollection services, IConfiguration configuration)
        {
            //When a request receive, this operations check the JWT and set User object
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = getTokenValidationParameters(configuration);
                    options.Events = getJWTEvents();
                });
        }
        #endregion JWT

        /// <summary>
        /// Set swagger setttings
        /// </summary>
        /// <param name="services"></param>
        public static void setSwaggerSettings(this IServiceCollection services)
        {
            services.AddSwaggerDocument(c =>
            {
                c.DocumentName = "FTSS, 1.1.5";
                c.Title = "FTSS API Document";
                c.Description = "FTSS API {GET,Post,Put,Delete}";
                c.OperationProcessors.Add(new OperationSecurityScopeProcessor("Bearer"));
                c.GenerateExamples = true;
                c.GenerateCustomNullableProperties = true;
                c.AllowNullableBodyParameters = true;
                c.AddSecurity("Bearer", new NSwag.OpenApiSecurityScheme()
                {
                    Description = "Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                    Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
                    BearerFormat = "Authorization: Bearer {token}"
                });
            });
        }
    }
}
