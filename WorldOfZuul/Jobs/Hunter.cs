namespace WorldOfZuul.Jobs;

public class Hunter : Job
{
    private readonly int _resourceGainedPerTurn;
    private readonly int _animalsKilledPerTurn;

    public Hunter(int resourceGainedPerTurn, int animalsKilledPerTurn) : base(2, "Hunter", "")
    {
        _resourceGainedPerTurn = resourceGainedPerTurn;
        _animalsKilledPerTurn = animalsKilledPerTurn;
    }

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