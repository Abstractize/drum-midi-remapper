using System.Reflection;
using System.Text.Json;
using Models;
using Services.Contracts;

namespace Services.Implementations;

public class MapLoaderService : IMapLoaderService
{
    private readonly Assembly _assembly = Assembly.GetExecutingAssembly();

    public async Task<DrumMap> LoadAsync(DrumMapTypes type)
    {
        var resourceName = $"Services.Resources.Maps.{type}.json";

        using Stream? stream = _assembly.GetManifestResourceStream(resourceName) ??
            throw new FileNotFoundException($"Embedded map not found: {resourceName}");

        return await JsonSerializer.DeserializeAsync<DrumMap>(stream)
               ?? throw new Exception($"Error deserializing map: {type}");
    }
}