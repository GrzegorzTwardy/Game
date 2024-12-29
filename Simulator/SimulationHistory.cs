using Simulator.Maps;

namespace Simulator;
public class SimulationHistory
{
    private Simulation _simulation { get; }
    public int SizeX { get; }
    public int SizeY { get; }
    public List<SimulationTurnLog> TurnLogs { get; } = [];
    // store starting positions at index 0
    public bool startPos = true;

    public SimulationHistory(Simulation simulation)
    {
        _simulation = simulation ??
            throw new ArgumentNullException(nameof(simulation));
        SizeX = _simulation.Map.SizeX;
        SizeY = _simulation.Map.SizeY;
        Run();
    }

    private void Run()
    {
        //var turnLog = new SimulationTurnLog(mappableName, move, Symbols);
        //TurnLogs.Add(turnLog);
        // implement
        var startSymbols = GetSymbols();
        TurnLogs.Add(new SimulationTurnLog(null, "No moves have been made", startSymbols));
        while (!_simulation.Finished)
        {
            //string mappableName = _simulation.CurrentMappable switch
            //{
            //    Creature c => c.Name,
            //    Animals a => a.Description,
            //    _ => "Unknown"
            //};

            //string move = _simulation.CurrentMoveName;

            IMappable mappable = _simulation.CurrentMappable;
            string move = _simulation.CurrentMoveName;

            _simulation.Turn();
            var symbols = GetSymbols();

            TurnLogs.Add(new SimulationTurnLog(mappable, move, symbols));
        }

        //testHistory();
    }

    public Dictionary<Point, char> GetSymbols()
    {
        Dictionary<Point, char> Symbols = new();
        for (int y = SizeY-1; y >= 0; y--)
        //for (int y = 0; y < SizeY; y++)
        {
            for (int x = 0; x < SizeX; x++)
            {
                var currentPoint = new Point(x, y);
                var mappablesAtPoint = _simulation.Map.At(currentPoint);
                if (mappablesAtPoint.Count > 1)
                {
                    Symbols.Add(currentPoint, 'X');
                }
                else if (mappablesAtPoint.Count == 1)
                {
                    Symbols.Add(currentPoint, _simulation.Map.At(currentPoint)[0].Symbol);
                }
                else
                {
                    Symbols.Add(currentPoint, ' ');
                }
            }
        }
        return Symbols;
    }

    /// <summary>
    /// State of map after single simulation turn.
    /// </summary>
    public class SimulationTurnLog
    {
        /// <summary>
        /// Text representastion of moving object in this turn.
        /// CurrentMappable.ToString()
        /// </summary>
        //public required string Mappable { get; init; }
        //public string Mappable { get; init; }
        public IMappable? Mappable { get; init; }
        /// <summary>
        /// Text representation of move in this turn.
        /// CurrentMoveName.ToString();
        /// </summary>
        //public required string Move { get; init; }
        public string Move { get; init; }
        /// <summary>
        /// Dictionary of IMappable.Symbol on the map in this turn.
        /// </summary>
        //public required Dictionary<Point, char> Symbols { get; init; }
        public Dictionary<Point, char> Symbols { get; init; }


        public SimulationTurnLog(IMappable? mappable, string? move, Dictionary<Point, char> symbols)
        {
            Mappable = mappable ?? null;
            Move = move ?? throw new ArgumentNullException(nameof(move));
            Symbols = symbols ?? throw new ArgumentNullException(nameof(symbols));
        }
    }

    private void testHistory()
    {
        for (int i = 0; i < TurnLogs.Count; i++)
        {
            Console.WriteLine($"===== TURN {i} ======");
            Console.WriteLine(TurnLogs[i].Mappable);
            Console.WriteLine(TurnLogs[i].Move);
            Console.WriteLine(TurnLogs[i].Symbols[new Point(SizeX-1, 1)]);
            Console.WriteLine();
        }
    }
}