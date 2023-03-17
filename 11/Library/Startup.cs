using System.Security.Claims;
using Library.Data;
using Library.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Library.Models;
using Microsoft.AspNetCore.Identity;

namespace Library
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
            services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequiredLength = 7;
                o.Password.RequireUppercase = true;
                o.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<LibraryContext>();

            services.AddDbContext<LibraryContext>(options =>
                 options.UseSqlite("Data Source=library.db"));

            services.AddMvc();

            services.AddAuthorization(o =>
            {
                o.AddPolicy("RequireEmail", p =>
                {
                    p.RequireClaim(ClaimTypes.Email);
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, LibraryContext libraryContext)
        {
            libraryContext.Database.EnsureDeleted();
            libraryContext.Database.EnsureCreated();

            app.UseStaticFiles();

            app.UseAuthentication();
            

            app.UseNodeModules(env.ContentRootPath);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "LibraryRoute",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Library", action = "Index" },
                    constraints: new { id = "[0-9]+" });
            });
        }
    }
}
