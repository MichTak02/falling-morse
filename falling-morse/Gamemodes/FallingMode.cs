using pv178_project.Db;
using pv178_project.Models;
using pv178_project.Utils;

namespace pv178_project.Gamemodes;

public class FallingMode(FallingModeType modeType)
{
    private const int InitialLives = 3;
    private static readonly Position Bottom = new(0, Console.WindowHeight - 1);
    private readonly (int from, int to) _speedInterval = DifficultyValues.SpeedInterval;
    private double RandomSpeed => Random.Shared.Next(_speedInterval.from, _speedInterval.to) / 1000.0;

    private FallingModeType ModeType { get; } = modeType;
    private readonly List<FallingWord> _fallingWords = new();
    private bool _running = true;

    private readonly ScreenElement<int> _score = new(new Position(0, 0), "Score", 0);
    private readonly ScreenElement<int> _lives = new(new Position(Console.WindowWidth - 9, 0), "Lives", InitialLives);
    private readonly ScreenElement<string> _inputField = new(Bottom, "Enter word", "");

    public void Run()
    {
        KeypressSender keyboardHandler = new KeypressSender();
        keyboardHandler.KeyPressed += HandleKeyPress;
        WordSpawner wordSpawner = new WordSpawner();
        wordSpawner.WordSpawned += HandleWordSpawned;

        var keyboardTokenSource = new CancellationTokenSource();
        var wordSpawnerTokenSource = new CancellationTokenSource();
        var keyboardToken = keyboardTokenSource.Token;
        var wordSpawnerToken = wordSpawnerTokenSource.Token;
        Task.Run(() => keyboardHandler.Loop(keyboardToken), keyboardToken);
        Task.Run(() => wordSpawner.Loop(wordSpawnerToken), wordSpawnerToken);

        while (_running)
        {
            Refresh();
            Thread.Sleep(100);
        }

        wordSpawnerTokenSource.Cancel();
        keyboardTokenSource.Cancel();

        if (_lives.Value <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        var gamemode = ModeType == FallingModeType.FromMorse ? GamemodeEnum.FallingMorse : GamemodeEnum.FallingText;
        int percentile;
        using (var db = new ScoreContext())
        {
            var allScores = db.ScoreResults
                .Where(data => data.Difficulty == Settings.Difficulty && data.GameMode == gamemode);
                
            var worseCount = allScores
                .Count(data => data.Score < _score.Value);
            percentile = !allScores.Any() ? 100 : (int) Math.Round((double) worseCount / allScores.Count() * 100);
        }
        
        WriteToDb();
        Console.Clear();
        Console.WriteLine("Game over!");
        Console.WriteLine($"Your score: {_score.Value}");
        Console.WriteLine($"You are better than {percentile} % of players.");
        Console.WriteLine("Press any key to exit.");
        _running = false;
        Console.ReadKey();
    }
    private void WriteToDb()
    {
        Console.Clear();
        Console.WriteLine("Saving score...");
        using var db = new ScoreContext();
        try
        {
            db.ScoreResults.Add(new DbScoreResult(Settings.Name, _score.Value, Settings.Difficulty,
                ModeType == FallingModeType.FromMorse ? GamemodeEnum.FallingMorse : GamemodeEnum.FallingText));
            db.SaveChanges();
        }
        catch (Exception)
        {
            Console.WriteLine("Error saving score to database.");
            Console.ReadKey();
        }
    }

    private void Refresh()
    {
        Console.Clear();
        if (_lives.Value <= 0)
        {
            _running = false;
            return;
        }

        RemoveOverflown();
        _score.WriteOut();
        _lives.WriteOut();
        ConsoleUtils.WriteAtPosition(new string('-', Console.WindowWidth), 0, 1);
        WriteWords();
        _inputField.WriteOut();
    }

    private void WriteWords()
    {
        foreach (var word in _fallingWords)
        {
            string converted = ModeType == FallingModeType.FromMorse ? word.MorseText : word.Text;
            ConsoleUtils.WriteAtPosition(converted.Styled(), word.Position);
            word.Shift();
        }
    }

    private void RemoveOverflown()
    {
        for (int i = _fallingWords.Count - 1; i >= 0; i--)
        {
            if (_fallingWords[i].WouldOverflow(Bottom.Y))
            {
                _fallingWords.Remove(_fallingWords[i]);
                _lives.Value -= 1;
            }
        }
    }

    private void HandleKeyPress(object? sender, ConsoleKeyInfo key)
    {
        string value = _inputField.Value;

        if (key.Key == ConsoleKey.Backspace && _inputField.Value.Length > 0)
        {
            _inputField.Value = value.Substring(0, value.Length - 1);
            return;
        }

        if (key.Key == ConsoleKey.Escape)
        {
            _running = false;
            return;
        }

        if (key.Key == ConsoleKey.Enter)
        {
            CheckWord(_inputField.Value);
            _inputField.Value = "";
            return;
        }

        _inputField.Value += key.KeyChar;
    }

    private void HandleWordSpawned(object? sender, string word)
    {
        int wordLength = ModeType == FallingModeType.FromMorse ? word.ToMorse().Length : word.Length;
        int maxX = Math.Max(Console.WindowWidth - wordLength, 0); // Avoid text wrapping
        
        _fallingWords.Add(new FallingWord(word, new Position(Random.Shared.Next(0, maxX), 2),
            RandomSpeed));
    }

    private void CheckWord(string enteredWord)
    {
        string converted = ModeType == FallingModeType.FromMorse ? enteredWord : enteredWord.FromMorse() ?? "";
        if (_fallingWords.Select(word => word.Text).Contains(converted, StringComparer.OrdinalIgnoreCase))
        {
            _fallingWords.Remove(new FallingWord(converted, new Position(0, 0), 0));
            _score.Value += converted.Length;
        }
        else if (_inputField.Value.Length != 0)
        {
            _lives.Value -= 1;
        }
    }
}