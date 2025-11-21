using WorldOfZuul.Jobs;

namespace WorldOfZuul.RoomType
{
    public class Forest : Room
    {
         // one tree cut reduces SustainabilityPoints by 2
         // one animal lost reduces SustainabilityPoints by 1

        // Single Random instance to avoid creating new seeds on each call
        private static readonly Random Rng = new();

        public Forest(string shortDesc, string longDesc) : base(shortDesc, longDesc, new Lumberjack(1, 1))
        {
        }

        public override void EnterRoom()
        {
           
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
                    CutTree();
                    break;
                case "plant":
                    PlantTree();
                    break;
                case "kill":
                    KillAnimal();
                    break;
                default:
                    Console.WriteLine("Invalid command in the forest.");
                    break;
            }
        }



        public void CutTree(int amount = 1)
        {
            // If there are fewer trees left, inform the player
            if (Game.Resources.Trees < amount)
            {
                Console.WriteLine("No trees left to cut.");
                return;
            }

            // Cut the amount of trees
            Game.Resources.Trees = -amount;

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
        }

        private void PlantTree()
        {
            Game.Resources.Trees = 1;
            Game.SustainabilityPoints += 2;
            Console.WriteLine($"You planted a tree. Trees remaining: {Game.Resources.Trees}.");
        }

        public void KillAnimal(int amount = 1)
        {
            // If there are no animals left, inform the player
            if (Game.Resources.Animals > amount)
            {
                Console.WriteLine("No animals left to kill.");
                return;
            }

            // Kill one animal
            Game.Resources.Animals = -amount;

            // Killing an animal reduces sustainability
            Game.SustainabilityPoints--;

            // Prevent SustainabilityPoints from going negative
        }

    }
}
