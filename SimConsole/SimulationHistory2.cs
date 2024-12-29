using SimConsole;
using Simulator.Maps;
using Simulator;

public class SimulationHistory2
{
    // Klasa do przechowywania map i danych o ruchach w poszczególnych turach
    public class TurnData
    {
        public Map Map { get; set; }
        public int TurnNumber { get; set; }
        public string MappableName { get; set; }
        public string MappableMove { get; set; }

        public TurnData(int turnNumber, Map map, string mName, string mDir)
        {
            TurnNumber = turnNumber;
            Map = map;
            MappableName = mName;
            MappableMove = mDir;
        }

        public TurnData() { }
    }


    private int _turnNumber = 0;
    public Simulation Sim { get; }
    private List<TurnData> _allTurns = new List<TurnData>();

    public SimulationHistory2(Simulation sim)
    {
        Sim = sim;
        MapVisualizer mapVisualizer = new(sim.Map);
        Console.WriteLine("Starting Positions: ");
        mapVisualizer.Draw();
        Console.WriteLine("Press any key to continue...");
        //Console.ReadLine();

        while (!sim.Finished)
        {
            Console.WriteLine($"Turn {_turnNumber+1}");
            Console.WriteLine($"<{sim.CurrentMappable.GetType().Name}>{sim.CurrentMappable.Position}: {sim.CurrentMoveName} ");
            sim.Turn();
            SaveTurn();
            //Console.WriteLine($" DATA : {_allTurns[_turnNumber-1].TurnNumber}, {_allTurns[_turnNumber - 1].MappableName}, {_allTurns[_turnNumber - 1].MappableMove}");
            mapVisualizer.Draw();
            Console.WriteLine("Press any key to continue...");
            //Console.ReadLine();
        }
        TurnInfo(5);
        TurnInfo(10);
        TurnInfo(15);
        TurnInfo(20);

        var mappableOn5 = GetTurnsMappableInfo(5);

        Console.WriteLine($"\nMappable [{mappableOn5.Item1}] on turn 5 went [{mappableOn5.Item2}]...");
    }
    public void SaveTurn()
    {
        string copiedName = Sim.LastMappable is Creature c ? c.Name :
                            Sim.LastMappable is Animals a ? a.Description :
                            "Unknown";

        string? copiedMove = Sim.LastMove;

        var copiedMap = Sim.Map.Clone();

        _turnNumber++;
        var turnData = new TurnData(
            _turnNumber,
            copiedMap,
            copiedName,
            copiedMove
        );

        _allTurns.Add(turnData);
    }

    public void TurnInfo(int turnNumber)
    {
        if (turnNumber < 0 || turnNumber > _allTurns.Count)
            throw new IndexOutOfRangeException("Wrong turn number.");

        var turn = _allTurns[turnNumber - 1];
        MapVisualizer mapVisualizer = new(turn.Map);
        Console.WriteLine($"============== TURN {turn.TurnNumber} ===============");
        if (turn.MappableMove != null)
            Console.WriteLine($"{turn.MappableName} goes {turn.MappableMove}...");
        else Console.WriteLine($"No one moves - end of simulation.");
        mapVisualizer.Draw();
    }

    public (string MappableName, string MappableMove) GetTurnsMappableInfo(int turnNumber)
    {
        if (turnNumber < 0 || turnNumber > _allTurns.Count)
            throw new IndexOutOfRangeException("Wrong turn number.");

        var turn = _allTurns[turnNumber - 1];
        return (turn.MappableName, turn.MappableMove);
    }
}