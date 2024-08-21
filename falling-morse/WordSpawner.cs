using System.Timers;
using pv178_project.Models;
using Timer = System.Timers.Timer;

namespace pv178_project;

public class WordSpawner
{
    public event EventHandler<string>? WordSpawned;
    private readonly WordGetter _wordGetter = new();
    private readonly Timer _timer = new Timer();
    private readonly (int from, int to) _spawnInterval = DifficultyValues.SpawnInterval;


    public void Loop(CancellationToken ct)
    {
        _timer.Interval = 1000;
        _timer.Elapsed += SpawnWord;
        _timer.Enabled = true;
        while (!ct.IsCancellationRequested)
        {
            Thread.Yield();
        }
    }

    private void SpawnWord(object? sender, ElapsedEventArgs e)
    {
        string word = _wordGetter.GetRandomWord();
        OnWordSpawned(word);
        _timer.Interval = Random.Shared.Next(_spawnInterval.from, _spawnInterval.to);
    }
    
    protected virtual void OnWordSpawned(string word)
    {
        WordSpawned?.Invoke(this, word);
    }
}