namespace pv178_project.Utils;

public static class TextUtils
{
    public static string Underline(this string text, char character = '-')
    {
        text += $"{Environment.NewLine}{new string(character, text.Length)}";
        return text;
    }

    public static string Numbered(this string text, int number)
    {
        text = $"{number}) {text}";
        return text;
    }
    
    public static string MenuItem(this string text)
    {
        text = $"=> {text} <=";
        return text;
    }
    
    public static string BulletPoint(this string text)
    {
        text = $" - {text}";
        return text;
    }

    public static string Styled(this string text)
    {
        text = $"[{text}]";
        return text;
    }
}