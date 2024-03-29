﻿using System.Net;

namespace Lesson1API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, 
            RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;   
        }

        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;  

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                // Log this exception
                logger.LogError(ex, $"{errorId} : {ex.Message}");

                // Return A custom error respone
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong! We are looking into resolving this."
                };
               
                await httpContext.Response.WriteAsJsonAsync( error );
            }
        }
    }
}
