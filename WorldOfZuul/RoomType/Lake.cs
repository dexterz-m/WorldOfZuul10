using System.Collections;
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
                        _ = CatchFish().ContinueWith(t =>
                        {
                            if (t.Exception != null){
                                Console.WriteLine("Error: " + t.Exception.InnerException.Message);
                            }
                        }, TaskContinuationOptions.OnlyOnFaulted);
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
            if (Game.TurnsThisDay >= 10)
            {
                Console.WriteLine("You don't have enough turns left today!");
                return;
            }

            int randTurns;

            switch (Fish)
            {
                case >= 10:
                    Console.WriteLine("There's a lot of fish!");
                    Console.WriteLine("You started fishing...");

                    randTurns = 1;

                    break;
                
                case > 5:
                    Console.WriteLine("Looks like the lake is evenly populated!");
                    Console.WriteLine("You started fishing...");
                    
                    randTurns = Rnd.Next(1, 3); // 1–2 turns
                    
                    break;

                case > 0:
                    Console.WriteLine("Seems like not much fish left!");
                    Console.WriteLine("You started fishing...");

                    randTurns = Rnd.Next(2, 4); // 2–3 turns

                    break;

                default:
                    Console.WriteLine("There are no fishes to catch!");

                    return;
            }

            double catchChance = Fish >= 10 ? 0.95 : // chance to catch fish
                                 Fish > 5   ? 0.75 :
                                              0.25;

            bool success = Rnd.NextDouble() <= catchChance;

            if (success)
            {
                Console.WriteLine("You caught a fish!");
                Fish--;
                Game.Resources.Food++;
                Console.WriteLine($"You now have {Game.Resources.Food} food.");
            }
            else
            {
                Console.WriteLine("You didn't catch any fish this time.");
            }

            Console.WriteLine($"{randTurns} turn(s) used.");
            Game.NextTurn(randTurns);
        }

        private async Task FeedFish()
        {

            if (Game.Resources.GrainSeeds > 0)
            {
                Game.Resources.GrainSeeds -= 1;

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
