namespace pv178_project.Visuals.Pages.Leaderboard;

public static class FilterValuesToDays
{
    public static DateTime GetTimeDateThreshold(this FilterValuesEnum filter)
    {
        return filter switch
        {
            FilterValuesEnum.Day => DateTime.Now.AddDays(-1),
            FilterValuesEnum.Week => DateTime.Now.AddDays(-7),
            FilterValuesEnum.Month => DateTime.Now.AddMonths(-1),
            FilterValuesEnum.Year => DateTime.Now.AddYears(-1),
            FilterValuesEnum.AllTime => DateTime.MinValue,
            _ => throw new ArgumentOutOfRangeException(nameof(filter), filter, "Unknown enum value")
        };
    }
}