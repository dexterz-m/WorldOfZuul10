namespace WorldOfZuul.Domain.Jobs;

public class Lumberjack : Job
{
    private int _resourceGainedPerTurn;
    private int _treesCutDownPerTurn;
    
    public Lumberjack(int resourceGainedPerTurn, int treesCutDownPerTurn) : base(1, "Lumberjack", "Cuts trees for wood")
    {
        _resourceGainedPerTurn = resourceGainedPerTurn;
        _treesCutDownPerTurn = treesCutDownPerTurn;
    }

    public int GetResourceGainedPerTurn() => _resourceGainedPerTurn;
    public int GetTreesCutDownPerTurn() => _treesCutDownPerTurn;

    public (int woodGained, int treesCut) CalculateWorkOutput()
    {
        int villagersWorking = Villagers?.Count ?? 0;
        int woodGained = _resourceGainedPerTurn * villagersWorking;
        int treesCut = _treesCutDownPerTurn * villagersWorking;
        
        return (woodGained, treesCut);
    }
}