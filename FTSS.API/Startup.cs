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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJWT(Configuration);
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddControllers();
            services.AddDatabaseContext(cns);
            services.AddDBLogger(cns);
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
