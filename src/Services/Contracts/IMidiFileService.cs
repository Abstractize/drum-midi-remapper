using Models;

namespace Services.Contracts;

public interface IMidiFileService
{
    public Task<Stream> RemapAsync(DrumMap sourceMap, DrumMap targetMap, Stream midiStream);
}
