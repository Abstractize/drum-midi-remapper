using Managers.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MidiMapController(IMidiMapManager midiMapManager) : ControllerBase
    {
        private readonly IMidiMapManager _midiMapManager = midiMapManager;

        [HttpPost]
        public async Task<string> RemapMidi(string sourceMapType, string targetMapType, string midiPath)
        => await _midiMapManager.RemapMidi(sourceMapType, targetMapType, midiPath);
    }
}