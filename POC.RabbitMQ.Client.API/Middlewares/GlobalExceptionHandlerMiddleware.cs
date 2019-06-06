using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace POC.RabbitMQ.Client.API.Middlewares
{
    public static class GlobalExceptionHandlerMiddleware
    {
        public static void Handle(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>();

                if (exception != null)
                {
                    var errorResult = new GlobalExceptionResult("There was an error processing your request. Please try again.");
                    errorResult.SetDevelopmentDetails(exception.Error.Message, exception.Error.StackTrace, exception.Error.ToString());

                    string responseText = JToken
                        .FromObject(errorResult)
                        .ToString();

                    context.Response.ContentType = "application/json";

                    await context.Response
                        .WriteAsync(responseText)
                        .ConfigureAwait(false);
                }
            });
        }
    }
}
