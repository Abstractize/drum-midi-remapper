using Services.Contracts;
using Managers.Contracts;
using Models;

namespace Managers.Implementations;

public class MidiMapManager(IMapLoaderService mapLoader, IMidiFileService midiFileService) : IMidiMapManager
{
    private readonly IMapLoaderService _mapLoader = mapLoader;
    private readonly IMidiFileService _midiFileService = midiFileService;

    public async Task<string> RemapMidi(string sourceMapArg, string targetMapArg, string midiPath)
    {
        if (!Enum.TryParse<DrumMapTypes>(sourceMapArg, true, out var sourceMapType))
            throw new ArgumentException($"Invalid source map: '{sourceMapArg}'");


        if (!Enum.TryParse<DrumMapTypes>(targetMapArg, true, out var targetMapType))
            throw new ArgumentException($"Invalid target map: '{targetMapArg}'");

        DrumMap sourceMap = await _mapLoader.LoadAsync(sourceMapType);
        DrumMap targetMap = await _mapLoader.LoadAsync(targetMapType);

        return await _midiFileService.RemapAsync(sourceMap, targetMap, midiPath);
    }
}