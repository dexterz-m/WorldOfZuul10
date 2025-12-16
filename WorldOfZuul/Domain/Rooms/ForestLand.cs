using System.Resources;
using WorldOfZuul.ConsoleUi;
using WorldOfZuul.Domain.CommandHandler;
using WorldOfZuul.Domain.Jobs;

namespace WorldOfZuul.Domain.Rooms;

public class Forest : Room
{
    private static readonly Random Rng = new();
    private Resources _resources = new Resources();

    public Forest(string shortDesc, string longDesc) : base(shortDesc, longDesc, new Lumberjack(1, 1))
    {
    }

    public override string GetEnterRoomMessage()
    {
        return "You have entered the Forest.\n\n" +
               "Below are the current stats:\n" +
               $"Trees: {_resources.Trees}\n" +
               $"Animals: {_resources.Animals}\n\n" +
               "Available actions:\n" +
               " - cut    : Cut down one tree (reduces sustainability)\n" +
               " - plant  : Plant a tree (increases sustainability)\n" +
               " - kill   : Kill one animal (reduces sustainability)\n\n" +
               "Type a command to perform the action.";
    }

    public override string RoomCommandHandler(Command command, Resources resources)
    {

        _resources = resources;

        switch (command.Name)
        {
            case "cut":
                return CutTree();
            case "plant":
                return PlantSapling();
            case "kill":
                return KillAnimal();
            default:
                return "Invalid command in the forest.";
        }
    }


    public string GetForestStats(Resources resources, int sustainabilityPoints)
    {
        return "Below are the current stats:\n" +
               $"Trees: {resources.Trees}\n" +
               $"Animals: {resources.Animals}\n" +
               $"Sustainability Points: {sustainabilityPoints}\n\n" +
               "Available actions:\n" +
               "cut - cut tree    : Cut down one tree (reduces sustainability)\n" +
               "plant - plant tree  : Plant a tree (increases sustainability)\n" +
               "kill - kill animal : Kill one animal (reduces sustainability)\n\n" +
               "Type a command to perform the action.\n";
    }

    public string CutTree(int amount = 1)
    {
        if (_resources.Trees < amount)
        {
            return "No trees left to cut.";
        }

        _resources.Trees -= amount;
        _resources.Wood += amount * Rng.Next(1, 5);
        _resources.Saplings += amount * Rng.Next(1, 4);
        _resources.SustainabilityPoints -= 2;


        if (_resources.Animals > 0)
        {
            int animalsLost = Rng.Next(1, 4); // picks 1, 2 or 3
            animalsLost = Math.Min(animalsLost, _resources.Animals); // don't remove more than exist
            _resources.Animals -= animalsLost;

            // Each lost animal reduces SustainabilityPoints by 1 (weight can be adjusted)
            _resources.SustainabilityPoints -= animalsLost;
        }

        string message = $"You cut {amount} tree(s). Consider planting a tree to maintain ecosystem balance.";

        return message;
    }

    public string PlantSapling()
    {
        if (_resources.Saplings > 0)
        {
            _resources.Saplings--;
            //PlantedSaplings.Add(DataHandler.TurnsThisDay);
            return "You have planted a sapling.";
        }
        else
        {
            return "You don't have any saplings to plant.";
        }
    }

    public string KillAnimal(int amount = 1)
    {
        if (_resources.Animals < amount)
        {
            return "No animals left to kill.";
        }

        _resources.Animals -= amount;
        _resources.Food += amount * Rng.Next(1, 4);
        _resources.SustainabilityPoints--;

        return $"You killed {amount} animal(s).";
    }
}