using Simulator.Maps;
using System.ComponentModel;

namespace Simulator;

internal class Program
{
    static void Main(string[] args)
    {
        BigBounceMap map = new(8, 6);
        List<IMappable> mappables = [new Player("GRACZ"), new Mob("Jacek")];
        List<Point> points = [new(1, 2), new(3, 5)];

        Game game = new(map, mappables, points);
        game.Run();
    }
}
