using System.Linq.Expressions;

namespace Simulator;
public class Orc : Creature
{
    public override char Symbol { get; } = 'O';
    static int huntCounter = 0;
    private int rage = 1;
    public int Rage
    {
        get { return rage; }
        init { rage = Validator.Limiter(value, 0, 10); }
    }
    
    public override int Power
    {
        get { return (7 * Level) + (3 * Rage); }
    }

    public override string Info => $"{Name} [{Level}][{Rage}]";

    public Orc(string name, int level = 1, int rage = 1) : base(name, level)
    {
        Rage = rage;
    }

    public override int Hp
    {
        get { return (7 * Level) + (3 * Rage); }
    }
    public override int Resistance
    {
        get { return (7 * Level) + (3 * Rage); }
    }

    public Orc() : base("Unknown Orc", 1) => Rage = rage;

    public void Hunt()
    {
        huntCounter += 1;
        if (rage < 10 && huntCounter % 2 == 0) rage++;
    }

    public override string Greeting()
    {
        return $"Hi, I'm {Name}, my level is {Level}, my rage is {Rage}.";
    }
}
