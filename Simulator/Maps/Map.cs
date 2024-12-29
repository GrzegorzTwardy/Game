namespace Simulator.Maps;

/// <summary>
/// Map of points.
/// </summary>
public abstract class Map
{
    private Dictionary<Point, List<IMappable>> _fields;
    public Dictionary<Point, List<IMappable>> Fields { get => _fields; set => _fields = value; }
    private readonly Rectangle _map;
    protected Map(int sizeX, int sizeY)
    {
        if(sizeX < 5)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeX), "Too narrow");
        }
        if(sizeY < 5)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeY), "Too short");
        }

        SizeX = sizeX;
        SizeY = sizeY;
        _map = new Rectangle(0, 0, SizeX-1, SizeY-1);

        Fields = new Dictionary<Point, List<IMappable>>();
    }

    public int SizeX { get; }
    public int SizeY { get; }

    public virtual bool Exist(Point p) => _map.Contains(p);

    public abstract Point Next(Point p, Direction d);

    public abstract Point NextDiagonal(Point p, Direction d);


    public void Add(IMappable mappable, Point p)
    {
        Fields.TryGetValue(p, out var list);
        if (list != null)
            list.Add(mappable);
        else
            Fields[p] = new List<IMappable> { mappable };
    }

    public void Remove(IMappable mappable, Point p)
    {
        if (Fields.TryGetValue(p, out var list))
            list.Remove(mappable);
    }

    public List<IMappable> At(Point p)
    {
        if (Fields.TryGetValue(p, out var list))
        {
            return list;
        }
        else
        {
            return new List<IMappable>();
        }
    }

    public void At(int x, int y)
    {
        var point = new Point(x, y);
        At(point);
    }

    public void Move(IMappable mappable, Point startPoint, Point endPoint)
    {
        Remove(mappable, startPoint);
        Add(mappable, endPoint);
    }
    public abstract Map Clone();


    //public abstract Point NextDiagonal(Point p, Direction d);

    //public abstract void Add(IMappable mappable, Point p);

    //public abstract void Remove(IMappable mappable, Point p);

    //public abstract List<IMappable> At(Point p);

    //public abstract void At(int x, int y);

    //public abstract void Move(IMappable mappable, Point startPoint, Point endPoint);
}