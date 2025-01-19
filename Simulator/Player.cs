using System;
using System.Runtime.CompilerServices;

namespace Simulator;

public class Player : Creature
{
    public override char Symbol { get; } = 'P';
    private int exp=0;
    private int power;
    private int resistance;
    private int _hp;

    public override int Resistance
    {
        get { return resistance; }
        set { resistance = value; }
    }

    public override int Power
    {
        get { return power; }
        set { power = value; }
    }
    public override int Hp
    {
        get { return _hp; }
        set { _hp = value; }
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

    public Player(string name, int level = 1) : base(name, level) 
    {
        _hp = 7 * level;
        power = 8 * level;
        resistance = 8 * level;
    }
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
            resistance += 8;
            power += 8;
            _hp += 7;
            Console.WriteLine($"Congratulations! {Name} has leveled up to Level {Level}!");
        }
    }
}
