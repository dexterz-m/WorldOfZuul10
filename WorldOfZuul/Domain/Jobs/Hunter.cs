using WorldOfZuul.ConsoleUi;

namespace WorldOfZuul.Domain.Jobs;

public class Hunter : Job
{
    private int _resourceGainedPerTurn;
    private int _animalsKilledPerTurn;

    public Hunter(int resourceGainedPerTurn, int animalsKilledPerTurn) : base(2, "Hunter", "Hunts animals for food")
    {
        _resourceGainedPerTurn = resourceGainedPerTurn;
        _animalsKilledPerTurn = animalsKilledPerTurn;
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