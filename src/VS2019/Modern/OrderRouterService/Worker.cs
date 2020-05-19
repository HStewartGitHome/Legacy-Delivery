using DeliverySupport.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace OrderRouterService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IProcess _process;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            ServiceProvider serviceProvider = MyServiceFactory.GetServiceProvider();
            _process = serviceProvider.GetService<IProcess>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                bool nextFileProcess = await _process.ProcessNextFile();
                if (nextFileProcess)
                    _logger.LogInformation("Next File is processed");
                else
                    _logger.LogInformation("Next File is processed");


                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
