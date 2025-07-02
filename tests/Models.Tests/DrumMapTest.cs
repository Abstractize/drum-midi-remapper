namespace Models.Tests;

public class DrumMapTest
{
    [Fact]
    public void DrumMap_DefaultConstructor_InitializesProperties()
    {
        var drumMap = new DrumMap();

        Assert.NotNull(drumMap.Name);
        Assert.Equal(string.Empty, drumMap.Name);
        Assert.NotNull(drumMap.Mapping);
        Assert.Empty(drumMap.Mapping);
    }

    [Fact]
    public void DrumMap_CanSetName()
    {
        var drumMap = new DrumMap
        {
            Name = "My Drum Map"
        };

        Assert.Equal("My Drum Map", drumMap.Name);
    }

    [Fact]
    public void DrumMap_CanAddMapping()
    {
        var drumMap = new DrumMap();
        drumMap.Mapping["Kick"] = 36;
        drumMap.Mapping["Snare"] = 38;

        Assert.Equal(2, drumMap.Mapping.Count);
        Assert.Equal(36, drumMap.Mapping["Kick"]);
        Assert.Equal(38, drumMap.Mapping["Snare"]);
    }

    [Fact]
    public void DrumMap_CanUpdateMapping()
    {
        var drumMap = new DrumMap();
        drumMap.Mapping["Kick"] = 36;
        drumMap.Mapping["Kick"] = 35;

        Assert.Single(drumMap.Mapping);
        Assert.Equal(35, drumMap.Mapping["Kick"]);
    }

    [Fact]
    public void DrumMap_CanRemoveMapping()
    {
        var drumMap = new DrumMap();
        drumMap.Mapping["Kick"] = 36;
        drumMap.Mapping.Remove("Kick");

        Assert.Empty(drumMap.Mapping);
    }
}