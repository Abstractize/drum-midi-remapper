using Services.Contracts;
using Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Services;

public static class ServiceCollectionEx
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IMidiFileService, MidiFileService>();
        services.AddSingleton<IMapLoaderService, MapLoaderService>();

        return services;
    }
}