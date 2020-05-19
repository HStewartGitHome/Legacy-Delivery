using DeliverySupport.Data;
using DeliverySupport.DataAccess;
using DeliverySupport.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using XMLSupport.Services;

namespace OrderRouterService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .AddJsonFile("orderrouterservice.json")
               .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            Log.Information("Application started");

            try
            {
                var host = CreateHostBuilder(args).Build();

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConfiguration(context.Configuration.GetSection("logging"));
                    logging.AddDebug();
                    logging.AddConsole();
                    logging.AddSerilog();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                    configurationBuilder.AddJsonFile("orderrouterservice.json", false, true);
                    IConfiguration configuration = configurationBuilder.Build();
                    services.AddSingleton<IConfiguration>(configuration);
                    services.AddScoped<ISqlDataAccess, SqlDataAccess>();
                    services.AddScoped<IItemDataService, ItemSqlDataService>();
                    services.AddScoped<IOrderDataService, OrderSqlDataService>();
                    services.AddScoped<IMessageDataService, MessageSqlDataService>();
                    services.AddTransient<IProcess, Process>();
                    services.AddTransient<XMLClient>();
                    services.AddHostedService<Worker>();


                    ServiceProvider serviceProvider = services.BuildServiceProvider();
                    MyServiceFactory.SetServiceProvider(serviceProvider);
                });


        }
    }
}
