namespace pv178_project.Utils;

public static class MathUtils
{
    public static int Modulo(int number, int divider)
    {
        return (number % divider + divider) % divider;
    }
}