using Infrastructure.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Middlewares
{
    public class GlobalExceptionHandler : IMiddleware
    {
        private readonly ILogger _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate _next)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);

            }
        }
        private async Task HandleExceptionAsync(HttpContext context, System.Exception exception) {
           context.Response.ContentType = "application/json";
            var response = exception switch
            {

                NotFoundException => new ProblemDetails()
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Type = "Server Error",
                    Title = "Not Found",
                    Detail = exception.Message

                },

                ValidationException validationException => new ProblemDetails()
                {
                       Status= (int)HttpStatusCode.BadRequest,
                       Type = "Server Error",
                       Title = "Validation Error",
                       Detail = validationException.Message
                }
            };

            context.Response.StatusCode = response.Status.Value;
            string json = JsonSerializer.Serialize(response);  
            await context.Response.WriteAsync(json);



        }
    }


        
    
}
