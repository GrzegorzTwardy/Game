namespace Simulator.Maps;

public class BigBounceMap : BigMap
{
    public BigBounceMap(int sizeX, int sizeY) : base(sizeX, sizeY) { }

    public static Direction InvertedDirection(Direction d)
    {
        return d switch
        {
            Direction.Right => Direction.Left,
            Direction.Left => Direction.Right,
            Direction.Up => Direction.Down,
            Direction.Down => Direction.Up,
            _ => d
        };
    }

    public override Point Next(Point p, Direction d)
    {
        Point nextPoint = p.Next(d);
        
        if (Exist(nextPoint))
            return nextPoint;

        return p.Next(InvertedDirection(d));
    }

    // latajace ptaki
    public Point NextBounce(Point p, Direction d)
    {
        Point nextPoint = p.Next(d);

        if (Exist(nextPoint) && Exist(nextPoint.Next(d)))
        {
            return nextPoint.Next(d);
        }
        else if (Exist(nextPoint) && !Exist(nextPoint.Next(d)))
        {
            // idzie o i pole, ale od 2 sie odbije, czyli wraca w to samo miejsce
            return p;
        }
        else if (!Exist(nextPoint) && !Exist(nextPoint.Next(d)))
        {
            // odbij o 2 pole w przeciwnym kierunku
            return p.Next(InvertedDirection(d)).Next(InvertedDirection(d));
        }

        return p;
    }


    public override Point NextDiagonal(Point p, Direction d)
    {
        Point nextPoint = p.NextDiagonal(d);
        if (Exist(nextPoint))
            return nextPoint;

        switch (d)
        {
            case Direction.Up:
                if (p.Y != SizeY-1)
                    return new Point(p.X - 1, p.Y+1);
                if (p.X != SizeX-1)
                    return new Point(p.X + 1, p.Y - 1);
                return new Point(p.X - 1, p.Y - 1);
            case Direction.Left:
                if (p.Y != SizeY - 1)
                    return new Point(p.X + 1, p.Y + 1);
                if (p.X != 0)
                    return new Point(p.X - 1, p.Y - 1);
                return new Point(1, p.Y - 1); ;
            case Direction.Right:
                if (p.X != SizeX - 1)
                    return new Point(p.X + 1, p.Y + 1);
                if (p.Y != 0)
                    return new Point(p.X - 1, p.Y - 1);
                return new Point(p.X - 1, 1); ;
            case Direction.Down:
                if (p.X != 0)
                    return new Point(p.X - 1, p.Y + 1);
                if (p.Y != 0)
                    return new Point(p.X + 1, p.Y - 1);
                return new Point(1, 1); ;
        }
        throw new InvalidDataException("Invalid Direction Value.");
    }
}
