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

        /// <summary>
        /// Get default ORM
        /// </summary>
        /// <returns></returns>
        private Logic.Database.IDatabaseContext GetORM()
        {
            //Create Dapper ORM
            var ctx = new Logic.Database.DatabaseContextDapper(cns);
            
            return ctx;
        }

        /// <summary>
        /// Get default logger
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private Logic.Log.ILog GeLogger(Logic.Database.IDatabaseContext ctx)
        {
            var dbLogger = new Logic.Log.DB(ctx);
            return dbLogger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJWT(Configuration);
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddControllers();

            //Get default ORM
            var ctx = GetORM();

            //Get default logger
            var logger = GeLogger(ctx);

            //Add ORM to service pool
            services.AddDatabaseContext(ctx);

            //Add logger to service pool
            services.AddDBLogger(logger);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
