using Advertising.Api.Mvc.Filters;
using Advertising.Configuration;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;

namespace Advertising.Api
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddAppConfiguration(configuration);

            collection.AddHttpContextAccessor();

            collection.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            collection.AddMvc(options => { options.Filters.Add(new ValidationFilter()); });

            collection.AddOptions();
            collection.AddRouting();
            collection.AddCors();
            collection.AddHealthChecks();
            collection.AddAppMvc();
            collection.AddAppSwagger();

            collection.AddAdvertisingCore();
            collection.AddAdvertisingData();
            collection.AddAdvertisingAdvertServices();

            return collection;
        }

        private static void AddAppConfiguration(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.BindConfiguration<AppConfig>(configuration.GetSection("App"));
            collection.BindConfiguration<DatabaseConfig>(configuration.GetSection("App").GetSection("Database"));
            collection.BindConfiguration<BusConfig>(configuration.GetSection("App").GetSection("Bus"));
        }

        private static void AddAppMvc(this IServiceCollection collection)
        {
            var mvcBuilder = collection.AddControllers();

            mvcBuilder.AddFluentValidation(configuration =>
            {
                var assemblies = mvcBuilder.PartManager.ApplicationParts
                    .OfType<AssemblyPart>()
                    .Where(part => part.Name.StartsWith("Advertising", StringComparison.InvariantCultureIgnoreCase))
                    .Select(part => part.Assembly);

                configuration.RegisterValidatorsFromAssemblies(assemblies);

                configuration.ImplicitlyValidateChildProperties = true;
            });

            mvcBuilder.AddControllersAsServices();
        }

        private static void AddAppSwagger(this IServiceCollection collection)
        {
            collection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Advertising.Api", Version = "v1" });
            });
        }

        private static TConfig BindConfiguration<TConfig>(this IServiceCollection collection, IConfiguration configuration) where TConfig : class, new()
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var config = new TConfig();

            configuration.Bind(config);

            collection.Configure<TConfig>(configuration);

            return config;
        }
    }
}
