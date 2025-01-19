using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Simulator;

public class Troll : Creature
{
    public override char Symbol { get; } = 'T';
    public override int Power
    {
        get { return 5 * Level; }
    }
    public override int Resistance
    {
        get { return 5 * Level; }
    }
    public override int Hp
    {
        get { return 7 * Level; }
    }
    public override string Info => $"{Name} [{Level}]";

    public Troll(string name, int level = 1) : base(name, level) { }
    public Troll() : base("Unknown Troll", 1) { }

    public override string Greeting()
    {
        return $"Hi, I'm {Name}, my level is {Level}.";
    }
}
