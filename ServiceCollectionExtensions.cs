using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract)
            .ToList();

        foreach (var type in types)
        {
            var interfaces = type.GetInterfaces();
            if (interfaces.Any())
            {
                foreach (var @interface in interfaces)
                {
                    services.AddTransient(@interface, type);
                }
            }
            else
            {
                services.AddTransient(type);
            }
        }

        return services;
    }
}
