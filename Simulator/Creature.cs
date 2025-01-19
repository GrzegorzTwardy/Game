using Simulator.Maps;
namespace Simulator;

public abstract class Creature : IMappable
{
    public virtual char Symbol { get; }
    public Map? Map { get; private set; }
    public Point Position { get; private set; }

    private string? name;
    private int level = 1;

    public string Name
    {
        get { return name ?? "Unknown"; }
        init
        {
            name = Validator.Shortener(value, 3, 25, '#');
        }
    }

    public int Level
    {
        get { return level; }
        set
        {
            var tmp = value;

            if (tmp < 1)
                level = 1;
            else if (tmp > 10)
                level = 10;
            else
                level = tmp;
        }
    }

    public abstract int Power { get; }
    public abstract int Resistance { get; }
    public abstract int Hp { get; }

    public abstract string Info { get; }

    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    public Creature() {}

    public void InitMapandPosition(Map map, Point position)
    {
        Map = map;
        Position = position;
        map.Add(this, position);
    }
    public override string ToString()
    {
        return GetType().Name + ":  " + Info;
    }

    public abstract string Greeting();

    public void Upgrade()
    {
        if (level < 10)
            level++;
        else
            Console.WriteLine($"Cannot upgrade {Name}, it's level is 10 (max).");
    }

    public void Go(Direction direction)
    {
        if (Map == null)
            throw new ArgumentNullException(nameof(Map));

        Point nextPosition = Map.Next(Position, direction);

        Map.Move(this, Position, nextPosition);
        Position = nextPosition;
    }
}