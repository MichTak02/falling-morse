using System.Text;
using System.Text.RegularExpressions;

namespace pv178_project.Utils;

public static class MorseTranslator
{
    private static readonly Dictionary<char, string> TextToMorseDict = new()
    {
        {'A', ".-"},
        {'B', "-..."},
        {'C', "-.-."},
        {'D', "-.."},
        {'E', "."},
        {'F', "..-."},
        {'G', "--."},
        {'H', "...."},
        {'I', ".."},
        {'J', ".---"},
        {'K', "-.-"},
        {'L', ".-.."},
        {'M', "--"},
        {'N', "-."},
        {'O', "---"},
        {'P', ".--."},
        {'Q', "--.-"},
        {'R', ".-."},
        {'S', "..."},
        {'T', "-"},
        {'U', "..-"},
        {'V', "...-"},
        {'W', ".--"},
        {'X', "-..-"},
        {'Y', "-.--"},
        {'Z', "--.."},
        {'0', "-----"},
        {'1', ".----"},
        {'2', "..---"},
        {'3', "...--"},
        {'4', "....-"},
        {'5', "....."},
        {'6', "-...."},
        {'7', "--..."},
        {'8', "---.."},
        {'9', "----."},
        {' ', ""},
        {'.', "|"}
    };

    private const int EnglishLettersCount = 26;
    private static readonly Dictionary<string, char> MorseToTextDict =
        TextToMorseDict.Keys
            .Zip(TextToMorseDict.Values, (letter, morse) => new { letter, morse })
            .ToDictionary(item => item.morse, item => item.letter);
    
    public static string ToMorse(this string text)
    {
        text = text
            .Normalize(NormalizationForm.FormD) // Ignore diacritics
            .ToUpper()
            .ShrinkSpaces()
            .RemoveUnknownChars();
        StringBuilder morseText = new StringBuilder();
        foreach (var c in text)
        {
            morseText.Append(TextToMorseDict[c]).Append('|');
        }

        if (text[^1] != '.')
        {
            morseText.Append('|');
        }
        return morseText.ToString();
    }

    public static string? FromMorse(this string morseText)
    {
        
        morseText = morseText.Replace("/", "|");
        if (!IsMorseValid(morseText))
        {
            return null;
        }
        
        StringBuilder text = new StringBuilder();
        var splitted = morseText.Split("|");
        foreach (var morseLetter in splitted)
        {
            if (!MorseToTextDict.TryGetValue(morseLetter, out char letter))
            {
                return null;
            }

            text.Append(letter);
        }

        text.Remove(text.Length - 1, 1); // Remove trailing space
        text.Replace("  ", ". ");
        return text.ToString().Trim();
    }

    private static bool IsMorseValid(string text)
    {
        text = text.Trim().Replace(" ", "");
        return !Regex.IsMatch(text, "[^|.-]") && !Regex.IsMatch(text, @"\|\|\|\|+");
    }

    private static string ShrinkSpaces(this string text)
    {
        return Regex.Replace(text.Trim(), @"\s+", " ").Replace(". ", ".");
    }

    private static string RemoveUnknownChars(this string text)
    {
        return Regex.Replace(text, @"[^A-Z0-9 .]", "");
    }
    
    public static string GetTable()
    {
        StringBuilder table = new StringBuilder();
        table.Append("Hint table:\n");
        
        var hintDict = TextToMorseDict.Take(EnglishLettersCount).ToDictionary(pair => pair.Key, pair => pair.Value);
        int dictCount = hintDict.Count;
        for (int i = 0; i < dictCount / 2; i++)
        {
            table.Append($"{hintDict.Keys.ElementAt(i)} {hintDict.Values.ElementAt(i)}\t" +
                         $"{hintDict.Keys.ElementAt(i + dictCount / 2)} {hintDict.Values.ElementAt(i + dictCount / 2)}\n");
        }
        
        if (dictCount % 2 == 1)
        {
            table.Append($"{hintDict.Keys.ElementAt(dictCount - 1)} {hintDict.Values.ElementAt(dictCount - 1)}\n");
        }

        return table.ToString();
    }
}