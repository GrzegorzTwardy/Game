using System;
using System.Runtime.CompilerServices;

namespace Simulator;

public class Player : Creature
{
    public override char Symbol { get; } = 'P';
    private int resistance;

    public override int Resistance
    {
        get { return resistance; }
    }

    public override int Power
    {
        get { return 8 * Level; }
    }
    public override int Hp
    {
        get { return 7 * Level; }
    }

    public override string Info => $"{Name} [{Level}][{Power}]";

    public Player(string name, int level = 1) : base(name, level) { }
    public Player() : base("Unknown Player", 1) { }

    public override string Greeting()
    {
        return $"Hi, I'm {Name}, my level is {Level}.";
    }
}
