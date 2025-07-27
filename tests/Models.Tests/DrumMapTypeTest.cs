namespace Models.Tests;

public class DrumMapTypeTest
{
    [Fact]
    public void DrumMapType_ContainsExpectedValues()
    {
        Assert.Equal(0, (int)DrumMapTypes.StevenSlate);
        Assert.Equal(1, (int)DrumMapTypes.GuitarPro);
        Assert.Equal(2, (int)DrumMapTypes.LogicPro);
        Assert.Equal(3, (int)DrumMapTypes.ProTools);
    }

    [Theory]
    [InlineData(DrumMapTypes.StevenSlate)]
    [InlineData(DrumMapTypes.GuitarPro)]
    [InlineData(DrumMapTypes.LogicPro)]
    [InlineData(DrumMapTypes.ProTools)]
    public void DrumMapType_IsDefined(DrumMapTypes type)
    {
        Assert.True(Enum.IsDefined(type));
    }

    [Theory]
    [InlineData("StevenSlate", DrumMapTypes.StevenSlate)]
    [InlineData("GuitarPro", DrumMapTypes.GuitarPro)]
    [InlineData("LogicPro", DrumMapTypes.LogicPro)]
    [InlineData("ProTools", DrumMapTypes.ProTools)]
    public void DrumMapType_ParseString_ReturnsCorrectEnum(string name, DrumMapTypes expected)
    {
        var parsed = (DrumMapTypes)Enum.Parse(typeof(DrumMapTypes), name);
        Assert.Equal(expected, parsed);
    }
}