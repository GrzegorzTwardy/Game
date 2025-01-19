using System;
using System.Runtime.CompilerServices;

namespace Simulator;

public class Player : Creature
{
    public override char Symbol { get; } = 'P';
    private int exp=0;

    public override int Resistance
    {
        get { return 8*Level; }
        set { }
    }

    public override int Power
    {
        get { return 8 * Level; }
        set { }
    }
    public override int Hp
    {
        get { return 7 * Level; }
        set { }
    }

    public int Exp
    {
        get { return exp; }
        set 
        { 
            exp = value;
            CheckLevelUp();
        }
    }

    public override string Info => $"{Name} [{Level}][{Power}]";

    public Player(string name, int level = 1) : base(name, level) { }
    public Player() : base("Unknown Player", 1) { }

    public override string Greeting()
    {
        return $"Hi, I'm {Name}, my level is {Level}.";
    }

    private void CheckLevelUp()
    {
        while (exp >= 100) 
        {
            exp -= 100; 
            Level++;   
            Console.WriteLine($"Congratulations! {Name} has leveled up to Level {Level}!");
        }
    }
}
