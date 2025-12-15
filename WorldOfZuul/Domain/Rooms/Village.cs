using WorldOfZuul.ConsoleUi;
using WorldOfZuul.Domain.CommandHandler;
using WorldOfZuul.Domain.Jobs;

namespace WorldOfZuul.Domain.Rooms;

public class Village : Room
{
    private static List<Villager> Villagers { get; set; } = new List<Villager>();
    private int Houses { get; set; } = 5;

    private Resources _resources = new Resources();

    public Village(string shortDesc, string longDesc) : base(shortDesc, longDesc)
    {
    }

    public override string GetEnterRoomMessage()
    {
        return $"You have entered the Village.\n\n" +
               "Below are the current stats:\n" +
               $"Villagers: {Villagers.Count}\n" +
               $"Houses: {Houses}\n\n" +
               "Here are available commands\n" +
               "ls v                                   List out all villagers and their status\n" +
               "ls r                                   List out all rooms\n" +
               "ls j                                   List out all jobs\n" +
               "cd [ROOM NAME]                         Goes to room\n\n" +
               "feed [VILLAGER ID] [AMOUNT]            Feeds villager and activates it\n" +
               "assign [VILLAGER ID] [JOB ID]          Assigns villager to a task\n" +
               "help                                   Show help\n" +
               "sleep                                  Skip the remaining moves\n";
    }

    public override string RoomCommandHandler(Command command, Resources resources)
    {
        switch (command.Name)
        {
            case "feed":
                FeedVillager(Convert.ToInt32(command.SecondWord), Convert.ToInt32(command.ThirdWord ?? "1"));
                break;
            case "assign":
                //if (AssignVillager(Convert.ToInt32(command.SecondWord), Convert.ToInt32(command.ThirdWord))) Game.NextTurn();
                break;
            case "cook":
                Cook(Convert.ToInt32(command.SecondWord ?? "1"));
                break;
            default:
                Console.WriteLine("I don't know what command.");
                break;
        }

        return "Village";
    }



    private string FeedVillager(int villagerId, int foodAmount)
    {
        var villager = Villagers.FirstOrDefault(villager => villager.Id == villagerId);
        if (villager == null)
        {
            return $"No villager with ID {villagerId} found.";
        }
        //Game.NextTurn();
        //villager.Feed(foodAmount);
        return $"Villager {villager.Id} has been fed {foodAmount} food. Current hunger: villager.Hunger.";
    }

    //private string AssignVillager(int villagerId, int jobId)
    //{
    //    var villager = Villagers.FirstOrDefault(villager => villager.Id == villagerId);
    //    if (villager == null)
    //    {
            
    //        return $"No villager with ID {villagerId} found.";
    //    }
    //    //if (!villager.CanWork && jobId != 0)
    //    //{
    //    //    return $"Villager with ID {villagerId} is not able to work. Try feeding them first.";
    //    //}

    //    Job? targetJob = null;
    //    foreach (var room in Game.Rooms) //this must be handled better later
    //    {
    //        if (room?.Jobs == null) continue;
    //        foreach (var job in room.Jobs.Where(job => job?.Id == jobId))
    //        {
    //            targetJob = job;
    //        }
    //        if (targetJob != null) break;
    //    }

    //    if (targetJob == null)
    //    {           
    //        return $"No job with ID {jobId} found.";
    //    }
         
    //    foreach (var room in DataHandler.Rooms) //this must be handled better later
    //    {
    //        if (room?.Jobs == null) continue;
    //        foreach (var job in room.Jobs)
    //        {
    //            job?.Villagers?.Remove(villager);
    //        }
    //    }

    //    if (targetJob.Villagers != null && targetJob.Villagers.Contains(villager))
    //    {
    //        return $"Villager with ID {villagerId} already assigned to {targetJob.Name}.";
    //    }

    //    targetJob.AddVillager(villager);
    //    return "Villager assigned";
    //}

    private string Cook(int amount)
    {
        if (_resources.GrainSeeds < amount)
        {
            return $"Not enough grains to cook {amount} food. You have {_resources.GrainSeeds} grains.";
        }
        _resources.GrainSeeds -= amount;
        _resources.Food += amount;
        //Game.NextTurn();

        return $"Cooked {amount} food. You now have {_resources.Food} food and {_resources.GrainSeeds} grains left.";
    }

    public void CreateVillagers(int numberOfVillagers = 3)
    {
        for (int i = 0; i < numberOfVillagers; i++)
        {
            var villager = new Villager(i + 1, $"Villager {i + 1}");
            Villagers.Add(villager);
        }

        // Assign all villagers to unemployed job initially
        foreach (var villager in Villagers)
        {
            //AssignVillager(villager.Id, 0);
        }
    }

    public void FoodLoss() // this must be called differently
    {
        //foreach (var villager in Villagers)
        //{
        //    bool canWork = villager.Starve(2);
        //    if (!canWork) continue;
        //    Console.WriteLine($"Villager {villager.Id} is too hungry to work!");
        //    //AssignVillager(villager.Id, 0);
        //}
    }

    public void ListVillagers() // this must be called differently
    {

        //Console.WriteLine("+----+------------+--------+----------+");
        //Console.WriteLine("| ID |    Name    | Hunger | Can Work |");
        //foreach (var villager in Villagers)
        //{
        //    Console.WriteLine("+----+------------+--------+----------+");
        //    Console.Write($"| {villager.Id,2} | {villager.Name,10} | ");
        //    Console.BackgroundColor = villager.Hunger <= 25 ? ConsoleColor.Red : ConsoleColor.DarkGreen;
        //    Console.ForegroundColor = ConsoleColor.White;
        //    Console.Write($"{villager.Hunger,6}");
        //    Console.ResetColor();
        //    Console.Write(" | ");
        //    Console.BackgroundColor = !villager.CanWork ? ConsoleColor.Red : ConsoleColor.DarkGreen;
        //    Console.ForegroundColor = ConsoleColor.White;
        //    Console.Write($"{villager.CanWork,8}");
        //    Console.ResetColor();
        //    Console.WriteLine(" |");
        //    Console.WriteLine("+----+------------+--------+----------+");
        //}
    }
}