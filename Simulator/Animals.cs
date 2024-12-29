using Simulator.Maps;
using System.Text.RegularExpressions;

namespace Simulator;

public class Animals : IMappable
{
    public virtual char Symbol { get; } = 'A';
    public Map? Map { get; private set; }
    private string description = "Unknown";
    public Point Position { get; set; }
    public string Description
    {
        get => description;
        init => description = Validator.Shortener(value, 3, 15, '#');
    }
    public uint Size { get; set; }

    public Animals(string description, uint size = 3)
    {
        Description = description;
        Size = size;
    }

    public virtual string Info => $"{Description} <{Size}>";

    public void InitMapandPosition(Map map, Point position)
    {
        Map = map;
        Position = position;
        map.Add(this, position);
    }

    public virtual void Go(Direction direction)
    {
        if (Map == null)
            throw new ArgumentNullException("This mappable hasn't been assigned to any map.");

        Point nextPosition = Map.Next(Position, direction);
        Map.Move(this, Position, nextPosition);
        Position = nextPosition;
    }

    public override string ToString()
    {
        return GetType().Name + ": " + Info;
    }
}
