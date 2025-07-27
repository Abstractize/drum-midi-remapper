using Models;

namespace Managers.Contracts;

public interface IMidiMapManager
{
    Task<string> RemapMidi(string sourceMapArg, string targetMapArg, string midiPath);
}
