using pv178_project.Models;
using pv178_project.Utils;

namespace pv178_project.Gamemodes;

public class SoundToTextMode
{
    private const int InitialLives = 3;

    private bool _running = true;
    private BeepingWord? _currentWord;
    private readonly WordGetter _wordGetter = new();
    private readonly ScreenElement<int> _score = new(new Position(0, 0), "Score", 0);
    private readonly ScreenElement<int> _lives = new(new Position(Console.WindowWidth - 9, 0), "Lives", InitialLives);
    private readonly ScreenElement<string> _info = new(new Position(0, 3), "Info", "");
    private static readonly Position Bottom = new(0, Console.WindowHeight - 1);
    private readonly ScreenElement<string> _inputField = new(Bottom, "Enter word", "");
    
    private readonly Position _hintPosition = new(0, 6);
    
    private bool _waitingConfirmation = true;

    public void Run()
    {
        KeypressSender keyboardHandler = new KeypressSender();
        keyboardHandler.KeyPressed += HandleKeyPress;
        var keyboardTokenSource = new CancellationTokenSource();
        var keyboardToken = keyboardTokenSource.Token;
        Task.Run(() => keyboardHandler.Loop(keyboardToken), keyboardToken);

        while (_running)
        {
            Refresh();
            Thread.Sleep(100);
        }

        _currentWord?.Player.Stop();
        keyboardTokenSource.Cancel();
        
        if (_lives.Value <= 0)
        {
            Console.Clear();
            Console.WriteLine("Game over!");
            Console.WriteLine($"Your score: {_score.Value}");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }

    private void Refresh()
    {
        Console.Clear();
        SetInfo();
        if (_lives.Value <= 0)
        {
            _running = false;
            return;
        }

        _score.WriteOut();
        _lives.WriteOut();
        ConsoleUtils.WriteAtPosition(new string('-', Console.WindowWidth), 0, 1);
        _info.WriteOut();
        if (Settings.ShowHint)
        {
            ConsoleUtils.WriteAtPosition(MorseTranslator.GetTable(), _hintPosition);
        }
        _inputField.WriteOut();
    }

    private void GenerateAndPlayWord()
    {
        string word = _wordGetter.GetRandomWord();
        _currentWord = new BeepingWord(word);
        _currentWord.Play();
    }

    private void SetInfo()
    {
        if (_currentWord == null)
        {
            _info.Value = "Press Enter to start";
            return;
        }
        
        if (_currentWord.Player.Playing)
        {
            _info.Value = "Playing word...";
        }
        else if (!_waitingConfirmation)
        {
            _info.Value = "Playing stopped, enter the word and press Enter";
        }
    }

    private void HandleKeyPress(object? sender, ConsoleKeyInfo key)
    {
        if (_waitingConfirmation)
        {
            _waitingConfirmation = false;
            if (_lives.Value > 0)
            {
                GenerateAndPlayWord();
            }
            return;
        }

        string value = _inputField.Value;

        if (key.Key == ConsoleKey.Backspace && value.Length > 0)
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
            if (_currentWord != null && _inputField.Value.Equals(""))
            {
                return;
            }
            _waitingConfirmation = true;
            CheckWord(_inputField.Value);
            _inputField.Value = "";
            _currentWord?.Player.Stop();
            return;
        }

        _inputField.Value += key.KeyChar;
    }

    private void CheckWord(string enteredWord)
    {
        if (enteredWord.Equals(_currentWord?.Text))
        {
            _score.Value += enteredWord.Length;
            _info.Value = $"Correct!{Environment.NewLine}";
            _info.Value += "Press any key to continue...";
        }
        else if (_inputField.Value.Length != 0)
        {
            _lives.Value -= 1;
            _info.Value = $"Incorrect, correct answer was: {_currentWord?.Text}{Environment.NewLine}";
            _info.Value += "Press any key to continue...";
        }
    }
}