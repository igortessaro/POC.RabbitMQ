﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POC.RabbitMQ.Client.API.Middlewares;
using POC.RabbitMQ.Domain.Config;
using POC.RabbitMQ.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;

namespace POC.RabbitMQ.Client.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //RabbitMQConfig rabbitMQConfig = new RabbitMQConfig();
            CustomerRabbitMQConfig customerRabbitMQConfig = new CustomerRabbitMQConfig();
            //this.Configuration.Bind("RabbitMQ", rabbitMQConfig);
            this.Configuration.Bind("RabbitMQ:CustomerRabbitMQ", customerRabbitMQConfig);
            //services.AddSingleton(rabbitMQConfig);
            services.AddSingleton(customerRabbitMQConfig);

            ServiceCollectionBootstrapper.ConfigureServices(services);

            services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "POC RabbitMQ API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler(configure => GlobalExceptionHandlerMiddleware.Handle(configure, env));

            app.UseCors(policy => policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            app.UseMvcWithDefaultRoute();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "POC.RabbitMQ.Client.API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
