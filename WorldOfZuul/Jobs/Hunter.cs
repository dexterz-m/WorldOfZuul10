namespace WorldOfZuul.Jobs;

public class Hunter : Job
{

    public Hunter() : base(2, "Hunter", "")
    {
    }

    public override void Work()
    {
        foreach (var room in Game.Rooms.Where(room => room is { ShortDescription: "Forest" }))
        {
            var forest = room as RoomType.Forest;
            forest?.KillAnimal(this.Villagers?.Count ?? 0);
        }
    }
}