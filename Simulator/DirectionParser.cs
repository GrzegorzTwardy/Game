namespace Simulator;

public static class DirectionParser
{
    public static List<Direction> Parse(string d)
    {
        int validSize = 0;

        foreach (char c in d.ToLower())
        {
            if (c == 'r' || c == 'l' || c == 'u' || c == 'd')
                validSize++;
        }
        
        var result = new List<Direction>();

        int i = 0;

        foreach (char c in d.ToLower())
        {
            switch (c)
            {
                case 'r':
                    result.Add(Direction.Right);
                    i++;
                    break;
                case 'l':
                    result.Add(Direction.Left);
                    i++;
                    break;
                case 'u':
                    result.Add(Direction.Up);
                    i++;
                    break;
                case 'd':
                    result.Add(Direction.Down);
                    i++;
                    break;
            }
        }
        return result;
    }
}
