using System.Text.RegularExpressions;

namespace GUI.Extensions;

public static partial class StringEx
{
    public static string PascalCaseToUpperWithSpaces(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        // Insert space before each uppercase letter that is not the first
        string withSpaces = MyRegex().Replace(input, " $1");

        // Convert everything to uppercase
        return withSpaces.ToUpper();
    }

    [GeneratedRegex("(?<!^)([A-Z])")]
    private static partial Regex MyRegex();
}