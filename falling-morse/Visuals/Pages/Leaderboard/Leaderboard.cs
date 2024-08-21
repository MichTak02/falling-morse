using Microsoft.EntityFrameworkCore;
using pv178_project.Db;
using pv178_project.Gamemodes;
using pv178_project.Models;
using pv178_project.Utils;
using pv178_project.Visuals.Menus;
using Table = ConsoleTable.Table;

namespace pv178_project.Visuals.Pages.Leaderboard;

public class Leaderboard
{
    private DbSet<DbScoreResult> DbData { get; set; } = null!;
    private int _difficultyIndex;
    private int _gamemodeIndex;
    private DifficultyEnum CurrentDifficulty => Enum.GetValues<DifficultyEnum>()[_difficultyIndex];
    private GamemodeEnum CurrentGamemode => Enum.GetValues<GamemodeEnum>()[_gamemodeIndex];
    private bool _running = true;
    private readonly Dictionary<FilterValuesEnum, string> _filterText = new()
    {
        { FilterValuesEnum.Day, "last day" },
        { FilterValuesEnum.Week, "last week" },
        { FilterValuesEnum.Month, "last month" },
        { FilterValuesEnum.Year, "last year" },
        { FilterValuesEnum.AllTime, "all time" }
    };
    
    
    private void Loading()
    {
        Console.Clear();
        Console.WriteLine("Loading...");
    }

    public void Run()
    {
        Loading();
        DbData = new ScoreContext().ScoreResults;
        
        while (_running)
        {
            Show();
        }
    }
    private void Show()
    {
        var dateThreshold = Settings.LeaderboardFilter.GetTimeDateThreshold();
        var allResults = DbData
            .Where(result => result.Date >= dateThreshold)
            .Where(result => result.Difficulty == CurrentDifficulty && result.GameMode == CurrentGamemode)
            .OrderByDescending(result => result.Score);
        
        double avgScore = Math.Round(!allResults.Any() ? 0 : allResults.Average(result => result.Score), 2);
        var results = allResults.Take(10).ToList();
            
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Leaderboard".Underline());
        Console.WriteLine();
        Console.WriteLine($"Gamemode: {CurrentGamemode}");
        Console.WriteLine($"Difficulty: {CurrentDifficulty}");
        Console.WriteLine($"Filter: {_filterText[Settings.LeaderboardFilter]}");
        Console.WriteLine();
        
        var table = new Table().AddColumn("#", "Player", "Score", "Date");
        for (var i = 0; i < results.Count; i++)
        {
            table.AddRow(i + 1, results[i].PlayerName, results[i].Score, results[i].Date.Date.ToShortDateString());
        }
        Console.WriteLine(table);
        Console.WriteLine($"Avg. score: {avgScore}");
        Console.WriteLine();
        Console.WriteLine("Press:");
        Console.WriteLine("UP or DOWN to change difficulty".BulletPoint());
        Console.WriteLine("LEFT or RIGHT to change gamemode".BulletPoint());
        Console.WriteLine("F to change filter".BulletPoint());
        Console.WriteLine("ESC to go back".BulletPoint());
        HandleOption();
    }

    private void HandleOption()
    {
        var key = Console.ReadKey();

        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                _difficultyIndex = MathUtils.Modulo(_difficultyIndex - 1, Enum.GetValues<DifficultyEnum>().Length);
                break;
            case ConsoleKey.DownArrow:
                _difficultyIndex = MathUtils.Modulo(_difficultyIndex + 1, Enum.GetValues<DifficultyEnum>().Length);
                break;
            case ConsoleKey.LeftArrow:
                _gamemodeIndex = MathUtils.Modulo(_gamemodeIndex - 1, Enum.GetValues<GamemodeEnum>().Length);
                break;
            case ConsoleKey.RightArrow:
                _gamemodeIndex = MathUtils.Modulo(_gamemodeIndex + 1, Enum.GetValues<GamemodeEnum>().Length);
                break;
            case ConsoleKey.F:
                LeaderboardFilterMenu leaderboardFilterMenu = new();
                leaderboardFilterMenu.Resolve();
                break;
            case ConsoleKey.Escape:
                _running = false;
                break;
        }
    }
}