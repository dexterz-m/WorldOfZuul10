namespace WorldOfZuul;

public class Resources
{
    public int Food { get; set; } = 10;
    public int GrainSeeds { get; set; } = 2;
    public int Grains { get; set; } = 0;

    public int Animals { get; set; } = 100;
    public int Trees { get; set; } = 100;
    public int Wood { get; set; } = 0;
    public int Saplings { get; set; } = 0;
    private List<int> PlantedSaplings { get; set; } = new List<int>();
    
    public void PlantSapling()
    {
        if (Saplings > 0)
        {
            Saplings--;
            PlantedSaplings.Add(Game.CurrentTurn);
            Console.WriteLine("You have planted a sapling.");
        }
        else
        {
            Console.WriteLine("You don't have any saplings to plant.");
        }
    }
    
    public void TurnToTrees()
    {
        var maturedSaplings = PlantedSaplings.Where(turn => Game.CurrentTurn - turn >= 5).ToList();
        foreach (var turn in maturedSaplings)
        {
            Trees++;
            PlantedSaplings.Remove(turn);
            Console.WriteLine("A sapling has matured into a tree.");
        }
    }
}