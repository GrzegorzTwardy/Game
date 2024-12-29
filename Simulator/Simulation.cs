using Simulator.Maps;
using Simulator;
using System.Xml.Linq;

public class Simulation
{
    private readonly List<Direction> _parsedMoves;
    private int _turnIndex = 0;

    public Map Map { get; }
    public List<IMappable> Mappables { get; }
    public List<Point> Positions { get; }
    public string Moves { get; }
    public bool Finished = false;

    // Pobiera aktualny obiekt do poruszania się na mapie
    public IMappable CurrentMappable => Mappables[_turnIndex % Mappables.Count];
    public IMappable LastMappable { get; set; }

    // Pobiera nazwę ruchu dla aktualnej tury
    public string? CurrentMoveName =>
        _parsedMoves.Count > 0 && _turnIndex < _parsedMoves.Count
            ? _parsedMoves[_turnIndex].ToString().ToLower()
            : null;

    public string? LastMove { get; set; }

    public Simulation(Map map, List<IMappable> mappables, List<Point> positions, string moves)
    {
        if (mappables.Count == 0)
            throw new ArgumentException("IMappables' list is empty");
        if (mappables.Count != positions.Count)
            throw new ArgumentException("Number of mappables differs from number of starting positions");
        if (map == null)
            throw new ArgumentException("There is no map selected");

        Map = map;
        Mappables = mappables;
        Positions = positions;
        _parsedMoves = DirectionParser.Parse(moves);
        _turnIndex = 0;


        for (int i = 0; i < mappables.Count; i++)
        {
            Mappables[i].InitMapandPosition(Map, Positions[i]);
        }
    }

    public void Turn()
    {
        SaveMovedInfo(CurrentMappable, CurrentMoveName);

        if (Finished)
            throw new InvalidOperationException("Simulation is already finished");

        if (_parsedMoves.Count == 0 || _turnIndex >= _parsedMoves.Count)
        {
            Finished = true;
            return;
        }
        CurrentMappable.Go(_parsedMoves[_turnIndex]);

        _turnIndex++;

        if (_turnIndex >= _parsedMoves.Count)
            Finished = true;
    }

    public void SaveMovedInfo(IMappable Cm, string Cd)
    {
        LastMappable = Cm;
        LastMove = Cd;
    }
}








//using Simulator.Maps;

//namespace Simulator;

//public class Simulation
//{
//    private readonly List<Direction> _parsedMoves;
//    private int _turnIndex = 0;
//    /// <summary>
//    /// Simulation's map.
//    /// </summary>
//    public Map Map { get; }

//    /// <summary>
//    /// IMappable moving on the map.
//    /// </summary>
//    public List<IMappable> Mappables { get; }

//    /// <summary>
//    /// Starting positions of mappables.
//    /// </summary>
//    public List<Point> Positions { get; }

//    /// <summary>
//    /// Cyclic list of mappables moves. 
//    /// Bad moves are ignored - use DirectionParser.
//    /// First move is for first mappable, second for second and so on.
//    /// When all mappables make moves, 
//    /// next move is again for first mappable and so on.
//    /// </summary>
//    public string Moves { get; }

//    /// <summary>
//    /// Has all moves been done?
//    /// </summary>
//    public bool Finished = false;

//    /// <summary>
//    /// Creature which will be moving current turn.
//    /// </summary>
//    public IMappable CurrentMappable => Mappables[_turnIndex % Mappables.Count];

//    /// <summary>
//    /// Lowercase name of direction which will be used in current turn.
//    /// </summary>
//    //public string CurrentMoveName => _parsedMoves[_turnIndex].ToString().ToLower();
//    public string CurrentMoveName =>
//    _parsedMoves.Count > 0 ? _parsedMoves[_turnIndex].ToString().ToLower() : null;

//    /// <summary>
//    /// Simulation constructor.
//    /// Throw errors:
//    /// if mappables' list is empty,
//    /// if number of mappables differs from 
//    /// number of starting positions.
//    /// </summary>
//    public Simulation(Map map, List<IMappable> mappables,
//        List<Point> positions, string moves)
//    {
//        if (mappables.Count == 0)
//            throw new ArgumentException("IMappables' list is empty");
//        if (mappables.Count != positions.Count)
//            throw new ArgumentException("Number of mappables differs from number of starting positions");
//        if (map == null)
//            throw new ArgumentException("There is no map selected");

//        Map = map;
//        Mappables = mappables;
//        Positions = positions;
//        _parsedMoves = DirectionParser.Parse(moves);
//        _turnIndex = 0;

//        //set up starting positions
//        for (int i = 0; i < mappables.Count; i++)
//        {
//            Mappables[i].InitMapandPosition(Map, Positions[i]);
//        }
//    }

//    /// <summary>
//    /// Makes one move of current mappable in current direction.
//    /// Throw error if simulation is finished.
//    /// </summary>
//    public void Turn()
//    {
//        if (Finished)
//            throw new InvalidOperationException("Simulation is already finished");

//        if (_parsedMoves.Count == 0 || _turnIndex >= _parsedMoves.Count)
//        {
//            Finished = true;
//            return;
//        }

//        CurrentMappable.Go(_parsedMoves[_turnIndex]);

//        if (++_turnIndex == _parsedMoves.Count)
//            Finished = true;
//    }
//}
