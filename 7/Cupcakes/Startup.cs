using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cupcakes.Data;
using Microsoft.EntityFrameworkCore;
using Cupcakes.Repositories;

namespace Cupcakes
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICupcakeRepository, CupcakeRepository>();

            services.AddDbContext<CupcakeContext>(
                o => o.UseSqlite(_configuration.GetConnectionString("DefaultConnection")), 
                ServiceLifetime.Transient);

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "CupcakeRoute",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Cupcake", action = "Index" },
                    constraints: new { id = "[0-9]+" });
            });
        }
    }
}
