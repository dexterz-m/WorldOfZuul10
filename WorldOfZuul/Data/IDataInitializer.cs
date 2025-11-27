using WorldOfZuul.Domain;
using WorldOfZuul.Domain.CommandHandler;
using WorldOfZuul.Domain.Job;
using WorldOfZuul.Domain.Rooms;

namespace WorldOfZuul.Data;

public interface IDataInitializer
{
    public List<Job> LoadJobs();
    public List<Room> LoadRooms();
    public List<string> LoadCommands();
    public List<Villager> LoadVillagers();
    public Advisor LoadAdvisor();
    
}