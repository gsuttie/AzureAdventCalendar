using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AzureAdventCalendarWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var settings = config.Build();
                })
         .UseStartup<Startup>();
    }
}
