using Managers.Contracts;
using Managers.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Managers;

public static class ServiceCollectionEx
{
    public static IServiceCollection AddManagers(this IServiceCollection services)
    {
        services.AddTransient<IMidiMapManager, MidiMapManager>();
        services.AddTransient<ICliArgumentManager, CliArgumentManager>();

        return services;
    }
}