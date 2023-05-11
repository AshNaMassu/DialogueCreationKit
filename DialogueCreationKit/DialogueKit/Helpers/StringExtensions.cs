using System.Text.RegularExpressions;

namespace DialogueCreationKit.DialogueKit.Helpers;

public static class StringExtensions
{

    public static bool IsCyrillicText(this string inputText)
    {
        return inputText.Where(c => Regex.IsMatch(c.ToString(), @"\p{L}"))
            .All(c => Regex.IsMatch(c.ToString(), @"\p{IsCyrillic}"));
    }

    public static string RemoveExtraSpaces(this string inputText)
    {
        return Regex.Replace(inputText, @"\s+", " ").Trim().ToLower();

    }
}