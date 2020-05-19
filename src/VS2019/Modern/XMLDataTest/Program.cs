using DeliverySupport.Data;
using DeliverySupport.DataAccess;
using DeliverySupport.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using XMLSupport.Services;

namespace XMLDataTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string settingsFile = "xmldatatest.json";
            var configuration = new ConfigurationBuilder()
                  .AddJsonFile(settingsFile)
                  .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            Log.Information("Application started");

            try
            {

                var services = new ServiceCollection();
                ConfigureServices(services);
                using (ServiceProvider serviceProvider = services.BuildServiceProvider())
                {
                    MyServiceFactory.SetServiceProvider(serviceProvider);
                    XMLDataTestApplication app = serviceProvider.GetService<XMLDataTestApplication>();

                    // Start up logic here
                    app.Run();
                }
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


        private static void ConfigureServices(ServiceCollection services)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("xmldatatest.json", false, true);
            IConfiguration configuration = configurationBuilder.Build();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddScoped<ISqlDataAccess, SqlDataAccess>();
            services.AddScoped<IItemDataService, ItemSqlDataService>();
            services.AddScoped<IOrderDataService, OrderSqlDataService>();
            services.AddScoped<IMessageDataService, MessageSqlDataService>();
            services.AddLogging(configure => configure.AddSerilog());
            //         services.AddLogging(configure => configure.AddConsole())
            services.AddTransient<XMLDataTestApplication>();
            services.AddTransient<XMLClient>();
            services.AddTransient<Process>();
        }
    }

    public class XMLDataTestApplication
    {
        private readonly ILogger<XMLDataTestApplication> _logger;

        public XMLDataTestApplication(ILogger<XMLDataTestApplication> logger)
        {
            _logger = logger;

        }
        internal void Run()
        {
            _logger.LogInformation("Application Started at {dateTime}", DateTime.UtcNow);

            ServiceProvider serviceProvider = MyServiceFactory.GetServiceProvider();
            Process process = serviceProvider.GetService<Process>();
            process.ProcessAllTests();

            _logger.LogInformation("Application Ended at {dateTime}", DateTime.UtcNow);
        }
    }
}
