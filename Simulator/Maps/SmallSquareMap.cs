namespace Simulator.Maps;

public class SmallSquareMap : SmallMap
{
    public int Size { get; }

    public SmallSquareMap(int size) : base(size, size)
    {
        Size = size;
    }

    //public override bool Exist(Point p)
    //{
    //    return p.X >= 0 && p.X <= SizeX-1 && p.Y >= 0 && p.Y <= SizeY-1;
    //}

    public override Point Next(Point p, Direction d)
    {
        Point nextPoint = p.Next(d);
        return Exist(nextPoint) ? nextPoint : default;
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        Point nextDiagonalPoint = p.NextDiagonal(d);
        return Exist(nextDiagonalPoint) ? nextDiagonalPoint : default;
    }

    public override Map Clone()
    {
        var newMap = (SmallMap)Activator.CreateInstance(this.GetType(), SizeX, SizeY)!;
        foreach (var field in Fields)
        {
            newMap.Fields[field.Key] = new List<IMappable>(field.Value);
        }
        return newMap;
    }
}
