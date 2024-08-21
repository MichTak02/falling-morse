using pv178_project.Visuals.Pages.Leaderboard;

namespace pv178_project.Models;

public static class Settings
{
    private static readonly string NamePath = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "data", "name");
    public static DifficultyEnum Difficulty { get; set; } = DifficultyEnum.Easy;
    public static string Name { get; set; } = "Player";
    public static bool ShowHint { get; set; } = true;
    public static FilterValuesEnum LeaderboardFilter { get; set; } = FilterValuesEnum.AllTime;

    public static void LoadName()
    {
        using var sr = new StreamReader(NamePath);
        string name = sr.ReadToEnd();
        if (name.Length > 0)
        {
            Name = name.Trim();
        }
    }
    
    public static void SaveName()
    {
        using var sr = new StreamWriter(NamePath);
        sr.WriteLine(Name);
    }
}