using Advertising.Api.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Advertising.Api
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureApplication(this IApplicationBuilder application)
        {
            application.UseMiddleware<ExceptionMiddleware>();
            application.UseStaticFiles();
            application.UseRouting();
            application.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            application.UseMiddleware<CorrelationMiddleware>();
            application.UseAppSwagger();
        }

        private static void UseAppSwagger(this IApplicationBuilder application)
        {
            application.UseSwagger();
            application.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Advertising.Api v1"));
        }
    }
}
