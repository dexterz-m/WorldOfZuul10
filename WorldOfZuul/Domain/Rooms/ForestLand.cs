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
               " - kill   : Kill one animal (reduces sustainability)\n";
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
            int animalsLost = Rng.Next(1, 4);
            animalsLost = Math.Min(animalsLost, _resources.Animals);
            _resources.Animals -= animalsLost;
            _resources.SustainabilityPoints -= animalsLost;
        }

        return $"You cut {amount} tree(s). Consider planting a tree to maintain ecosystem balance.";
    }

    public string PlantSapling()
    {
        int currentTurn = DataHandler.Instance?.TotalTurns ?? 0;

        if (_resources.TryPlantSapling(currentTurn))
        {
            return "You have planted a sapling.";
        }

        return "You don't have any saplings to plant.";
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
