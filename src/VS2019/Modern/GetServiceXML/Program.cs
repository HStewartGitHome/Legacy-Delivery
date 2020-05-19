using DeliverySupport.Data;
using DeliverySupport.DataAccess;
using DeliverySupport.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using XMLSupport.Services;

namespace GetServiceXML
{
    class Program
    {
        static void Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("getservicexml.json")
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
                    GetServiceXMLApplication app = serviceProvider.GetService<GetServiceXMLApplication>();

                    // Start up logic here
                    app.Run(args);
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
            configurationBuilder.AddJsonFile("getservicexml.json", false, true);
            IConfiguration configuration = configurationBuilder.Build();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddScoped<ISqlDataAccess, SqlDataAccess>();
            services.AddScoped<IItemDataService, ItemSqlDataService>();
            services.AddScoped<IOrderDataService, OrderSqlDataService>();
            services.AddScoped<IMessageDataService, MessageSqlDataService>();
            services.AddLogging(configure => configure.AddSerilog());
            services.AddTransient<GetServiceXMLApplication>();
            services.AddTransient<Process>();
            services.AddTransient<XMLClient>();

        }
    }

    public class GetServiceXMLApplication
    {
        private readonly ILogger<GetServiceXMLApplication> _logger;


        public GetServiceXMLApplication(ILogger<GetServiceXMLApplication> logger)
        {
            _logger = logger;
        }
        internal void Run(string[] args)
        {

            _logger.LogInformation("Application Started at {dateTime}", DateTime.UtcNow);

            if (args.Length < 2)
                _logger.LogInformation("GetServiceXML  TYPE FilePath");
            else
            {
                ServiceProvider serviceProvider = MyServiceFactory.GetServiceProvider();
                Process process = serviceProvider.GetService<Process>();
                string str = args[0].ToUpper();
                string type = Right(str, str.Length - 1).Trim();
                str = args[1].Trim();
                string fileName = Right(str, str.Length - 1).Trim();

                _logger.LogInformation("TYPE=" + type + " FilePath = " + fileName);

                if (process.ProcessXML(type, fileName) == true)
                    _logger.LogInformation("Successfully execute TYPE of " + type + " for file [" + fileName + "]");
                else
                    _logger.LogWarning("Fail to execute TYPE of " + type + " for file [" + fileName + "]");
            }


            _logger.LogInformation("Application Ended at {dateTime}", DateTime.UtcNow);
        }

        public string Right(string original, int numberCharacters)
        {
            return original.Substring(numberCharacters > original.Length ? 0 : original.Length - numberCharacters);
        }
    }
}
