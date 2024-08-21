namespace pv178_project;

public class KeypressSender
{
    public event EventHandler<ConsoleKeyInfo>? KeyPressed;

    public void Loop(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            var key = Console.ReadKey(true);
            OnKeyPressed(key);
        }
    }

    protected virtual void OnKeyPressed(ConsoleKeyInfo key)
    {
        KeyPressed?.Invoke(this, key);
    }
}