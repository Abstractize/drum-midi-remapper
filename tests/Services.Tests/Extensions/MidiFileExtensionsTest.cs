using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Models;

namespace Services.Extensions.Tests
{
    public class MidiFileExtensionsTest
    {
        private DrumMap CreateDrumMap(Dictionary<string, int> mapping)
        {
            return new DrumMap { Mapping = mapping };
        }

        private MidiFile CreateMidiFileWithNotes(params int[] noteNumbers)
        {
            var trackChunk = new TrackChunk();
            foreach (var note in noteNumbers)
            {
                trackChunk.Events.Add(new NoteOnEvent((SevenBitNumber)note, (SevenBitNumber)100));
                trackChunk.Events.Add(new NoteOffEvent((SevenBitNumber)note, (SevenBitNumber)0));
            }
            return new MidiFile(trackChunk);
        }

        [Fact]
        public void RemapNotes_MapsNotesAccordingToDrumMaps()
        {
            // Arrange
            var sourceMap = CreateDrumMap(new Dictionary<string, int>
            {
                { "Kick", 36 },
                { "Snare", 38 }
            });
            var targetMap = CreateDrumMap(new Dictionary<string, int>
            {
                { "Kick", 40 },
                { "Snare", 41 }
            });
            var midiFile = CreateMidiFileWithNotes(36, 38);

            // Act
            midiFile.RemapNotes(sourceMap, targetMap);

            // Assert
            var notes = new List<int>();
            foreach (var trackChunk in midiFile.GetTrackChunks())
            {
                foreach (var midiEvent in trackChunk.Events)
                {
                    if (midiEvent is NoteOnEvent noteOn)
                        notes.Add(noteOn.NoteNumber);
                }
            }
            Assert.Contains(40, notes); // Kick mapped
            Assert.Contains(41, notes); // Snare mapped
            Assert.DoesNotContain(36, notes);
            Assert.DoesNotContain(38, notes);
        }

        [Fact]
        public void RemapNotes_IgnoresNotesNotInSourceMap()
        {
            // Arrange
            var sourceMap = CreateDrumMap(new Dictionary<string, int>
            {
                { "Kick", 36 }
            });
            var targetMap = CreateDrumMap(new Dictionary<string, int>
            {
                { "Kick", 40 }
            });
            var midiFile = CreateMidiFileWithNotes(36, 38); // 38 is not in sourceMap

            // Act
            midiFile.RemapNotes(sourceMap, targetMap);

            // Assert
            var notes = new List<int>();
            foreach (var trackChunk in midiFile.GetTrackChunks())
            {
                foreach (var midiEvent in trackChunk.Events)
                {
                    if (midiEvent is NoteOnEvent noteOn)
                        notes.Add(noteOn.NoteNumber);
                }
            }
            Assert.Contains(40, notes); // Kick mapped
            Assert.Contains(38, notes); // Snare not mapped, remains
        }

        [Fact]
        public void RemapNotes_DoesNothingIfNoMapping()
        {
            // Arrange
            var sourceMap = CreateDrumMap(new Dictionary<string, int>());
            var targetMap = CreateDrumMap(new Dictionary<string, int>());
            var midiFile = CreateMidiFileWithNotes(36, 38);

            // Act
            midiFile.RemapNotes(sourceMap, targetMap);

            // Assert
            var notes = new List<int>();
            foreach (var trackChunk in midiFile.GetTrackChunks())
            {
                foreach (var midiEvent in trackChunk.Events)
                {
                    if (midiEvent is NoteOnEvent noteOn)
                        notes.Add(noteOn.NoteNumber);
                }
            }
            Assert.Contains(36, notes);
            Assert.Contains(38, notes);
        }
    }
}