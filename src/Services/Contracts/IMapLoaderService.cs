using Models;

namespace Services.Contracts;

public interface IMapLoaderService
{
    abstract Task<DrumMap> LoadAsync(DrumMapType type);
}
