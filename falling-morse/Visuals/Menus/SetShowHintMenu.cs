using pv178_project.Models;
using pv178_project.Visuals.Menus.MenuOptions;

namespace pv178_project.Visuals.Menus;

public class SetShowHintMenu : GenericMenu<SetShowHintMenuOptions>
{
    protected override string Heading => "Show hint";
    private readonly string _hintText = Settings.ShowHint ? "On" : "Off";
    protected override string Description => $"Current setting: {_hintText}";

    protected override Dictionary<SetShowHintMenuOptions, string> Options { get; } = new()
    {
        { SetShowHintMenuOptions.On, "On" },
        { SetShowHintMenuOptions.Off, "Off" },
        { SetShowHintMenuOptions.Back, "Back" }
    };

    public override bool Resolve()
    {
        var selected = GetOption();
        
        switch (selected)
        {
            case SetShowHintMenuOptions.On:
                Settings.ShowHint = true;
                break;
            case SetShowHintMenuOptions.Off:
                Settings.ShowHint = false;
                break;
            case SetShowHintMenuOptions.Back:
                return false;
        }
        
        return true;
    }
}