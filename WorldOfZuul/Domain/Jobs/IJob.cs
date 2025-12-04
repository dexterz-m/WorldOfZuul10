namespace WorldOfZuul.Domain.Jobs;

public interface IJob
{
    int Id { get; }
    string Name { get; }
    string Description { get; }
    List<Villager>? Villagers { get; }
    
    void AddVillager(Villager villager);
    void Work();
}