using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

public class Ent : Creature
{
    public override char Symbol { get; } = 'E';
    public override int Power
    {
        get { return 3 * Level; }
    }
    public override int Resistance
    {
        get { return 6 * Level; }
    }
    public override int Hp
    {
        get { return 10 * Level; }
    }

    public override string Info => $"{Name} [{Level}]";

    public Ent(string name, int level = 1) : base(name, level) { }
    public Ent() : base("Unknown Goblin", 1) { }

    public override string Greeting()
    {
        return $"Hi, I'm {Name}, my level is {Level}.";
    }
}
