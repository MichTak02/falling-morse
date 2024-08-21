using MorseSharp;
using NetCoreAudio;

namespace pv178_project.Models;

public class BeepingWord
{
    public string Text { get; }
    public Player Player { get; } = new();
    private readonly (int wordSpeed, int frequency) _audioOptions = (DifficultyValues.WordSpeed, 600);

    public BeepingWord(string text)
    {
        Text = text;
        CreateSoundFile();
    }
    
    private void CreateSoundFile()
    {
        Morse.GetConverter().ForLanguage(Language.English)
            .ToMorse(Text)
            .ToAudio()
            .SetAudioOptions(_audioOptions.wordSpeed, _audioOptions.wordSpeed, _audioOptions.frequency)
            .GetBytes(out Span<byte> morse);
        File.WriteAllBytes($"{Text}.wav", morse.ToArray());
    }
    
    public void Play()
    {
        Player.Play($"{Text}.wav");
    }
}