using pv178_project.Models;
using pv178_project.Visuals.Menus;

namespace pv178_project;

class Program
{
    static void Main(string[] args)
    {
        // Seed.Run();
        // Console.WriteLine("Seeding completed");
        // Console.ReadKey();
        // return;
        
        Console.CursorVisible = false;
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;
        Settings.LoadName();
        
        MainMenu menu = new MainMenu();
        bool menuResult;

        do
        {
            menuResult = menu.Resolve();
        } while (menuResult);
    }   
}