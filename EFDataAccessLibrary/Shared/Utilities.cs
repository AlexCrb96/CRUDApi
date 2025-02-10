using System.Globalization;
using System.Text.RegularExpressions;

namespace EFDataAccessLibrary.Shared;

public static class Utilities
{
    private static string NormalizeWhitespaces(string input)
    {
        return Regex.Replace(input.Trim(), @"\s+", " ");
    }

    public static string ToTitleCase(string input)
    {
        TextInfo stringFormatter = new CultureInfo("en-US").TextInfo;
        return stringFormatter.ToTitleCase(NormalizeWhitespaces(input));
    }
}