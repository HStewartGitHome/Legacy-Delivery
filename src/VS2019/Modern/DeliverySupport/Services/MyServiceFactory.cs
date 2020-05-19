using Microsoft.Extensions.DependencyInjection;

namespace DeliverySupport.Services
{
    public static class MyServiceFactory
    {
        static ServiceProvider _serviceProvider;


        public static ServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }

        public static void SetServiceProvider(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
