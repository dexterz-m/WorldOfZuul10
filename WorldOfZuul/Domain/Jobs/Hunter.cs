using System.Xml.Linq;
using WorldOfZuul.ConsoleUi;

namespace WorldOfZuul.Domain.Jobs;

public class Hunter : Job
{
    private int _resourceGainedPerTurn;
    private int _animalsKilledPerTurn;

    public Hunter(int resourceGainedPerTurn, int animalsKilledPerTurn) : base(0, "DefaultName", "DefaultDescription")
    {
        _resourceGainedPerTurn = resourceGainedPerTurn;
        _animalsKilledPerTurn = animalsKilledPerTurn;
    }
    public Hunter(int id, string name, string description) : base(id, name, description)
    {

    }

    public int GetResourceGainedPerTurn() => _resourceGainedPerTurn;
    public int GetAnimalsKilledPerTurn() => _animalsKilledPerTurn;

    public (int foodGained, int animalsKilled) CalculateWorkOutput()
    {
        int villagersWorking = Villagers?.Count ?? 0;
        int foodGained = _resourceGainedPerTurn * villagersWorking;
        int animalsKilled = _animalsKilledPerTurn * villagersWorking;
        
        return (foodGained, animalsKilled);
    }
}