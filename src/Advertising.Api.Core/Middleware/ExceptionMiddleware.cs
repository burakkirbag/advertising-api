using Advertising.Api.Extensions;
using Advertising.Api.Mvc.Models;
using Advertising.Domain;
using Advertising.Domain.Rules;
using Advertising.Extensions;
using Advertising.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Advertising.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                LogHelper.AddError(httpContext.GetCorrelationId(), ex);

                var statusCode = GetStatusCode(ex);

                var data = new ApiReturn<object>()
                {
                    Code = (int)statusCode,
                    Success = false,
                    Message = ex.Message,
                    InternalMessage = statusCode == HttpStatusCode.InternalServerError ? ex.StackTrace : string.Empty
                };

                httpContext.Response.StatusCode = (int)statusCode;
                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsync(data.ToJson());
            }
        }

        private HttpStatusCode GetStatusCode(Exception ex)
        {
            if (ex is BusinessRuleValidationException) return HttpStatusCode.BadRequest;
            else if (ex is Application.ApplicationException) return HttpStatusCode.NotAcceptable;
            else if (ex is DomainException) return HttpStatusCode.Conflict;
            else if (ex is AdvertisingException) return HttpStatusCode.InternalServerError;
            else return HttpStatusCode.InternalServerError;
        }
    }
}