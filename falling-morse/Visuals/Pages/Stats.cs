using Microsoft.EntityFrameworkCore;
using pv178_project.Db;
using pv178_project.Models;
using pv178_project.Utils;

namespace pv178_project.Visuals.Pages;

public class Stats
{
    private DbSet<DbScoreResult> DbData { get; set; }

    public Stats()
    {
        Console.Clear();
        Console.WriteLine("Loading...");
        DbData = new ScoreContext().ScoreResults;
    }

    public void Show()
    {
        Console.Clear();
        Console.WriteLine("Statistics".Underline());
        Console.WriteLine();
        Console.WriteLine($"Most played mode: {GetMostPlayedMode()}");
        Console.WriteLine($"Total games played: {DbData.Count()}");
        var mostActivePlayer = GetMostActivePlayer();
        Console.WriteLine($"Most active player: {mostActivePlayer.name} ({mostActivePlayer.score} games played)");
        Console.WriteLine();
        Console.WriteLine("Press any key to return to main menu...");
        Console.ReadKey();
    }

    private string GetMostPlayedMode()
    {
        var results = DbData
            .GroupBy(result => result.GameMode,
                (key, g) => new { gamemode = key, count = g.Count() })
            .OrderByDescending(result => result.count)
            .First();

        return results.gamemode.ToString();
    }

    private (string name, int score) GetMostActivePlayer()
    {
        var results = DbData
            .GroupBy(result => result.PlayerName,
                (key, g) => new { name = key, count = g.Count() })
            .OrderByDescending(result => result.count)
            .First();

        return (results.name, results.count);
    }
}