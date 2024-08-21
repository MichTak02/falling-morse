using pv178_project.Utils;

namespace pv178_project.Models;

public class ScreenElement<T>(Position position, string label, T value)
{
    public Position Position { get; } = position;
    public string Label { get; } = label;
    public T Value { get; set; } = value;

    public void WriteOut()
    {
        ConsoleUtils.WriteAtPosition($"{Label}: {Value}", Position);
    }
}