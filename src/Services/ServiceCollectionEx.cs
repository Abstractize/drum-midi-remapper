using Services.Contracts;
using Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Services;

public static class ServiceCollectionEx
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
#if MACCATALYST
    services.AddSingleton<IMidiFileService, MidiFileServiceCoreMidi>();
#else
        services.AddSingleton<IMidiFileService, MidiFileServiceDryWetMidi>();
#endif
        services.AddSingleton<IMapLoaderService, MapLoaderService>();

        return services;
    }
}