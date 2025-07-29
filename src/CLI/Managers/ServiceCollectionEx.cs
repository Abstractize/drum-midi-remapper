using CLI.Managers.Contracts;
using CLI.Managers.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace CLI.Managers;

public static class ServiceCollectionEx
{
    public static IServiceCollection AddCLIManagers(this IServiceCollection services)
    {
        services.AddTransient<ICliArgumentManager, CliArgumentManager>();

        return services;
    }
}