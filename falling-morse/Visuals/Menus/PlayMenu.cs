using pv178_project.Gamemodes;
using pv178_project.Visuals.Menus.MenuOptions;

namespace pv178_project.Visuals.Menus;

public class PlayMenu : GenericMenu<PlayMenuOptions>
{
    protected override string Heading => "Select game mode";
    protected override string Description => "Use UP and DOWN arrows to navigate. Press ENTER to select menu item.";

    protected override Dictionary<PlayMenuOptions, string> Options { get; } = new()
    {
        { PlayMenuOptions.TextToMorse, "Text to morse" },
        { PlayMenuOptions.MorseToText, "Morse to text" },
        { PlayMenuOptions.SoundToText, "Sound to text" },
        { PlayMenuOptions.Back, "Back" }
    };
    
    public override bool Resolve()
    {
        var selected = GetOption();
        
        switch (selected)
        {
            case PlayMenuOptions.TextToMorse:
                FallingMode fallingMode = new(FallingModeType.ToMorse);
                fallingMode.Run();
                break;
            case PlayMenuOptions.MorseToText:
                fallingMode = new(FallingModeType.FromMorse);
                fallingMode.Run();
                break;
            case PlayMenuOptions.SoundToText:
                SoundToTextMode soundToTextMode = new();
                soundToTextMode.Run();
                break;
            case PlayMenuOptions.Back:
                return false;
        }

        return true;
    }
}