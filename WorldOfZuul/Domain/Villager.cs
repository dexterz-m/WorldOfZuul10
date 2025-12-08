namespace WorldOfZuul.Domain;

public class Villager // must be implemented
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public Villager(int id, string name)
    {
        Id = id;
        Name = name;
    }
}