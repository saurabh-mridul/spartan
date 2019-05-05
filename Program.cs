using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Spartan.Persistence.Contexts;

namespace Spartan
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            // required to seed the data for in memory case.
            using (var scope = host.Services.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    context.Database.EnsureCreated();
                }
            }

            host.Run();

        }

        public static IWebHost BuildWebHost(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
           .UseStartup<Startup>()
           .Build();

    }
}
