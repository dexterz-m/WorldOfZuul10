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
        Game.Resources.Wood = _resourceGainedPerTurn;
        foreach (var room in Game.Rooms.Where(room => room is { ShortDescription: "Forest" }))
        {
            var forest = room as RoomType.Forest;
            var tmp = this.Villagers?.Count ?? 0;
            forest?.CutTree(tmp * _resourceGainedPerTurn);
        }
    }
}