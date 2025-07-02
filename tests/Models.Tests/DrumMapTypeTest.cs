namespace Models.Tests;

public class DrumMapTypeTest
{
    [Fact]
    public void DrumMapType_ContainsExpectedValues()
    {
        Assert.Equal(0, (int)DrumMapType.StevenSlate);
        Assert.Equal(1, (int)DrumMapType.GuitarPro);
        Assert.Equal(2, (int)DrumMapType.LogicPro);
        Assert.Equal(3, (int)DrumMapType.ProTools);
    }

    [Theory]
    [InlineData(DrumMapType.StevenSlate)]
    [InlineData(DrumMapType.GuitarPro)]
    [InlineData(DrumMapType.LogicPro)]
    [InlineData(DrumMapType.ProTools)]
    public void DrumMapType_IsDefined(DrumMapType type)
    {
        Assert.True(Enum.IsDefined(type));
    }

    [Theory]
    [InlineData("StevenSlate", DrumMapType.StevenSlate)]
    [InlineData("GuitarPro", DrumMapType.GuitarPro)]
    [InlineData("LogicPro", DrumMapType.LogicPro)]
    [InlineData("ProTools", DrumMapType.ProTools)]
    public void DrumMapType_ParseString_ReturnsCorrectEnum(string name, DrumMapType expected)
    {
        var parsed = (DrumMapType)Enum.Parse(typeof(DrumMapType), name);
        Assert.Equal(expected, parsed);
    }
}