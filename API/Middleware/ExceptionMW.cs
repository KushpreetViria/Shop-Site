using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{

    /*
        A HTTP middleware that catches exceptions and creates an appropriate 
        HTTP response (rather then always respond with stack trace).
    */

    public class ExceptionMW
    {
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMW> _logger;
		private readonly IHostEnvironment _env;

		public ExceptionMW(RequestDelegate next, 
        ILogger<ExceptionMW> logger,
        IHostEnvironment env)
        {
            this._next = next;
			this._logger = logger;
			this._env = env;
		}

        public async Task InvokeAsync(HttpContext context){
            try{                                                                     //let the HTTP request pass, but catch an exception if it occurs.
                await _next(context);

            }catch(Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException e){   //attempt to accsss same database resource by 2 different threads.

                _logger.LogError(e, e.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                var res =  new APIException(context.Response.StatusCode,"This resource does not exist.",
                    "An attempt was made to alter the same resouce 2 or more times concurrently.");

                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                var json = JsonSerializer.Serialize(res,options);

                await context.Response.WriteAsync(json);

            }catch(Exception e){                                                           //something in the code broke, return a 500 error response.

                _logger.LogError(e, e.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                var response = _env.IsDevelopment() 
                ? new APIException(context.Response.StatusCode, e.Message, e.StackTrace.ToString())
                : new APIException(context.Response.StatusCode, "Internal Server Error");

                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                var json = JsonSerializer.Serialize(response,options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}