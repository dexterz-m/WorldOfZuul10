using WorldOfZuul.Not_Implemented;

namespace WorldOfZuul.Jobs;

public class Lumberjack : Job
{
    private const int TreesCutDownPerTurn = 1;
    public Lumberjack() : base(1, "Lumberjack", "")
    {
    }

    /// <summary>
    /// Performs lumberjack work for the current turn for all assigned villagers.
    /// </summary>
    /// <remarks>
    /// Adds wood to the shared Resources (Resources.Wood) equal to _resourceGainedPerTurn * numberOfVillagers and subtracts trees from Resources.Trees equal to _treesCutDownPerTurn * numberOfVillagers; do not modify villagers' resources.
    /// </remarks>
    public override void Work()
    {
        foreach (var room in Game.Rooms.Where(room => room is { ShortDescription: "Forest" }))
        {
            var forest = room as RoomType.Forest;
            var tmp = this.Villagers?.Count ?? 0;
            forest?.CutTree(tmp * TreesCutDownPerTurn);
        }
    }
}