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

namespace FTSS.API
{
    public class Startup
    {
        #region properties
        private const string ConnectionStringName = "cns";
        private string cns
        {
            get
            {
                var rst = Configuration.GetConnectionString(ConnectionStringName);
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
            services.AddSingleton<Logic.Log.ILog, Logic.Log.LogNLog>();

            //Add Mapper
            var mapper = services.AddMapper();

            //Add Authuntication service based on JWT
            services.AddJWT(Configuration);

            services.AddSingleton<IConfiguration>(Configuration);

            //Add ORM to service pool
            var ctx = services.AddORM(cns);

            //Add logger to service pool
            services.AddLogger(ctx);
            
            //Add API logger to service pool
            services.AddAPILogger(ctx, mapper);

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

            //app.ConfigureExceptionHandler(logger);
            //app.UseExceptionHandler(o => o.(logger));

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
