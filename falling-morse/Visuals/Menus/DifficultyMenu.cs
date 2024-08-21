using pv178_project.Models;
using pv178_project.Visuals.Menus.MenuOptions;

namespace pv178_project.Visuals.Menus;

public class DifficultyMenu : GenericMenu<DifficultyMenuOptions>
{
    protected override string Heading => "Difficulty";
    protected override string Description => $"Current difficulty: {Settings.Difficulty}";
    protected override Dictionary<DifficultyMenuOptions, string> Options { get; } = new()
    {
        { DifficultyMenuOptions.Easy, "Easy" },
        { DifficultyMenuOptions.Medium, "Medium" },
        { DifficultyMenuOptions.Hard, "Hard" },
        { DifficultyMenuOptions.Back, "Back" }
    };
    
    public override bool Resolve()
    {
        var selected = GetOption();
        
        switch (selected)
        {
            case DifficultyMenuOptions.Easy:
                Settings.Difficulty = DifficultyEnum.Easy;
                break;
            case DifficultyMenuOptions.Medium:
                Settings.Difficulty = DifficultyEnum.Medium;
                break;
            case DifficultyMenuOptions.Hard:
                Settings.Difficulty = DifficultyEnum.Hard;
                break;
            case DifficultyMenuOptions.Back:
                return false;
        }

        return true;
    }
}