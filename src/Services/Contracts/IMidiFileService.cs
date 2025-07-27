using Models;

namespace Services.Contracts;

public interface IMidiFileService
{
    public Task<string> RemapAsync(DrumMap sourceMap, DrumMap targetMap, string midiFilePath);
}
