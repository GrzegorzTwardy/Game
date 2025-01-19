using System;

namespace Simulator;

public class Elf : Creature
{
    public override char Symbol { get; } = 'E';
    static int sngCounter = 0;
    private int agility;
    public int Agility
    {
        get { return agility; }
        init { agility = Validator.Limiter(value, 0, 10); }
    }
    public override int Power
    {
        get { return (8 * Level) + (2 * Agility); }
    }

    public override int Resistance
    {
        get { return 7 * Level; }
    }

    public override int Hp
    {
        get { return 7 * Level; }
    }

    public override string Info => $"{Name} [{Level}][{Agility}]";

    public Elf(string name, int level = 1, int agility = 1) : base(name, level)
    {
        Agility = agility;
    }

    public Elf() : base("Unknown Elf", 10) => Agility = 6;

    public void Sing()
    {
        sngCounter += 1;
        if (agility < 10 && sngCounter % 3 == 0) agility += 1;
    }

    public override string Greeting()
    {
        return $"Hi, I'm {Name}, my level is {Level}, my agility is {Agility}.";
    }
}
