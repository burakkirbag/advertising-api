using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Advertising.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetCorrelationId(this HttpContext httpContext)
        {
            if (httpContext.Request != null && httpContext.Request.Headers.ContainsKey("X-Correlation-Id"))
                return httpContext.Request.Headers["X-Correlation-Id"];

            return Guid.NewGuid().ToString();
        }

        public static string GetClientIP(this HttpContext httpContext)
        {
            var ip = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(ip)) ip = ip.Split(',')[0];

            if (string.IsNullOrWhiteSpace(ip)) ip = Convert.ToString(httpContext.Request.HttpContext.Connection.RemoteIpAddress);

            if (string.IsNullOrWhiteSpace(ip)) ip = httpContext.Request.Headers["REMOTE_ADDR"].FirstOrDefault();

            return ip;
        }
    }
}