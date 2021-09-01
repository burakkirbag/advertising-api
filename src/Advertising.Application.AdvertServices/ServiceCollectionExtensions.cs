using Advertising.Adverts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Advertising
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAdvertisingAdvertServices(this IServiceCollection collection)
        {
            collection.AddTransient<IAdvertVisitService, AdvertVisitService>();
            return collection;
        }
    }
}
