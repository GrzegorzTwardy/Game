namespace Simulator.Maps;

public interface IMappable
{
    char Symbol { get; }
    string Info { get; }
    Map? Map { get; }
    Point Position { get; }
    void Go(Direction direction);
    void InitMapandPosition(Map map, Point position);
    public string ToString();
}
