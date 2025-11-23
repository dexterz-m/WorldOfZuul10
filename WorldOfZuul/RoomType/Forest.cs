using WorldOfZuul.Jobs;

namespace WorldOfZuul.RoomType
{
    public class Forest : Room
    {
        // one tree cut reduces SustainabilityPoints by 2
        // one animal lost reduces SustainabilityPoints by 1

        // Single Random instance to avoid creating new seeds on each call
        private static readonly Random Rng = new();

        public Forest(string shortDesc, string longDesc) : base(shortDesc, longDesc, new Lumberjack())
        {
        }

        public override void EnterRoom()
        {

            Console.Clear();
            // Display current state of the forest
            Console.WriteLine("Below are the current stats:");
            Console.WriteLine($"Trees: {Game.Resources.Trees}");
            Console.WriteLine($"Animals: {Game.Resources.Animals}");
            Console.WriteLine($"Sustainability Points: {Game.SustainabilityPoints}");
            Console.WriteLine();

            // List available actions for the player
            Console.WriteLine("Available actions:");
            Console.WriteLine("cut - cut tree    : Cut down one tree (reduces sustainability)");
            Console.WriteLine("plant - plant tree  : Plant a tree (increases sustainability)");
            Console.WriteLine("kill - kill animal : Kill one animal (reduces sustainability)");
            Console.WriteLine();


            Console.WriteLine("Type a command to perform the action.");
            Console.WriteLine();
        }

        public override void CommandList(Command command)
        {
            switch (command.Name)
            {
                case "cut":
                    Game.NextTurn(CutTree() ? 1 : 0);
                    break;
                case "plant":
                    Game.Resources.PlantSapling();
                    break;
                case "kill":
                    Game.NextTurn(KillAnimal() ? 1 : 0);
                    break;
                default:
                    Console.WriteLine("Invalid command in the forest.");
                    break;
            }
        }

        public bool CutTree(int amount = 1)
        {
            // If there are fewer trees left, inform the player
            if (Game.Resources.Trees < amount)
            {
                Console.WriteLine("No trees left to cut.");
                return false;
            }

            // Cut the amount of trees
            Game.Resources.Trees = -amount;
            Game.Resources.Wood = amount * Rng.Next(1, 5);
            Game.Resources.Saplings = amount * Rng.Next(1, 4);

            // Cutting a tree reduces sustainability
            Game.SustainabilityPoints -= 2;

            // If there are animals, randomly 1 to 3 disappear (but not more than current number of animals)
            if (Game.Resources.Animals > 0)
            {
                int animalsLost = Rng.Next(1, 4); // picks 1, 2 or 3
                animalsLost = Math.Min(animalsLost, Game.Resources.Animals); // don't remove more than exist
                Game.Resources.Animals = -animalsLost;

                // Each lost animal reduces SustainabilityPoints by 1 (weight can be adjusted)
                Game.SustainabilityPoints -= animalsLost;
            }

            // Hint to player about replanting
            Console.WriteLine("Consider planting a tree to maintain ecosystem balance.");
            return true;
        }

        public bool KillAnimal(int amount = 1)
        {
            // If there are no animals left, inform the player
            if (Game.Resources.Animals > amount)
            {
                Console.WriteLine("No animals left to kill.");
                return false;
            }

            // Kill one animal
            Game.Resources.Animals = -amount;
            Game.Resources.Food = amount * Rng.Next(1, 4);

            // Killing an animal reduces sustainability
            Game.SustainabilityPoints--;
            return true;
        }
    }
}
