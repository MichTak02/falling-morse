using pv178_project.Utils;

namespace pv178_project.Models;

public class FallingWord(string text, Position position, double speed)
{
    private const int BaseSpeedDelay = 3000;
    private const double SpeedNormalization = 24.0;
    public string Text { get; } = text;
    public string MorseText => Text.ToMorse();
    public Position Position { get; } = position;
    private double Speed { get; set; } = speed;
    private double NormalizedSpeed => Speed * (Console.WindowHeight / SpeedNormalization);
    private DateTime _lastUpdated = new DateTime(0);
    private TimeSpan NotUpdatedDiff => DateTime.Now - _lastUpdated;
    private bool ShouldMove => NotUpdatedDiff.TotalMilliseconds > BaseSpeedDelay / NormalizedSpeed;

    public void Shift()
    {
        if (!ShouldMove)
        {
            return;
        }

        _lastUpdated = DateTime.Now;
        Position.Y += 1;
    }

    public bool WouldOverflow(int borderY)
    {
        return ShouldMove && Position.Y + 1 >= borderY;
    }
    
    
    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (obj.GetType() != typeof(FallingWord))
        {
            return false;
        }
        
        FallingWord other = (FallingWord) obj;

        return Text.ToUpper().Equals(other.Text.ToUpper());
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Text.ToUpper());
    }
}