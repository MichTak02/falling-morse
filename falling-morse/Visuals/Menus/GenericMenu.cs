using pv178_project.Utils;

namespace pv178_project.Visuals.Menus;

public abstract class GenericMenu<T> where T : struct, Enum, IConvertible
{
    protected abstract string Heading { get; }
    protected abstract string Description { get; }
    protected abstract Dictionary<T, string> Options { get; }
    protected bool DescriptionAtBottom { get; set; } = true;
    protected bool UnderlineHeading { get; set; } = true;
    
    private int _currentIndex = 0;
    private T CurrentItem => Options.Keys.ToList()[_currentIndex];

    public T GetOption()
    {
        ConsoleKeyInfo key;
        do
        { 
            Display();
            key = Console.ReadKey(true);
            int direction = key.Key == ConsoleKey.UpArrow ? -1 : key.Key == ConsoleKey.DownArrow ? 1 : 0; 
            _currentIndex = MathUtils.Modulo(_currentIndex + direction, Options.Count);
        } while (key.Key != ConsoleKey.Enter);

        return CurrentItem;
    }

    private void Display()
    {
        Console.Clear();
        
        Console.WriteLine(UnderlineHeading ? Heading.Underline() : Heading);
        Console.WriteLine();

        if (!DescriptionAtBottom)
        {
            Console.WriteLine(Description);
            Console.WriteLine();
        }

        var enumValues = Enum.GetValues<T>();
        foreach (T option in enumValues)
        {
            if (option.Equals(enumValues[^1])) // Back option spacing
            {
                Console.WriteLine();
            }
            
            if (option.Equals(CurrentItem))
            {
                ConsoleUtils.WriteInverted(Options[option].MenuItem());
                continue;
            }
            
            Console.WriteLine(Options[option].MenuItem());
        }

        if (DescriptionAtBottom)
        {
            Console.WriteLine();
            Console.WriteLine(Description);
        }
    }
    
    public abstract bool Resolve();
}