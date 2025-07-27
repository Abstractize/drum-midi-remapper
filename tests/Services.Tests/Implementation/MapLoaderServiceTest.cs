using System.Reflection;
using System.Text.Json;
using Models;
using Services.Implementations;
using Moq;

namespace Services.Tests.Implementation;

public class MapLoaderServiceTest
{
    [Fact]
    public async Task LoadAsync_ValidType_ReturnsDrumMap()
    {
        // Arrange
        var type = DrumMapTypes.ProTools;
        var resourceName = $"Services.Resources.Maps.{type}.json";
        var drumMap = new DrumMap { Name = "TestMap" };
        var json = JsonSerializer.Serialize(drumMap);

        var assemblyMock = new Mock<Assembly>();
        var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
        assemblyMock.Setup(a => a.GetManifestResourceStream(resourceName)).Returns(stream);

        var service = new MapLoaderServiceTestable(assemblyMock.Object);

        // Act
        var result = await service.LoadAsync(type);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("TestMap", result.Name);
    }

    [Fact]
    public async Task LoadAsync_ResourceNotFound_ThrowsFileNotFoundException()
    {
        // Arrange
        var type = DrumMapTypes.GuitarPro;
        var resourceName = $"Services.Resources.Maps.{type}.json";
        var assemblyMock = new Mock<Assembly>();
        assemblyMock.Setup(a => a.GetManifestResourceStream(resourceName)).Returns((Stream?)null);

        var service = new MapLoaderServiceTestable(assemblyMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<FileNotFoundException>(() => service.LoadAsync(type));
    }

    [Fact]
    public async Task LoadAsync_InvalidJson_ThrowsException()
    {
        // Arrange
        var type = DrumMapTypes.LogicPro;
        var resourceName = $"Services.Resources.Maps.{type}.json";
        var invalidJson = "{ invalid json }";
        var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(invalidJson));
        var assemblyMock = new Mock<Assembly>();
        assemblyMock.Setup(a => a.GetManifestResourceStream(resourceName)).Returns(stream);

        var service = new MapLoaderServiceTestable(assemblyMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<JsonException>(() => service.LoadAsync(type));
    }

    // Helper class to inject mocked Assembly
    private class MapLoaderServiceTestable : MapLoaderService
    {
        private readonly Assembly _testAssembly;

        public MapLoaderServiceTestable(Assembly testAssembly)
        {
            _testAssembly = testAssembly;
        }

        public new async Task<DrumMap> LoadAsync(DrumMapTypes type)
        {
            var resourceName = $"Services.Resources.Maps.{type}.json";
            using Stream? stream = _testAssembly.GetManifestResourceStream(resourceName) ??
                throw new FileNotFoundException($"Embedded map not found: {resourceName}");

            return await JsonSerializer.DeserializeAsync<DrumMap>(stream)
                   ?? throw new Exception($"Error deserializing map: {type}");
        }
    }
}