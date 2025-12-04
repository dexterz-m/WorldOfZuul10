using WorldOfZuul.Domain.CommandHandler;

namespace WorldOfZuul.Domain.Rooms;

public class Lake : Room
{
    public int Fish { get; private set; } = 10;

    public Lake(string shortDesc, string longDesc) : base(shortDesc, longDesc)
    {
    }

    public override string GetEnterRoomMessage()
    {
        return "You have arrived at the lake.";
    }

    public string GetLakeStats(Resources resources)
    {
        return "Below are the current stats:\n" +
               $"Fish in the lake: {Fish}\n" +
               $"Grain seeds in your pocket: {resources.GrainSeeds}\n\n" +
               "Available actions:\n" +
               " catch fish - start fishing  : Catch fish\n" +
               " feed fish  - feed fish      : Feed fish\n\n" +
               "Type a command to perform the action.\n";
    }

    public (string message, int waitTime, bool success) StartFishing()
    {
        Random random = new Random();

        if (Fish >= 10)
        {
            int waitTime = random.Next(3000, 10000);
            return ("There's a lot of fish!\nYou started fishing...", waitTime, true);
        }
        else if (Fish < 10 && Fish > 5)
        {
            int waitTime = random.Next(7000, 15000);
            return ("Looks like lake is evenly populated!\nYou started fishing...", waitTime, true);
        }
        else if (Fish <= 5 && Fish > 0)
        {
            int waitTime = random.Next(10000, 20000);
            return ("Seems like there are not much fish left!\nYou started fishing...", waitTime, true);
        }
        else
        {
            return ("There are no fishes to catch!", 0, false);
        }
    }

    public string CompleteFishing(Resources resources)
    {
        if (Fish > 0)
        {
            Fish--;
            resources.Food += 1;
            return $"You caught a fish!\nNow you have {resources.Food} food.";
        }
        return "Failed to catch fish.";
    }

    public string FeedFish(Resources resources)
    {
        if (resources.GrainSeeds > 0)
        {
            Random random = new Random();
            resources.GrainSeeds -= 1;

            int addFish = random.Next(0, 3);
            Fish += addFish;

            return "You started feeding the fish with grain seeds...\nNow you have {resources.GrainSeeds} grain seeds left";
        }
        else
        {
            return "You have no grain seeds to feed fish!";
        }
    }

    public string CompleteFeedFish(int addedFish)
    {
        return $"{addedFish} fish came to your lake!";
    }
}