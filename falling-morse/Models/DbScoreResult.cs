using System.ComponentModel.DataAnnotations;
using pv178_project.Gamemodes;

namespace pv178_project.Models;

public class DbScoreResult
{
    public DbScoreResult(string playerName, int score, DifficultyEnum difficulty, GamemodeEnum gameMode)
    {
        Id = Guid.NewGuid().ToString();
        PlayerName = playerName;
        Score = score;
        Difficulty = difficulty;
        GameMode = gameMode;
        Date = DateTime.Now;
    }
    
    public DbScoreResult(string playerName, int score, DifficultyEnum difficulty, GamemodeEnum gameMode, DateTime date)
    {
        Id = Guid.NewGuid().ToString();
        PlayerName = playerName;
        Score = score;
        Difficulty = difficulty;
        GameMode = gameMode;
        Date = date;
    }

    public DbScoreResult()
    {
        
    }

    [Key]
    public string Id { get; set; }
    public string PlayerName { get; set; }
    public int Score { get; set; }
    public DateTime Date { get; set; }
    public DifficultyEnum Difficulty { get; set; }
    public GamemodeEnum GameMode { get; set; }
}