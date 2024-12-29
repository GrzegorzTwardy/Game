namespace Simulator;

public class Rectangle
{
    public int X1 { get; }
    public int Y1 { get; }
    public int X2 { get; }
    public int Y2 { get; }

    public Rectangle(int x1, int y1, int x2, int y2)
    {
        if (x1 == x2 || y1 == y2)
            throw new ArgumentException("Punkty są współliniowe, nie można utworzyć prostokąta.");

        if (x1 > x2)
        {
            if (y1 > y2)
            {
                (X1, Y1) = (x2, y2);
                (X2, Y2) = (x1, y1);
            }
            else
            {
                (X1, Y1) = (x2, y1);
                (X2, Y2) = (x1, y2);
            }
        }
        else
        {
            if (y1 > y2)
            {
                (X1, Y1) = (x1, y2);
                (X2, Y2) = (x2, y1);
            }
            else
            {
                (X1, Y1) = (x1, y1);
                (X2, Y2) = (x2, y2);
            }
        }
    }

    public Rectangle(Point p1, Point p2) : this(p1.X, p1.Y, p2.X, p2.Y) {}

    public bool Contains(Point point)
    {
        return point.X >= X1 && point.X <= X2 && point.Y >= Y1 && point.Y <= Y2;
    }

    public override string ToString()
    {
        return $"({X1}, {Y1}):({X2}, {Y2})";
    }
}
