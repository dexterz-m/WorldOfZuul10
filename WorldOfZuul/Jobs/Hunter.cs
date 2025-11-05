namespace WorldOfZuul.Jobs;

/// <summary>
/// Represents a hunter job that kills animals and produces food resources.
/// </summary>
/// <remarks>
/// Create a <see cref="Hunter"/> with the per-turn production settings. The two constructor
/// parameters configure how much resource is produced per turn and how many trees are cut down
/// per turn. Assign villagers with <see cref="Job.AddVillager"/>, then call <see cref="Work"/>
/// once per game turn to apply the job's effects.
/// </remarks>
/// <example>
/// // Create a hunter job and simulate a turn:
/// var hunter = new Hunter(resourceGainedPerTurn: 5, animalsKilledPerTurn: 1);
/// hunter.AddVillager(villager);
/// hunter.Work(); // perform work for all assigned villagers
/// </example>
public class Hunter : Job
{
    /// <summary>
    /// Amount of resource gained per villager per turn.
    /// </summary>
    private int _resourceGainedPerTurn;

    /// <summary>
    /// Number of trees cut down per villager per turn.
    /// </summary>
    private int _animalsKilledPerTurn;

    /// <summary>
    /// Initializes a new instance of the <see cref="Hunter"/> job.
    /// </summary>
    /// <param name="resourceGainedPerTurn">Amount of food gained for the job per turn (per villager).</param>
    /// <param name="animalsKilledPerTurn">Number of trees removed from the environment per turn (per villager).</param>
    public Hunter(int resourceGainedPerTurn, int animalsKilledPerTurn) : base(2, "Hunter", "")
    {
        _resourceGainedPerTurn = resourceGainedPerTurn;
        _animalsKilledPerTurn = animalsKilledPerTurn;
    }

    /// <summary>
    /// Performs hunter work for the current turn for all assigned villagers.
    /// </summary>
    /// <remarks>
    /// Adds wood to the shared Resources (Resources.Food) equal to _resourceGainedPerTurn * numberOfVillagers and subtracts animals from Forest.Animals equal to _animalsKilledPerTurn * numberOfVillagers; do not modify villagers' resources.
    /// </remarks>
    public override void Work()
    {
        Game.Resources.Food = _resourceGainedPerTurn;
        foreach (var room in Game.Rooms.Where(room => room is { ShortDescription: "Forest" }))
        {
            var forest = room as RoomType.Forest;
            forest?.KillAnimal(_animalsKilledPerTurn * (this.Villagers?.Count ?? 0));
        }
    }
}