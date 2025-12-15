using WorldOfZuul.Domain;
using WorldOfZuul.Domain.CommandHandler;
using WorldOfZuul.Domain.Jobs;
using WorldOfZuul.Domain.Rooms;

namespace WorldOfZuul.Data;

public interface IDataInitializer
{
    public List<Job> jobs { get; }
    public List<Room> rooms { get; }
    public List<Villager> villagers { get; }
    public Resources resources { get; }
}