using pv178_project.Visuals.Menus.MenuOptions;

namespace pv178_project.Visuals.Menus;

public class SettingsMenu : GenericMenu<SettingsMenuOptions>
{
    protected override string Heading => "Settings";
    protected override string Description => "Use UP and DOWN arrows to navigate. Press ENTER to select menu item.";
    protected override Dictionary<SettingsMenuOptions, string> Options { get; } = new()
    {
        { SettingsMenuOptions.SetName, "Set name" },
        { SettingsMenuOptions.SetDifficulty, "Set difficulty" },
        { SettingsMenuOptions.SetShowHint, "Show hint" },
        { SettingsMenuOptions.Back, "Back" }
    };
    public override bool Resolve()
    {
        var selected = GetOption();
        
        switch (selected)
        {
            case SettingsMenuOptions.SetName:
                SetNameMenu setNameMenu = new();
                setNameMenu.Resolve();
                break;
            case SettingsMenuOptions.SetDifficulty:
                DifficultyMenu difficultyMenu = new();
                difficultyMenu.Resolve();
                break;
            case SettingsMenuOptions.SetShowHint:
                SetShowHintMenu setShowHintMenu = new();
                setShowHintMenu.Resolve();
                break;
            case SettingsMenuOptions.Back:
                return false;
        }

        return true;
    }
}