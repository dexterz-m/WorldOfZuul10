using WorldOfZuul.Domain.CommandHandler;
using WorldOfZuul.Domain.Jobs;

namespace WorldOfZuul.Domain.Rooms;

public class Forest : Room
{
    private static readonly Random Rng = new();

    public Forest(string shortDesc, string longDesc) : base(shortDesc, longDesc, new Lumberjack(1, 1))
    {
    }

    public override string GetEnterRoomMessage()
    {
        return "You have entered the Forest.";
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

    public (string message, int sustainabilityChange, int animalsLost) CutTree(Resources resources, int amount = 1)
    {
        if (resources.Trees < amount)
        {
            return ("No trees left to cut.", 0, 0);
        }

        resources.Trees -= amount;
        resources.Wood += amount * 2;
        
        int sustainabilityLoss = -2 * amount;
        int animalsLost = 0;

        if (resources.Animals > 0)
        {
            animalsLost = Rng.Next(1, 4);
            animalsLost = Math.Min(animalsLost, resources.Animals);
            resources.Animals -= animalsLost;
            sustainabilityLoss -= animalsLost;
        }

        string message = $"You cut {amount} tree(s). Consider planting a tree to maintain ecosystem balance.";
        if (animalsLost > 0)
        {
            message += $"\n{animalsLost} animal(s) left the area due to habitat loss.";
        }

        return (message, sustainabilityLoss, animalsLost);
    }

    public (string message, int sustainabilityChange) PlantTree(Resources resources)
    {
        resources.Trees += 1;
        return ($"You planted a tree. Trees remaining: {resources.Trees}.", 2);
    }

    public (string message, int sustainabilityChange) KillAnimal(Resources resources, int amount = 1)
    {
        if (resources.Animals < amount)
        {
            return ("No animals left to kill.", 0);
        }

        resources.Animals -= amount;
        resources.Food += amount * 2;
        
        return ($"You killed {amount} animal(s).", -amount);
    }
}