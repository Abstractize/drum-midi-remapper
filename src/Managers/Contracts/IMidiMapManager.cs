using Models;

namespace Managers.Contracts;

public interface IMidiMapManager
{
    Task RemapMidi(RemapVariables variables);
}
