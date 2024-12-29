using Simulator.Maps;

namespace Simulator;

public class Birds : Animals
{
    public override char Symbol { get; } = 'b';
    public bool CanFly { get; set; } = true;

    public override string Info
    {
        get
        {
            if (CanFly)
                return $"{Description} (fly+) <{Size}>";
            else
                return $"{Description} (fly-) <{Size}>";
        }
    }
    public Birds(string description, bool canFly = true, uint size = 3) : base(description, size)
    {
        CanFly = canFly;
        if (CanFly) Symbol = 'B';
    }

    public override void Go(Direction direction)
    {
        if (Map == null)
            throw new ArgumentNullException(nameof(Map));

        var newPosition = new Point();

        if (CanFly)
        {
            if (Map is BigBounceMap bbm)
            {
                newPosition = bbm.NextBounce(Position, direction);
            }
            else
            {
                Point tmp = Map.Next(Position, direction);
                newPosition = Map.Next(tmp, direction);
            }
        }
        else newPosition = Map.NextDiagonal(Position, direction);

        Map.Move(this, Position, newPosition);
        Position = newPosition;
    }
}