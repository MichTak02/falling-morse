using pv178_project.Visuals.Menus.MenuOptions;
using pv178_project.Visuals.Pages;

namespace pv178_project.Visuals.Menus;

public class MainMenu : GenericMenu<MainMenuOptions>
{
    protected override string Heading =>
        "\n\n  _____     _ _ _               __  __                     \n |  ___|_ _| | (_)_ __   __ _  |  \\/  | ___  _ __ ___  ___ \n | |_ / _` | | | | '_ \\ / _` | | |\\/| |/ _ \\| '__/ __|/ _ \\\n |  _| (_| | | | | | | | (_| | | |  | | (_) | |  \\__ \\  __/\n |_|  \\__,_|_|_|_|_| |_|\\__, | |_|  |_|\\___/|_|  |___/\\___|\n                        |___/                              \n\n";

    protected override string Description => "Use UP and DOWN arrows to navigate. Press ENTER to select menu item.";

    protected override Dictionary<MainMenuOptions, string> Options { get; } = new()
    {
        { MainMenuOptions.Play, "Play" },
        { MainMenuOptions.Settings, "Settings" },
        { MainMenuOptions.Leaderboard, "Leaderboard" },
        { MainMenuOptions.Stats, "Statistics" },
        { MainMenuOptions.About, "About"},
        { MainMenuOptions.Exit, "Exit" }
    };

    public MainMenu()
    {
        UnderlineHeading = false;
    }

    public override bool Resolve()
    {
        var selected = GetOption();

        switch (selected)
        {
            case MainMenuOptions.Play:
                PlayMenu playMenu = new();
                playMenu.Resolve();
                break;
            case MainMenuOptions.Settings:
                SettingsMenu settingsMenu = new();
                settingsMenu.Resolve();
                break;
            case MainMenuOptions.Leaderboard:
                Pages.Leaderboard.Leaderboard leaderboard = new();
                leaderboard.Run();
                break;
            case MainMenuOptions.Stats:
                Stats stats = new();
                stats.Show();
                break;
            case MainMenuOptions.About:
                About about = new();
                about.Show();
                break;
            case MainMenuOptions.Exit:
                return false;
        }

        return true;
    }
}