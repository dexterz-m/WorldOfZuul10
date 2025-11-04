namespace WorldOfZuul.Jobs;

public class Job
{
    public int Id { get; }
    /*
     * 0 - unemployed
     * 1 - lumberjack
     * 2 - farmer
     */
    public string Name { get; }
    public string Description { get; }
    public List<Villager>? Villagers { get; private set; }

    public Job(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public void AddVillager(Villager villager)
    {
        Villagers ??= new List<Villager>();

        Villagers.Add(villager);
    }

    public virtual void Work() { }
}