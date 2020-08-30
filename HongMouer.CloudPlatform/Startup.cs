using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using HongMouer.CloudPlatform.Controllers;
using HongMouer.Common;
using HongMouer.Common.Utility;
using HongMouer.EHR.Models;
using HongMouer.EntityRelationalCore;
using HongMouer.EntityRelationalCore.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HongMouer.CloudPlatform
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();

            //DatabaseType.Oracle
            services.AddTransient<IRepository>(options => new OracleRepository("HongMouerConnection"));

            services.AddDenpendencyServices();
            services.AddHealthChecks().AddCheck<HealthCheck>("DbCheck"); 

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                     .AddCookie(options =>
                     {
                         options.Cookie.Name = "Token";
                         options.Cookie.HttpOnly = true;
                         options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                         options.LoginPath = "/Login/Login";
                         options.LogoutPath = "/Login/Logout";
                         options.SlidingExpiration = true;
                         //options.DataProtectionProvider = DataProtectionProvider.Create(new DirectoryInfo(@"D:\sso\key"));
                         options.TicketDataFormat = new TicketDataFormat(new  AesDataProtectorMiddleware());
                         //TokenController.CookieName = options.Cookie.Name;
                     });

            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateTimeFormatMiddleware());
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSystemMonitor(app.ApplicationServices.GetService<IRepository>());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
