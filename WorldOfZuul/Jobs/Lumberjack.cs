namespace WorldOfZuul.Jobs;

public class Lumberjack : Job
{
    private int _resourceGainedPerTurn;
    private int _treesCutDownPerTurn;

    public Lumberjack(int resourceGainedPerTurn, int treesCutDownPerTurn) : base(1, "Lumberjack", "")
    {
        _resourceGainedPerTurn = resourceGainedPerTurn;
        _treesCutDownPerTurn = treesCutDownPerTurn;
    }

    public override void Work()
    {
        //TODO: Implement cutting trees
    }
}