namespace Simulator.Maps;

public abstract class BigMap : Map
{
    public BigMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        if (sizeX > 1000)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeX), "Too wide");
        }
        if (sizeY > 1000)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeY), "Too tall");
        }
    }
    public override Map Clone()
    {
        var newMap = (BigMap)Activator.CreateInstance(this.GetType(), SizeX, SizeY)!;
        foreach (var field in Fields)
        {
            newMap.Fields[field.Key] = new List<IMappable>(field.Value);
        }
        return newMap;
    }
}