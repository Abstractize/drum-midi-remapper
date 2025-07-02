using Models;

namespace Services.Contracts;

public interface IMidiFileService
{
    public Task RemapAsync(DrumMap sourceMap, DrumMap targetMap, string midiFilePath);
}
