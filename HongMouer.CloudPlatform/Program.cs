using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HongMouer.CloudPlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostingEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddJsonFile("appsettings.json", optional: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
