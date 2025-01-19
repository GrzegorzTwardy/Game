using System;

namespace Simulator;

public class Mob : Creature
{
    public override char Symbol { get; } = 'G';
    private int _hp;
    public override int Power
    {
        get { return 5 * Level; }
        set { }
    }
    public override int Resistance
    {
        get { return 4 * Level; }
        set { }
    }
    public override int Hp
    {
        get { return _hp; }
        set { _hp = value; }
    }

    public override string Info => $"{Name} [{Level}]";

    public Mob(string name, int level = 1) : base(name, level)
    {
        _hp = 50 * level; // Inicjalizacja HP na podstawie poziomu
    }

    public Mob() : base("Unknown Goblin", 1) { }

    public override string Greeting()
    {
        return $"Hi, I'm {Name}, my level is {Level}.";
    }
}
