namespace WorldOfZuul.Jobs;

public class Hunter : Job
{
    private int _resourceGainedPerTurn;
    private int _animalsKilledPerTurn;

    public Hunter(int resourceGainedPerTurn, int animalsKilledPerTurn) : base(2, "Hunter", "")
    {
        _resourceGainedPerTurn = resourceGainedPerTurn;
        _animalsKilledPerTurn = animalsKilledPerTurn;
    }

    public override void Work()
    {
        Game.Resources.Food = _resourceGainedPerTurn * (this.Villagers?.Count ?? 0);
        var forest = (Game.Rooms.FirstOrDefault(r => r?.ShortDescription == "Forest")) as RoomType.Forest;
        forest?.KillAnimal(_animalsKilledPerTurn * (this.Villagers?.Count ?? 0));
    }
}