using Simulator;
using Simulator.Maps;
namespace SimConsole;

public class MapVisualizer
{
    public Map Map { get; }
    public MapVisualizer(Map map)
    {
        Map = map ?? throw new NullReferenceException(nameof(map));
    }

    public void Draw()
    {
        DrawBorder(Box.TopLeft, Box.TopMid, Box.TopRight);

        for (int y = Map.SizeY - 1; y > 0; y--)
        {
            DrawRow(Box.Vertical, Box.Vertical, y);
            DrawBorder(Box.MidLeft, Box.Cross, Box.MidRight);
        }
        DrawRow(Box.Vertical, Box.Vertical, 0);
        DrawBorder(Box.BottomLeft, Box.BottomMid, Box.BottomRight);
    }

    private void DrawBorder(char leftCorner, char middle, char rightCorner)
    {
        Console.Write(leftCorner);
        for (int x = 0; x < Map.SizeX-1; x++)
        {
            Console.Write(Box.Horizontal);
            Console.Write(middle);
        }
        Console.Write(Box.Horizontal);
        Console.WriteLine(rightCorner);
    }

    private void DrawRow(char verticalBorder, char verticalBorderEnd, int row)
    {
        Console.Write(verticalBorder);
        for (int x = 0; x < Map.SizeX-1; x++)
        {
            Console.Write(DrawSymbol(x, row));
            Console.Write(verticalBorder);
        }
        Console.Write(DrawSymbol(Map.SizeX - 1, row));
        Console.WriteLine(verticalBorderEnd);
    }

    private char DrawSymbol(int x, int y)
    {
        var mappables = Map.At(new Point(x, y));
        if (mappables == null || mappables.Count == 0)
            return ' ';

        if (mappables.Count > 1)
            return 'X';

        return mappables[0].Symbol;
    }
}
