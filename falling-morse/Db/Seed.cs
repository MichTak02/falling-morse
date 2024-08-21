using pv178_project.Gamemodes;
using pv178_project.Models;

namespace pv178_project.Db;

public static class Seed
{
    private static readonly List<string> Names =
    [
        "John", "Jane", "Alice", "Bob", "Charlie", "David", "Eve", "Frank", "Grace", "Heidi", "Ivan", "Jack", "Kate",
        "Liam", "Mia", "Nina", "Oliver", "Pam", "Quinn", "Ruth", "Sam", "Tina", "Ursula", "Victor", "Wendy", "Xander",
        "Yvonne", "Zack"
    ];

    private static string RandomName => Names[Random.Shared.Next(0, Names.Count)];
    private static int RandomScore => Random.Shared.Next(0, 100);

    private static GamemodeEnum RandomGamemode =>
        Random.Shared.Next(0, 3) switch
        {
            0 => GamemodeEnum.FallingText,
            1 => GamemodeEnum.FallingMorse,
            _ => GamemodeEnum.SoundToText,
        };

    private static DifficultyEnum RandomDifficulty => Random.Shared.Next(0, 3) switch
    {
        0 => DifficultyEnum.Easy,
        1 => DifficultyEnum.Medium,
        _ => DifficultyEnum.Hard
    };

    private static DateTime RandomDate => DateTime.Now.AddDays(-Random.Shared.Next(0, 365 * 2));

    public static void Run()
    {
        using var db = new ScoreContext();
        for (int i = 0; i < 100; i++)
        {
            db.ScoreResults.Add(
                new DbScoreResult(RandomName, RandomScore, RandomDifficulty, RandomGamemode, RandomDate));
        }

        db.SaveChanges();
    }
}