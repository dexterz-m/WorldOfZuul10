using WorldOfZuul.ConsoleUi;
using WorldOfZuul.Domain.CommandHandler;

namespace WorldOfZuul.Domain.Rooms;

public class Lake : Room
{
    public int Fish { get; private set; } = 10;
    private Resources _resources = new Resources();

    public Lake(string shortDesc, string longDesc) : base(shortDesc, longDesc)
    {
    }

    public override string GetEnterRoomMessage()
    {
        return "You have arrived at the lake.";
    }

    public override string RoomCommandHandler(Command command, Resources resources)
    {
        _resources = resources;
        switch (command.Name)
        {
            case "catch":
                return StartFishing();
            case "feed":
                return FeedFish();
            default:
                return "Invalid command for lake.";
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

    public string StartFishing() //this function must be changed wait time should be in turns not in real time
    {
        Random random = new Random();

        if (Fish >= 10)
        {
            int turns = random.Next(3, 10); // changing wait time to turns
            return $"There's a lot of fish!\nYou started fishing... Please wait {turns} turns."; // updated message
            
            //await Task.Delay(waitTime);

            if (random.NextDouble() <= 0.95)// 95% chance to catch fish
            {

                return "You caught a fish!";

                Fish--;
                _resources.Food += 1;

                return $"Now you have {_resources.Food} food.";
            }
            else
            {
                return "You didn't catch any fish this time.";
            }
            //Game.NextTurn();
        }
        else if (Fish < 10 && Fish > 5)
        {
            int waitTime = random.Next(7000, 15000);
            return "Looks like lake is evenly populated!\nYou started fishing...";
        }
        else if (Fish <= 5 && Fish > 0)
        {
            int waitTime = random.Next(10000, 20000);
            return "Seems like there are not much fish left!\nYou started fishing...";
        }
        else
        {
            return "There are no fishes to catch!");
        }
    }



    public string FeedFish()
    {
        if (_resources.GrainSeeds > 0)
        {
            Random random = new Random();
            _resources.GrainSeeds -= 1;

            int addFish = random.Next(0, 3);
            Fish += addFish;

            return $"You started feeding the fish with grain seeds...\nNow you have {_resources.GrainSeeds} grain seeds left";
        }
        else
        {
            return "You have no grain seeds to feed fish!";
        }
    }
}