using Models;

namespace Managers.Contracts;

public interface IMidiMapManager
{
    Task<Stream> RemapMidi(string sourceMapArg, string targetMapArg, Stream midiStream);
    Task<string> RemapMidi(string sourceMapArg, string targetMapArg, string midiPath);
}
