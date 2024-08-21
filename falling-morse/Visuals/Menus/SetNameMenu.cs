using pv178_project.Models;
using pv178_project.Utils;

namespace pv178_project.Visuals.Menus;

public class SetNameMenu
{
    private const char EscapeKeyCode = '\u001b';
    private string Heading => "Select name";
    private string Description => $"Current name: {Settings.Name}";
    
    public void Resolve()
    {
        Console.Clear();
        Console.WriteLine(Heading.Underline());
        Console.WriteLine();
        Console.WriteLine(Description);
        Console.WriteLine("Write your name (or leave empty to go back to main menu) and press ENTER");
        string name = Console.ReadLine() ?? "";
        if (name.Equals("") || name.Contains(EscapeKeyCode))
        {
            return;
        }
        
        Settings.Name = name;
        Settings.SaveName();
    }
}