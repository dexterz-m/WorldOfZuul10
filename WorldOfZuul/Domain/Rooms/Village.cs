using System;
using System.Linq;
using WorldOfZuul.Domain.CommandHandler;
using WorldOfZuul.Domain.Jobs;

namespace WorldOfZuul.Domain.Rooms;

public class Village : Room
{
    private int Houses { get; set; } = 5;

    public Village(string shortDesc, string longDesc) : base(shortDesc, longDesc)
    {
    }

    public override string GetEnterRoomMessage()
    {
        var dh = DataHandler.Instance;
        int villagerCount = dh?.Villagers?.Count ?? 0;

        return $"You have entered the Village.\n\n" +
               "Below are the current stats:\n" +
               $"Villagers: {villagerCount}\n" +
               $"Houses: {Houses}\n\n" +
               "Here are available commands\n" +
               " - feed [VILLAGER ID] [AMOUNT]           : Feeds villager\n" +
               " - assign [VILLAGER ID] [JOB ID]         : Assigns villager to a task\n" +
               " - cook [AMOUNT]                         : Converts grain to food\n" +
               " - sleep                                 : Sleeps thru the day\n";
    }

    public override string RoomCommandHandler(Command command, Resources resources)
    {
        switch (command.Name)
        {
            case "feed":
            {
                if (string.IsNullOrWhiteSpace(command.SecondWord))
                    return "Usage: feed [VILLAGER ID] [AMOUNT]";

                if (!int.TryParse(command.SecondWord, out int villagerId))
                    return "Villager ID must be a number.";

                int amount = 1;
                if (!string.IsNullOrWhiteSpace(command.ThirdWord) && !int.TryParse(command.ThirdWord, out amount))
                    return "Amount must be a number.";

                return FeedVillager(villagerId, amount);
            }
            case "assign":
            {
                if (string.IsNullOrWhiteSpace(command.SecondWord) || string.IsNullOrWhiteSpace(command.ThirdWord))
                    return "Usage: assign [VILLAGER ID] [JOB ID]";

                if (!int.TryParse(command.SecondWord, out int villagerId))
                    return "Villager ID must be a number.";

                if (!int.TryParse(command.ThirdWord, out int jobId))
                    return "Job ID must be a number.";

                return AssignVillager(villagerId, jobId);
            }
            case "cook":
            {
                int amount = 1;
                if (!string.IsNullOrWhiteSpace(command.SecondWord) && !int.TryParse(command.SecondWord, out amount))
                    return "Amount must be a number.";

                return Cook(resources, amount);
            }
            default:
                return "Invalid command in the village.";
        }
    }

    private string FeedVillager(int villagerId, int foodAmount)
    {
        var dh = DataHandler.Instance;
        if (dh == null) return "Game data not initialized.";

        var villager = dh.FindVillagerById(villagerId);
        if (villager == null) return $"No villager with ID {villagerId} found.";

        int beforeFood = dh.Resources.Food;
        int beforeHunger = villager.Hunger;

        villager.Feed(foodAmount);

        int afterFood = dh.Resources.Food;
        int afterHunger = villager.Hunger;

        int foodUsed = beforeFood - afterFood;
        return $"Fed villager {villager.Id}. Food used: {foodUsed}. Hunger: {beforeHunger} -> {afterHunger}.";
    }

    private string AssignVillager(int villagerId, int jobId)
    {
        var dh = DataHandler.Instance;
        if (dh == null) return "Game data not initialized.";

        var villager = dh.FindVillagerById(villagerId);
        if (villager == null)
        {
            return $"No villager with ID {villagerId} found.";
        }

        if (!villager.CanWork && jobId != 0)
        {
            return $"Villager with ID {villagerId} is not able to work. Try feeding them first.";
        }

        var targetJob = dh.FindJobById(jobId);
        if (targetJob == null)
        {
            return $"No job with ID {jobId} found.";
        }

        foreach (var job in dh.Jobs)
        {
            job?.Villagers?.RemoveAll(v => v.Id == villagerId);
        }

        if (targetJob.Villagers != null && targetJob.Villagers.Any(v => v.Id == villagerId))
        {
            return $"Villager with ID {villagerId} already assigned to {targetJob.Name}.";
        }

        targetJob.AddVillager(villager);
        return $"Villager {villagerId} assigned to {targetJob.Name}.";
    }

    private string Cook(Resources resources, int amount)
    {
        if (amount <= 0) return "Amount must be greater than 0.";

        if (resources.GrainSeeds < amount)
        {
            return $"Not enough grains to cook {amount} food. You have {resources.GrainSeeds} grains.";
        }

        resources.GrainSeeds -= amount;
        resources.Food += amount;

        return $"Cooked {amount} food. You now have {resources.Food} food and {resources.GrainSeeds} grains left.";
    }

    public void FoodLoss()
    {
        var dh = DataHandler.Instance;
        if (dh == null) return;

        foreach (var villager in dh.Villagers)
        {
            bool justBecameUnable = villager.Starve(2);
            if (justBecameUnable)
            {
                AssignVillager(villager.Id, 0);
            }
        }
    }

    public void ListVillagers()
    {
        var dh = DataHandler.Instance;
        if (dh == null) return;

        Console.WriteLine("+----+------------+--------+----------+--------------+");
        Console.WriteLine("| ID |    Name    | Hunger | Can Work |     Job      |");
        Console.WriteLine("+----+------------+--------+----------+--------------+");

        foreach (var villager in dh.Villagers)
        {
            Job? job = dh.FindVillagerJob(villager);
            Console.WriteLine($"| {villager.Id,2} | {villager.Name,10} | {villager.Hunger,6} | {villager.CanWork,8} | {job?.Name ?? "Unemployed",12} |");
            Console.WriteLine("+----+------------+--------+----------+--------------+");
        }
    }
}
