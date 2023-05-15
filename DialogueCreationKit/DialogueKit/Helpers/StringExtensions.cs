using System.Text.RegularExpressions;

namespace DialogueCreationKit.DialogueKit.Helpers;

public static class StringExtensions
{
    public static string Copy(this string source)
    {
        char[] copyStr = new char[source.Length];
        source.CopyTo(0, copyStr, 0, copyStr.Length);
        return new string(copyStr.ToArray());
    }

    public static bool IsCyrillicText(this string inputText)
    {
        return inputText.Where(c => Regex.IsMatch(c.ToString(), @"\p{L}"))
            .All(c => Regex.IsMatch(c.ToString(), @"\p{IsCyrillic}"));
    }

    public static string RemoveExtraSpaces(this string inputText)
    {
        //return Regex.Replace(inputText, @"\s+", " ").Trim().ToLower();
        return Regex.Replace(Regex.Replace(inputText, @"\W", " "), @"\s+", " ").Trim().ToLower();
    }

    public static string[] RemoveExtraSpacesAndToWord(this string inputText)
    {
        return Regex.Replace(Regex.Replace(inputText, @"[^а-яА-ЯёЁ#]", " "), @"\s+", " ").Trim().ToLower().Split(" ");
    }
}