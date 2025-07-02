using Services.Contracts;
using Managers.Contracts;
using Models;

namespace Managers.Implementations;

public class MidiMapManager(IMapLoaderService mapLoader, IMidiFileService midiFileService) : IMidiMapManager
{
    private readonly IMapLoaderService _mapLoader = mapLoader;
    private readonly IMidiFileService _midiFileService = midiFileService;

    public async Task RemapMidi(RemapVariables variables)
    {
        DrumMap sourceMap = await _mapLoader.LoadAsync(variables.SourceMapType);
        DrumMap targetMap = await _mapLoader.LoadAsync(variables.TargetMapType);

        await _midiFileService.RemapAsync(sourceMap, targetMap, variables.MidiPath);
    }
}