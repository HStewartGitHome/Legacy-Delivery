using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace Delivery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string settingsFile = GetSettingsFile();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(settingsFile)
                .Build();

            string strValue = configuration["AppSettings:UseSerilog"];
            if (string.IsNullOrEmpty(strValue) == true)
                strValue = "0";
            if (strValue == "1")
                Startup.UseSerilog = true;
            else
                Startup.UseSerilog = false;

            if (Startup.UseSerilog == true)
            {
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                string configType = configuration["AppSettings:ConfigType"];
                Log.Information("Application started with SettingsFile= {settingsFile} and ConfigValue={configType} and MachineName={machinename}", settingsFile, configType, Environment.MachineName);
            }


            try
            {
                strValue = configuration["AppSettings:UseSimData"];
                if (string.IsNullOrEmpty(strValue) == true)
                    strValue = "0";
                if (strValue == "1")
                {
                    Startup.UseSimData = true;
                    Startup.UseAuth = false;
                }
                else
                    Startup.UseSimData = false;

                if (Startup.UseSerilog == true)
                    Log.Information("UseSimData = {usesimdata} strValue = {strdata} UseAuth = {UseAuth} ", Startup.UseSimData, strValue, Startup.UseAuth);


                if (Startup.UseSimData == true)
                    Log.Information("Using Simulated in-memory Database");
                else
                    Log.Information("Using SQL Database");


                if ((Startup.UseSerilog == true) && (Startup.UseSimData == false))
                {
                    string settingConnect = "ConnectionStrings:SQLDB";
                    string settingValue = configuration[settingConnect];
                    Log.Information("{setting} = [{settingValue}]", settingConnect, settingValue);
                    if (Startup.UseAuth == true)
                    {
                        settingConnect = "ConnectionStrings:DefaultConnection";
                        settingValue = configuration[settingConnect];
                        Log.Information("{setting} = [{settingValue}]", settingConnect, settingValue);
                    }
                }



                var host = CreateHostBuilder(args).Build();
                var logger = host.Services.GetRequiredService<ILogger<Program>>();
                host.Run();
            }
            catch (Exception ex)
            {
                if (Startup.UseSerilog == true)
                {
                    Log.Fatal(ex, "Application failed to start");
                }

            }
            finally
            {
                if (Startup.UseSerilog == true)
                {
                    Log.CloseAndFlush();
                }
            }
        }

        public static string GetSettingsFile()
        {
            string settingsFile = "appsettings.json";

            var EnvVars = Environment.GetEnvironmentVariables();
            var KeyValuePair = EnvVars["ASPNETCORE_ENVIRONMENT"];


            string appEnviroment = "";
            if (KeyValuePair != null)
                appEnviroment = KeyValuePair.ToString();

            if (appEnviroment == "Development")
                settingsFile = "appsettings.Development.json";
            else
            {
                //  Startup.UseAuth = false;
                //  Startup.UseSerilog = false;
            }


            Console.WriteLine("appEnviroment=" + appEnviroment);
            Console.WriteLine("settingsFile=" + settingsFile);



            return settingsFile;
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
                        if (Startup.UseSerilog == true)
                        {
                            logging.AddSerilog();
                        }
                    })
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
        }
    }
}
