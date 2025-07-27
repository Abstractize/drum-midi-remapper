using Moq;
using Services.Contracts;
using Models;

namespace Managers.Tests.Implementations;

public class MidiMapManagerTest
{
    private const DrumMapTypes SOURCE = DrumMapTypes.GuitarPro;
    private const DrumMapTypes TARGET = DrumMapTypes.StevenSlate;
    private const string FILENAME = "test.mid";

    [Fact]
    public async Task RemapMidi_CallsServicesWithCorrectParameters()
    {
        // Arrange
        var mockMapLoader = new Mock<IMapLoaderService>();
        var mockMidiFileService = new Mock<IMidiFileService>();

        var sourceMap = new DrumMap();
        var targetMap = new DrumMap();

        var variables = new RemapVariables
        {
            SourceMapType = SOURCE.ToString(),
            TargetMapType = TARGET.ToString(),
            MidiPath = FILENAME
        };

        mockMapLoader.Setup(m => m.LoadAsync(SOURCE)).ReturnsAsync(sourceMap);
        mockMapLoader.Setup(m => m.LoadAsync(TARGET)).ReturnsAsync(targetMap);

        var manager = new MidiMapManager(mockMapLoader.Object, mockMidiFileService.Object);

        // Act
        await manager.RemapMidi(
            variables.SourceMapType,
            variables.TargetMapType,
            variables.MidiPath
        );

        // Assert
        mockMapLoader.Verify(m => m.LoadAsync(SOURCE), Times.Once);
        mockMapLoader.Verify(m => m.LoadAsync(TARGET), Times.Once);
        mockMidiFileService.Verify(m => m.RemapAsync(sourceMap, targetMap, "test.mid"), Times.Once);
    }
}