using System.Text;
using Simulator;
using Simulator.Maps;

namespace SimConsole;

internal class Program
{
    static void Lab7()
    {
        SmallSquareMap map = new(5);
        List<IMappable> creatures = [new Orc("Gorbag"), new Elf("Elandor")];
        List<Point> points = [new(2, 2), new(3, 1)];
        string moves = "dlrludl";

        Simulation simulation = new(map, creatures, points, moves);
        MapVisualizer mapVisualizer = new(simulation.Map);

        Console.WriteLine("Starting Positions: ");
        mapVisualizer.Draw();
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        while (!simulation.Finished)
        {
            Console.WriteLine($"<{simulation.CurrentMappable.GetType().Name}>{simulation.CurrentMappable.Position}: {simulation.CurrentMoveName} ");
            simulation.Turn();
            mapVisualizer.Draw();
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }

    static void AnimalsMap()
    {
        SmallTorusMap map = new(8, 6);
        List<IMappable> mappables = [new Orc("Gorbag"), new Elf("Elandor"), new Birds("orły", true), new Birds("strusie", false, 5), new Animals("króliki")];
        List<Point> points = [new(2, 2), new(3, 1), new(6, 4), new(7, 5), new(6, 0)];
        string moves = "dlrud uuddr uuuld";

        Simulation simulation = new(map, mappables, points, moves);
        MapVisualizer mapVisualizer = new(simulation.Map);

        Console.WriteLine("Starting Positions: ");
        mapVisualizer.Draw();
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        while (!simulation.Finished)
        {
            Console.WriteLine($"{simulation.CurrentMappable.Info} {simulation.CurrentMappable.Position}: {simulation.CurrentMoveName} ");
            simulation.Turn();
            mapVisualizer.Draw();
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }

    static void Lab9()
    {
        BigBounceMap map = new(8, 6);
        //BigTorusMap map = new(8, 6);
        List<IMappable> mappables = [new Orc("Gorbag"), new Elf("Elandor"), new Birds("orły", true), new Birds("strusie", false, 5), new Animals("króliki")];
        List<Point> points = [new(0, 2), new(3, 5), new(1, 5), new(7, 5), new(7, 1)];
        string moves = "lulur dllld dlull dluul";

        Simulation simulation = new(map, mappables, points, moves);
        SimulationHistory2 history = new(simulation);

    }

    static void Lab10()
    {
        BigBounceMap map = new(8, 6);
        List<IMappable> mappables = [new Orc("Gorbag"), new Elf("Elandor"), new Birds("orły", true), new Birds("strusie", false, 5), new Animals("króliki")];
        List<Point> points = [new(0, 2), new(3, 5), new(6, 3), new(7, 5), new(7, 1)];
        string moves = "lurur dluld dldll dldul";

        Simulation simulation = new(map, mappables, points, moves);
        SimulationHistory history = new(simulation);

        LogVisualizer logVisualizer = new(history);
        for (int i = 0; i < history.TurnLogs.Count; i++)
        {
            logVisualizer.Draw(i);
        }
        Console.WriteLine(mappables[0].ToString());
        Console.WriteLine(mappables[2].ToString());
        Console.WriteLine(mappables[4].ToString());
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Lab9();
    }
}
