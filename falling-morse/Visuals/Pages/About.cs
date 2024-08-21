using pv178_project.Utils;

namespace pv178_project.Visuals.Pages;

public class About
{
    public void Show()
    {
        Console.Clear();
        Console.WriteLine("About".Underline());
        Console.WriteLine();
        Console.WriteLine("This is a simple console game for practising morse code");
        Console.WriteLine();
        
        Console.WriteLine("Gememodes:".Underline('.'));
        Console.WriteLine();
        Console.WriteLine("Falling morse (morse to text)");
        Console.WriteLine("Try to translate falling morse words and type them before they hit the ground".BulletPoint());
        Console.WriteLine("Press ESC while playing to exit".BulletPoint());
        
        Console.WriteLine("Falling text (text to morse)");
        Console.WriteLine("Try to write falling words in morse code before they hit the ground".BulletPoint());
        Console.WriteLine("For letter delimiter use '/' or '|'".BulletPoint());
        Console.WriteLine("Word must end with delimiter".BulletPoint());
        Console.WriteLine("Press ESC while playing to exit".BulletPoint());
        
        Console.WriteLine("Playing sounds (sound to text)");
        Console.WriteLine("Listen to morse code, translate it and type it".BulletPoint());
        Console.WriteLine("Press ESC while playing to exit".BulletPoint());

        Console.WriteLine();
        Console.WriteLine("Made by Michal Takač in spring 2024 as a project to PV178 course at FI MUNI");
        Console.WriteLine();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}