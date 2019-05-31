using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using POC.RabbitMQ.Domain.Services;
using POC.RabbitMQ.Domain.Factories;
using POC.RabbitMQ.Domain.Repositories;

namespace POC.RabbitMQ.Infrastructure
{
    public static class ServiceCollectionBootstrapper
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.Scan(scan =>
            {
                scan.FromApplicationDependencies()
                    .AddClasses(filter => filter.AssignableTo<IRepository>())
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()

                    .AddClasses(filter => filter.AssignableTo<IFactory>())
                        .AsImplementedInterfaces()
                        .AsSelf()
                        .WithScopedLifetime()

                    .AddClasses(filter => filter.AssignableTo<IService>())
                        .AsImplementedInterfaces()
                        .AsSelf()
                        .WithScopedLifetime();
            });
        }
    }
}
