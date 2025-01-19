using System;

namespace Simulator;

public class Goblin : Creature
{
    public override char Symbol { get; } = 'G';
    public override int Power
    {
        get { return 5 * Level; }
    }
    public override int Resistance
    {
        get { return 4 * Level; }
    }
    public override int Hp
    {
        get { return 5 * Level; }
    }

    public override string Info => $"{Name} [{Level}]";

    public Goblin(string name, int level = 1) : base(name, level) { }
    public Goblin() : base("Unknown Goblin", 1) { }

    public override string Greeting()
    {
        return $"Hi, I'm {Name}, my level is {Level}.";
    }
}
