using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul.RoomType
{
    public class Forest : Room
    {
        int trees { get; set; } = 20; // one tree cut reduces SustainabilityPoints by 2
        int animals { get; set; } = 10; // one animal lost reduces SustainabilityPoints by 1

        // Single Random instance to avoid creating new seeds on each call
        private static readonly Random rng = new();

        public Forest(string shortDesc, string longDesc) : base(shortDesc, longDesc)
        {

        }

        public override void EnterRoom()
        {
           
            // Display current state of the forest
            Console.WriteLine("Below are the current stats:");
            Console.WriteLine($"Trees: {trees}");
            Console.WriteLine($"Animals: {animals}");
            Console.WriteLine($"Sustainability Points: {SustainabilityPoints}");
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



        private void CutTree()
        {
            // If there are no trees left, inform the player
            if (trees <= 0)
            {
                Console.WriteLine("No trees left to cut.");
                return;
            }

            // Cut one tree
            trees--;

            // Cutting a tree reduces sustainability
            SustainabilityPoints -= 2;

            Console.WriteLine($"You cut a tree. Trees remaining: {trees}.");

            // If there are animals, randomly 1 to 3 disappear (but not more than current number of animals)
            if (animals > 0)
            {
                int animalsLost = rng.Next(1, 4); // picks 1, 2 or 3
                animalsLost = Math.Min(animalsLost, animals); // don't remove more than exist
                animals -= animalsLost;

                // Each lost animal reduces SustainabilityPoints by 1 (weight can be adjusted)
                SustainabilityPoints -= animalsLost;

                Console.WriteLine($"{animalsLost} animals left the forest. Animals remaining: {animals}.");
            }

            Console.WriteLine($"Sustainability Points: {SustainabilityPoints}");

            

            // Hint to player about replanting
            Console.WriteLine("Consider planting a tree to maintain ecosystem balance.");
        }

        private void PlantTree()
        {
            trees++;
            SustainabilityPoints += 2;
            Console.WriteLine($"You planted a tree. Trees remaining: {trees}.");
        }

        private void KillAnimal()
        {
            // If there are no animals left, inform the player
            if (animals <= 0)
            {
                Console.WriteLine("No animals left to kill.");
                return;
            }

            // Kill one animal
            animals--;

            // Killing an animal reduces sustainability
            SustainabilityPoints--;

            Console.WriteLine($"You killed an animal. Animals remaining: {animals}.");
            Console.WriteLine($"Sustainability Points: {SustainabilityPoints}");

            // Prevent SustainabilityPoints from going negative
        }

    }
}
