namespace WorldOfZuul.Jobs;

/// <summary>
/// Represents a lumberjack job that cuts trees and produces wood resources.
/// </summary>
/// <remarks>
/// Create a <see cref="Lumberjack"/> with the per-turn production settings. The two constructor
/// parameters configure how much resource is produced per turn and how many trees are cut down
/// per turn. Assign villagers with <see cref="Job.AddVillager"/>, then call <see cref="Work"/>
/// once per game turn to apply the job's effects.
/// </remarks>
/// <example>
/// // Create a lumberjack job and simulate a turn:
/// var lumberjack = new Lumberjack(resourceGainedPerTurn: 5, treesCutDownPerTurn: 1);
/// lumberjack.AddVillager(villager);
/// lumberjack.Work(); // perform work for all assigned villagers
/// </example>
public class Lumberjack : Job
{
    /// <summary>
    /// Amount of resource gained per villager per turn.
    /// </summary>
    private int _resourceGainedPerTurn;

    /// <summary>
    /// Number of trees cut down per villager per turn.
    /// </summary>
    private int _treesCutDownPerTurn;

    /// <summary>
    /// Initializes a new instance of the <see cref="Lumberjack"/> job.
    /// </summary>
    /// <param name="resourceGainedPerTurn">Amount of resource gained for the job per turn (per villager).</param>
    /// <param name="treesCutDownPerTurn">Number of trees removed from the environment per turn (per villager).</param>
    public Lumberjack(int resourceGainedPerTurn, int treesCutDownPerTurn) : base(1, "Lumberjack", "")
    {
        _resourceGainedPerTurn = resourceGainedPerTurn;
        _treesCutDownPerTurn = treesCutDownPerTurn;
    }

    /// <summary>
    /// Performs lumberjack work for the current turn for all assigned villagers.
    /// </summary>
    /// <remarks>
    /// Adds wood to the shared Resources (Resources.Wood) equal to _resourceGainedPerTurn * numberOfVillagers and subtracts trees from Resources.Trees equal to _treesCutDownPerTurn * numberOfVillagers; do not modify villagers' resources.
    /// </remarks>
    public override void Work()
    {
        Game.resources.Wood = _resourceGainedPerTurn;
        
    }
}