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
        public static Logic.Database.IDatabaseContext_MisExtract AddORM_MisExtract(this IServiceCollection services, string connectionString)
        {
            //Create Dapper ORM
            var ctx = new Logic.Database.DatabaseContextDapper_MisExtract(connectionString);

            //Add DatabaseContext as a service to the service pool
            services.AddSingleton<Logic.Database.IDatabaseContext_MisExtract>(ctx);
            
            return ctx;
        }
        public static Logic.Database.IDatabaseContextDapper_Fapubs AddORM_Fapubs(this IServiceCollection services, string connectionString)
        {
            //Create Dapper ORM
            var ctx = new Logic.Database.DatabaseContextDapper_Fapubs(connectionString);

            //Add DatabaseContext as a service to the service pool
            services.AddSingleton<Logic.Database.IDatabaseContextDapper_Fapubs>(ctx);

            return ctx;
        }


        /// <summary>
        /// Add API logger service to service pool
        /// </summary>
        /// <param name="services"></param>
        /// <param name="defaultORM"></param>
        public static void AddAPILogger_MisExtract(this IServiceCollection services, 
                    Logic.Database.IDatabaseContext_MisExtract defaultORM,
                    AutoMapper.IMapper defaultMapper)
        {
            var APILoggerDatabase = new Logic.Log.APILoggerDatabase(defaultORM, defaultMapper);

            //Add logger as a service to the service pool
            services.AddSingleton<Logic.Log.IAPILogger>(APILoggerDatabase);
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
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = Logic.Security.JWT.GetTokenValidationParameters(key, issuer);
                    options.Events = Logic.Security.JWT.GetJWTEvents();
                });
        }


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
