namespace Simulator;

public class Player : Creature
{
    public override char Symbol { get; } = 'P';
    private int damage;
    private int resistance;

    public int Damage
    {
        get { return damage; }
        init { damage = Validator.Limiter(value, 0, 10); }
    }
    public int Resistance
    {
        get { return resistance; }
        init { resistance = Validator.Limiter(value, 0, 10); }
    }

    public override int Power
    {
        get { return (8 * Level) + (2 * damage); }
    }

    public override string Info => $"{Name} [{Level}][{Power}]";

    public Player(string name, int level = 1, int damage = 1, int resistance = 1) : base(name, level)
    {
        Damage = damage;
        Resistance = resistance;
    }

    public Player() : base("Unknown Player", 1) => damage = 6;

    public override string Greeting()
    {
        return $"Hi, I'm {Name}, my level is {Level}.";
    }
}
