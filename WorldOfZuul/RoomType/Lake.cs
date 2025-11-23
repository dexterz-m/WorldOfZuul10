using System.Threading.Tasks;

namespace WorldOfZuul.RoomType
{
    public class Lake : Room
    {
        private Random Rnd { get; set; } = new Random();
        private int Fish { get; set; } = 10;

        public Lake(string shortDesc, string longDesc) : base(shortDesc, longDesc)
        {

        }

        public override void EnterRoom()
        {
            Console.WriteLine("You have arrived at the lake. Below are the current stats:");
            Console.WriteLine($"Fish in the lake: {Fish}");
            Console.WriteLine($"Grain seeds in your pocket: {Game.Resources.GrainSeeds}");

            Console.WriteLine("Available actions:");
            Console.WriteLine(" catch fish - start fishing  : Catch fish");
            Console.WriteLine(" feed fish  - feed fish      : Feed fish");
            Console.WriteLine();
            Console.WriteLine("Type a command to perform the action.");
            Console.WriteLine();
        }

        public override async void CommandList(Command command)
        {
            try
            {
                switch (command.Name)
                {
                    case "catch":
                        await CatchFish();
                        break;
                    case "feed":
                        await FeedFish();
                        Console.WriteLine("Feed who?");
                        break;
                    default:
                        Console.WriteLine("Invalid command for lake.");
                        break;
                }
            }
            catch (Exception e)
            {
                await Console.Error.WriteLineAsync(e.Message);
            }
        }

        private async Task CatchFish()
        {
            switch (Fish)
            {
                case >= 10:
                {
                    Console.WriteLine("There's a lot of fish!");
                    Console.WriteLine("You started fishing...");
                    int waitTime = Rnd.Next(3000, 10000);
                    
                    await Task.Delay(waitTime);

                    if (Rnd.NextDouble() <= 0.95)// 95% chance to catch fish
                    {
                        
                        Console.WriteLine("You caught a fish!");

                        Fish--;
                        Game.Resources.Food = 1;

                        Console.WriteLine($"Now you have {Game.Resources.Food} food.");
                    }
                    else
                    {
                        Console.WriteLine("You didn't catch any fish this time.");
                    }
                    Game.NextTurn();

                    break;
                }
                case > 5:
                {
                    Console.WriteLine("Looks like lake is evenly populated!");

                    Console.WriteLine("You started fishing...");
                    int waitTime = Rnd.Next(7000, 15000);

                    await Task.Delay(waitTime);
                    
                    if (Rnd.NextDouble() <= 0.75) // 75% chance to catch fish
                    {
                        Console.WriteLine("You caught a fish!");

                        Fish--;
                        Game.Resources.Food = 1;

                        Console.WriteLine($"Now you have {Game.Resources.Food} food.");
                    }
                    else
                    {
                        Console.WriteLine("You didn't catch any fish this time.");
                    }
                    Game.NextTurn();

                    break;
                }
                case > 0:
                {
                    Console.WriteLine("Seems like there are not much fish left!");

                    Console.WriteLine("You started fishing...");
                    int waitTime = Rnd.Next(10000, 20000);

                    await Task.Delay(waitTime);
                    
                    if (Rnd.NextDouble() <= 0.25) // 25% chance to catch fish
                    {
                        Console.WriteLine("You caught a fish!");

                        Fish--;
                        Game.Resources.Food = 1;

                        Console.WriteLine($"Now you have {Game.Resources.Food} food.");
                    }
                    else
                    {
                        Console.WriteLine("You didn't catch any fish this time.");
                    }

                    Console.WriteLine("You caught a fish!");

                    Fish--;
                    Game.Resources.Food = 1;

                    Console.WriteLine($"Now you have {Game.Resources.Food} food.");
                    Game.NextTurn();
                    break;
                }
                default:
                    Console.WriteLine("There are no fishes to catch!");
                    break;
            }
        }

        private async Task FeedFish()
        {

            if (Game.Resources.GrainSeeds > 0)
            {
                Game.Resources.GrainSeeds = -1;

                Console.WriteLine("You started feeding the fish with grain seeds...");
                Console.WriteLine($"Now you have {Game.Resources.GrainSeeds} grain seeds left");

                await Task.Delay(3000);

                int addFish = Rnd.Next(0, 3);

                Console.WriteLine($"{addFish} fish came to your lake!");

                Fish += addFish;
                Game.NextTurn();
            }
            else
            {
                Console.WriteLine("You have no grain seeds to feed fish!");
            }
        }

    }
}
