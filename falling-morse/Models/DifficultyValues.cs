namespace pv178_project.Models;

public static class DifficultyValues
{
    public static (int from, int to) SpeedInterval => 
        Settings.Difficulty switch
        {
            DifficultyEnum.Easy => (400, 800),
            DifficultyEnum.Medium => (800, 1400),
            DifficultyEnum.Hard => (1400, 2000),
            _ => throw new ArgumentOutOfRangeException()
        };
    
    public static (int from, int to) SpawnInterval => 
        Settings.Difficulty switch
        {
            DifficultyEnum.Easy => (8000, 12000),
            DifficultyEnum.Medium => (5000, 7500),
            DifficultyEnum.Hard => (3500, 5800),
            _ => throw new ArgumentOutOfRangeException()
        };
    
    public static int WordSpeed => 
        Settings.Difficulty switch
        {
            DifficultyEnum.Easy => 3,
            DifficultyEnum.Medium => 6,
            DifficultyEnum.Hard => 9,
            _ => throw new ArgumentOutOfRangeException()
        };
}