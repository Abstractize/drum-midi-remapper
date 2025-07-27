using Models;

namespace Managers.Tests.Implementations
{
    public class CliArgumentManagerTest
    {
        [Fact]
        public async Task Execute_ValidArguments_ReturnsRemapVariables()
        {
            var manager = new CliArgumentManager();
            var args = new[] { "GuitarPro", "LogicPro", "test.mid" };

            var result = await manager.Execute(args);

            Assert.Equal(DrumMapTypes.GuitarPro.ToString(), result.SourceMapType);
            Assert.Equal(DrumMapTypes.LogicPro.ToString(), result.TargetMapType);
            Assert.Equal("test.mid", result.MidiPath);
        }

        [Fact]
        public async Task Execute_InsufficientArguments_ThrowsNullReferenceException()
        {
            var manager = new CliArgumentManager();
            var args = new[] { "GuitarPro", "LogicPro" };

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => manager.Execute(args));
            Assert.Contains("Insufficient arguments", ex.Message);
        }

        [Fact]
        public async Task Execute_InvalidSourceMap_ThrowsArgumentException()
        {
            var manager = new CliArgumentManager();
            var args = new[] { "InvalidMap", "LogicPro", "test.mid" };

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => manager.Execute(args));
            Assert.Contains("Invalid source map", ex.Message);
        }

        [Fact]
        public async Task Execute_InvalidTargetMap_ThrowsArgumentException()
        {
            var manager = new CliArgumentManager();
            var args = new[] { "GuitarPro", "InvalidMap", "test.mid" };

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => manager.Execute(args));
            Assert.Contains("Invalid target map", ex.Message);
        }
    }
}