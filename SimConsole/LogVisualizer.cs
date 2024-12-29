using SimConsole;
using Simulator;
using Simulator.Maps;

internal class LogVisualizer
{
    SimulationHistory Log { get; }
    public LogVisualizer(SimulationHistory log)
    {
        // implement
        Log = log ?? throw new ArgumentNullException(nameof(log));
    }

    public void Draw(int turnIndex)
    {
        if (turnIndex != 0)
            Console.WriteLine($" == Log: Turn {turnIndex} == ");
        else
            Console.WriteLine($" == Starting Positions == ");
        DrawBorder(Box.TopLeft, Box.TopMid, Box.TopRight);

        for (int y = Log.SizeY - 1; y > 0; y--)
        {
            DrawRow(turnIndex, Box.Vertical, Box.Vertical, y);
            DrawBorder(Box.MidLeft, Box.Cross, Box.MidRight);
        }
        DrawRow(turnIndex, Box.Vertical, Box.Vertical, 0);
        DrawBorder(Box.BottomLeft, Box.BottomMid, Box.BottomRight);
        Console.WriteLine();
    }

    private void DrawBorder(char leftCorner, char middle, char rightCorner)
    {
        Console.Write(leftCorner);
        for (int x = 0; x < Log.SizeX - 1; x++)
        {
            Console.Write(Box.Horizontal);
            Console.Write(middle);
        }
        Console.Write(Box.Horizontal);
        Console.WriteLine(rightCorner);
    }

    private void DrawRow(int turnIndex, char verticalBorder, char verticalBorderEnd, int row)
    {
        var CurrentLog = Log.TurnLogs[turnIndex];
        Console.Write(verticalBorder);
        for (int x = 0; x < Log.SizeX - 1; x++)
        {
            Console.Write(CurrentLog.Symbols[new Point(x, row)]);
            Console.Write(verticalBorder);
        }
        Console.Write(CurrentLog.Symbols[new Point(Log.SizeX - 1, row)]);
        Console.WriteLine(verticalBorderEnd);
    }
}