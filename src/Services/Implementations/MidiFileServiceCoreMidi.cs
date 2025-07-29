#if MACCATALYST
using CoreMidi;
using Models;
using Services.Contracts;
using System.IO;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class MidiFileServiceCoreMidi : IMidiFileService
    {
        public async Task<Stream> RemapAsync(DrumMap sourceMap, DrumMap targetMap, Stream midiStream)
        {
            // Leer todos los bytes del stream
            using var ms = new MemoryStream();
            await midiStream.CopyToAsync(ms);
            byte[] midiData = ms.ToArray();

            // Procesar y remapear
            RemapMidiEvents(midiData, sourceMap, targetMap);

            return new MemoryStream(midiData);
        }

        // (Resto métodos igual...)
        private static void RemapMidiEvents(byte[] midiData, DrumMap sourceMap, DrumMap targetMap)
        {
            Dictionary<int, int> mapping = BuildMapping(sourceMap, targetMap);

            for (int i = 0; i < midiData.Length - 2; i++)
            {
                byte status = midiData[i];
                if ((status & 0xF0) == 0x90 || (status & 0xF0) == 0x80)
                {
                    byte note = midiData[i + 1];
                    if (mapping.TryGetValue(note, out int mappedNote))
                        midiData[i + 1] = (byte)mappedNote;
                }
            }
        }

        private static Dictionary<int, int> BuildMapping(DrumMap sourceMap, DrumMap targetMap)
        {
            var mapping = new Dictionary<int, int>();
            foreach (var kvp in sourceMap.Mapping)
            {
                if (targetMap.Mapping.TryGetValue(kvp.Key, out int targetNote))
                    mapping[kvp.Value] = targetNote;
            }
            return mapping;
        }

        private static void SendToCoreMidi(byte[] midiData)
        {
            var client = new MidiClient("RemapperClient");
            var outputPort = client.CreateOutputPort("OutputPort");
            var dest = MidiEndpoint.GetDestination(0);

            if (dest == null)
            {
                Console.WriteLine("⚠️ No external MIDI device found.");
                return;
            }

            var packet = new MidiPacket(0, midiData);
            outputPort.Send(dest, new[] { packet });
            Console.WriteLine("🎹 Sent remapped MIDI events to external MIDI device.");
        }
    }
}
#endif