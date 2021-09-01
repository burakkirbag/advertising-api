using Advertising.Bus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Advertising
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAdvertisingCore(this IServiceCollection collection)
        {
            var applicationServicesAssemblies = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(x => x.GetName().Name.StartsWith("Advertising.Application.", StringComparison.InvariantCultureIgnoreCase)).ToArray();

            collection.AddAutoMapper(applicationServicesAssemblies);
            collection.AddMediatR(applicationServicesAssemblies);

            collection.AddTransient<IBusService, RabbitMQBusService>();

            return collection;
        }
    }
}
