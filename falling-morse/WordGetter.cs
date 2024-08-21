namespace pv178_project;

public class WordGetter
{
    private readonly string _wordsPath = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "assets", "english-nouns.txt");
    private readonly List<string> _words;

    public WordGetter()
    {
        using var sr = new StreamReader(_wordsPath);
        _words = new List<string>(sr.ReadToEnd().Split('\n'));
    }

    public string GetRandomWord()
    {
        int index = Random.Shared.Next(0, _words.Count - 1);
        return _words[index];
    }
}