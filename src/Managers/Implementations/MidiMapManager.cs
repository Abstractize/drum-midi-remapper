using Services.Contracts;
using Managers.Contracts;
using Models;
using System.IO;
using System.Threading.Tasks;

namespace Managers.Implementations;

public class MidiMapManager : IMidiMapManager
{
    private readonly IMapLoaderService _mapLoader;
    private readonly IMidiFileService _midiFileService;

    public MidiMapManager(IMapLoaderService mapLoader, IMidiFileService midiFileService)
    {
        _mapLoader = mapLoader;
        _midiFileService = midiFileService;
    }

    public async Task<Stream> RemapMidi(string sourceMapArg, string targetMapArg, Stream midiStream)
    {
        if (!Enum.TryParse<DrumMapTypes>(sourceMapArg, true, out var sourceMapType))
            throw new ArgumentException($"Invalid source map: '{sourceMapArg}'");

        if (!Enum.TryParse<DrumMapTypes>(targetMapArg, true, out var targetMapType))
            throw new ArgumentException($"Invalid target map: '{targetMapArg}'");

        DrumMap sourceMap = await _mapLoader.LoadAsync(sourceMapType);
        DrumMap targetMap = await _mapLoader.LoadAsync(targetMapType);

        return await _midiFileService.RemapAsync(sourceMap, targetMap, midiStream);
    }

    public async Task<string> RemapMidi(string sourceMapArg, string targetMapArg, string midiPath)
    {
        using var fileStream = File.OpenRead(midiPath);
        Stream stream = await RemapMidi(sourceMapArg, targetMapArg, fileStream);

        string tempFilePath = Path.GetTempFileName();

        using (var outputStream = File.OpenWrite(tempFilePath))
        {
            await stream.CopyToAsync(outputStream);
        }

        stream.Dispose();
        return tempFilePath;
    }
}