using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FTSS.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FTSS.Logic.Log;
using Microsoft.AspNetCore.Http;

namespace FTSS.API
{
    public class Startup
    {
        #region properties
        private const string cns_Zirab_MisExtract_Name = "cns_Zirab_MisExtract";
        private const string cns_Zirab_Fapubs_Name = "cns_Zirab_Fapubs";
        private string cns_Zirab_MisExtract
        {
            get
            {
                var rst = Configuration.GetConnectionString(cns_Zirab_MisExtract_Name);
                return (rst);
            }
        }
        private string cns_Zirab_Fapubs
        {
            get
            {
                var rst = Configuration.GetConnectionString(cns_Zirab_Fapubs_Name);
                return (rst);
            }
        }

        public IConfiguration Configuration { get; }
        #endregion properties

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add logger service
            services.AddSingleton<Logic.Log.ILog, Logic.Log.Log>();

            //Add Mapper
            var mapper = services.AddMapper();

            //Add Authuntication service based on JWT
            services.AddJWT(Configuration);

            services.AddSingleton<IConfiguration>(Configuration);
            //تنظیمات مجوز اجرا از روی دامنه های مختلف
            services.ConfigureCors();
            //Add ORM to service pool
            var ctx_Zirab_MisExtract = services.AddORM_MisExtract(cns_Zirab_MisExtract);
            var ctx_Zirab_Fapubs = services.AddORM_Fapubs(cns_Zirab_Fapubs);

            //Set swagger settings
            services.setSwaggerSettings();

            //Add API logger to service pool
            services.AddAPILogger_MisExtract(ctx_Zirab_MisExtract, mapper);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Logic.Log.ILog logger)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Use my custom global error handling as a middleware
            app.UseMiddleware<Middlewares.ExceptionMiddleware>();

            //Swagger settings
            app.UseOpenApi();
            app.UseSwaggerUi3();

            //به برنامه میگوییم که از چه قوانینی پیروی کند
            app.UseCors(Services.corsPolicyName);
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
