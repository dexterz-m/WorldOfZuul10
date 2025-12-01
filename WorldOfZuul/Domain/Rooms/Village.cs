using WorldOfZuul.Domain.CommandHandler;
using WorldOfZuul.Domain.Jobs;

namespace WorldOfZuul.Domain.Rooms;

public class Village : Room
{
    public int VillagersCount { get; private set; } = 20;
    public int Houses { get; private set; } = 5;
    
    public Village(string shortDesc, string longDesc) : base(shortDesc, longDesc, new Unemployed())
    {
    }

    public override string GetEnterRoomMessage()
    {
        return $"You have entered the Village.\n\n" +
               "Below are the current stats:\n" +
               $"Villagers: {VillagersCount}\n" +
               $"Houses: {Houses}\n\n" +
               "Here are available commands\n" +
               "ls v                                   List out all villagers and their status\n" +
               "ls r                                   List out all rooms\n" +
               "ls j                                   List out all jobs\n" +
               "cd [ROOM NAME]                         Goes to room\n\n" +
               "feed -[VILLAGER ID] [AMOUNT/DAY]       Feeds villager and activates it\n" +
               "assign -[VILLAGER ID] [JOB NAME]       Assigns villager to a task\n" +
               "sleep                                  Skip the remaining moves\n";
    }

    public string Feed(int villagerId, int amount, Resources resources)
    {
        if (resources.Food < amount)
        {
            return "Not enough food to feed the villager!";
        }

        resources.Food -= amount;
        resources.Hunger -= amount;
        
        return $"Villager {villagerId} has been fed {amount} food.";
    }

    public string Assign(Villager villager, int jobId)
    {   
        if (villager == null)
        {
            return $"No Villager with id {villager.Id}! Try 'ls v' to see villagers";
        }

        Job? job = dataHandler.FindJobById(jobId);
        if (job == null)
        {
            return $"No Job with id {jobId}! Try 'ls j' to see jobs";
        }

        job.AddVillager(villager);
        return $"Villager {villager.Name} assigned to {job.Name}";
    }
}