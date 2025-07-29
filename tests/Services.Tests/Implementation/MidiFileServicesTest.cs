
using Melanchall.DryWetMidi.Core;
using Models;
using Services.Implementations;


namespace Services.Tests.Implementation
{
    public class MidiFileServiceTest
    {
        [Fact]
        public async Task RemapAsync_FileDoesNotExist_ThrowsFileNotFoundException()
        {
            var service = new MidiFileServiceDryWetMidi();
            var sourceMap = new DrumMap();
            var targetMap = new DrumMap();
            using var fakeStream = new MemoryStream();

            var ex = await Assert.ThrowsAsync<FileNotFoundException>(() =>
                service.RemapAsync(sourceMap, targetMap, fakeStream));
            Assert.Contains("MIDI file not found", ex.Message);
        }

        [Fact]
        public async Task RemapAsync_ValidFile_RemapsAndSavesFile()
        {
            // Arrange
            var service = new MidiFileServiceDryWetMidi();
            var sourceMap = new DrumMap();
            var targetMap = new DrumMap();
            string tempFile = Path.GetTempFileName();
            string midiPath = Path.ChangeExtension(tempFile, ".mid");

            // Create a simple MIDI file
            var midiFile = new MidiFile();
            midiFile.Write(midiPath);

            try
            {
                // Act
                using (var midiStream = File.OpenRead(midiPath))
                {
                    await service.RemapAsync(sourceMap, targetMap, midiStream);
                }

                // Assert
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(midiPath));
                Assert.True(File.Exists(outputPath));
            }
            finally
            {
                if (File.Exists(midiPath))
                    File.Delete(midiPath);

                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(midiPath));
                if (File.Exists(outputPath))
                    File.Delete(outputPath);
            }
        }
    }
}