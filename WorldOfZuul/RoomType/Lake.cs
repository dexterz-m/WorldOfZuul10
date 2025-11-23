using System.Threading.Tasks;

namespace WorldOfZuul.RoomType
{
    public class Lake : Room
    {
        int Fish { get; set; } = 10;

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
            switch (command.Name)
            {
                case "catch":
                    if (command.SecondWord == "fish")
                        await CatchFish();
                    else
                        Console.WriteLine("Catch what?");
                    break;
                case "feed":
                    if (command.SecondWord == "fish")
                        await FeedFish();
                    else
                        Console.WriteLine("Feed who?");
                    break;
                default:
                    Console.WriteLine("Invalid command for lake.");
                    break;
            }
        }

        public async Task CatchFish()
        {

            if (Fish >= 10)
            {
                Random random = new Random();

                Console.WriteLine("There's a lot of fish!");

                Console.WriteLine("You started fishing...");
                int waitTime = random.Next(3000, 10000);

                await Task.Delay(waitTime);

                Console.WriteLine("You caught a fish!");

                Fish--;
                Game.Resources.Food = 1;

                Console.WriteLine($"Now you have {Game.Resources.Food} food.");

            }

            else if (Fish < 10 && Fish > 5)
            {
                Random random = new Random();

                Console.WriteLine("Looks like lake is evenly populated!");

                Console.WriteLine("You started fishing...");
                int waitTime = random.Next(7000, 15000);

                await Task.Delay(waitTime);

                Console.WriteLine("You caught a fish!");

                Fish--;
                Game.Resources.Food = 1;

                Console.WriteLine($"Now you have {Game.Resources.Food} food.");

            }

            else if (Fish <= 5 && Fish > 0)
            {
                Random random = new Random();

                Console.WriteLine("Seems like there are not much fish left!");

                Console.WriteLine("You started fishing...");
                int waitTime = random.Next(10000, 20000);

                await Task.Delay(waitTime);

                Console.WriteLine("You caught a fish!");

                Fish--;
                Game.Resources.Food = 1;

                Console.WriteLine($"Now you have {Game.Resources.Food} food.");
            }
            else
            {
                Console.WriteLine("There are no fishes to catch!");
            }
        }

        public async Task FeedFish()
        {

            if (Game.Resources.GrainSeeds > 0)
            {
                Random random = new Random();
                Game.Resources.GrainSeeds = -1;

                Console.WriteLine("You started feeding the fish with grain seeds...");
                Console.WriteLine($"Now you have {Game.Resources.GrainSeeds} grain seeds left");

                await Task.Delay(3000);

                int AddFish = random.Next(0, 3);

                Console.WriteLine($"{AddFish} fish came to your lake!");

                Fish += AddFish;
            }
            else
            {
                Console.WriteLine("You have no grain seeds to feed fish!");
            }
        }

    }
}
