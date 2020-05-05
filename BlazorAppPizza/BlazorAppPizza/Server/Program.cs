using BlazorAppPizza.Server.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace BlazorAppPizza.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var host = CreateHostBuilder(args).Build();

            //// Initialize the database
            //var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
            //using (var scope = scopeFactory.CreateScope())
            //{
            //    var context = scope.ServiceProvider.GetRequiredService<PizzaStoreContex>();
            //    if (context.Database.EnsureCreated())
            //    {
            //        SeedData.Initialize(context);
            //    }
            //}

            //host.Run();

            var Host = BuildWebHost(args);
            var ScopeFactory =
    Host.Services.GetRequiredService<IServiceScopeFactory>();
            using (var Scope = ScopeFactory.CreateScope())
            {
                var Context = Scope.ServiceProvider
                    .GetRequiredService<PizzaStoreContex>();
                if (Context.Specials.Count() == 0)
                {
                    SeedData.Initialize(Context);
                }
            }
            Host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
           .UseConfiguration(new ConfigurationBuilder()
               .AddCommandLine(args)
               .Build())
           .UseStartup<Startup>()
           .Build();

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
