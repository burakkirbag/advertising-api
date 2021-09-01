using Advertising.Adverts;
using Advertising.Data;
using Advertising.Data.Repositories;
using Advertising.Data.Uow;
using Advertising.Domain;
using Advertising.Domain.Uow;
using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace Advertising
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAdvertisingData(this IServiceCollection collection)
        {
            collection.AddDapper();
            collection.AddRepositories();

            return collection;
        }

        private static void AddDapper(this IServiceCollection collection)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            collection.AddScoped<IDataContext, DbContext>();
            collection.AddTransient<IUnitOfWork, UnitOfWork>();
            collection.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();
        }

        private static void AddRepositories(this IServiceCollection collection)
        {
            collection.AddTransient<IAdvertRepository, AdvertRepository>();
            collection.AddTransient<IAdvertVisitRepository, AdvertVisitRepository>();
        }
    }
}
