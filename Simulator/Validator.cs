namespace Simulator;

public static class Validator
{
    public static int Limiter(int value, int min, int max)
    {
        if (value > max)
            value = max;
        else if (value < 0)
            value = min;

        return value;
    }

    public static string Shortener(string value, int min, int max, char placeholder)
    {
        value = value.Trim();

        if (value.Length > 0)
        {
            if (value.Length > max)
            {
                value = value[..max].TrimEnd();
            }
            if (value.Length < min)
            {
                value = value.PadRight(min, placeholder);
            }
            value = char.ToUpper(value[0]) + value.Substring(1);
        }

        return value;
    }
}
