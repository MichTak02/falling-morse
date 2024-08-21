using pv178_project.Models;
using pv178_project.Visuals.Menus.MenuOptions;
using pv178_project.Visuals.Pages.Leaderboard;

namespace pv178_project.Visuals.Menus;

public class LeaderboardFilterMenu : GenericMenu<LeaderboardFilterMenuOptions>
{
    protected override string Heading => "Leaderboard filter";
    protected override string Description => $"Scores from:";
    protected override Dictionary<LeaderboardFilterMenuOptions, string> Options { get; } = new()
    {
        { LeaderboardFilterMenuOptions.Day, "Last day" },
        { LeaderboardFilterMenuOptions.Week, "Last week" },
        { LeaderboardFilterMenuOptions.Month, "Last month" },
        { LeaderboardFilterMenuOptions.Year, "Last year" },
        { LeaderboardFilterMenuOptions.AllTime, "All time" },
        { LeaderboardFilterMenuOptions.Back, "Back" }
    };

    public LeaderboardFilterMenu()
    {
        DescriptionAtBottom = false;
    }

    public override bool Resolve()
    {
        var selected = GetOption();

        switch (selected)
        {
            case LeaderboardFilterMenuOptions.Day:
                Settings.LeaderboardFilter = FilterValuesEnum.Day;
                break;
            case LeaderboardFilterMenuOptions.Week:
                Settings.LeaderboardFilter = FilterValuesEnum.Week;
                break;
            case LeaderboardFilterMenuOptions.Month:
                Settings.LeaderboardFilter = FilterValuesEnum.Month;
                break;
            case LeaderboardFilterMenuOptions.Year:
                Settings.LeaderboardFilter = FilterValuesEnum.Year;
                break;
            case LeaderboardFilterMenuOptions.AllTime:
                Settings.LeaderboardFilter = FilterValuesEnum.AllTime;
                break;
        }

        return true;
    }
}