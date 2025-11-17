using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    //NPC implementation
    public class Advisor
    {
        private readonly string _nickname = "Leafy Guide";
        private bool _introduced = false;

        private readonly List<string> _tips = new()
        {
            "Assign villagers to plant trees regularly; it keeps your village thriving.",
            "Balance fishing and farming; overfishing might deplete resources.",
            "Feed your villagers before assigning them tasks—they perform better when happy.",
            "Rotate crops to maintain soil quality.",
            "Use renewable jobs for villagers for long-term efficiency.",
            "Check the balance between animal population and hunting to avoid depletion."
        };

        private readonly List<string> _stats = new()
        {
            "Around 1/3 of all food produced globally is wasted each year.",
            "Every year, we lose about 10 million hectares of forests worldwide.",
            "If everyone recycled just 10% more, it would save over 50 million tons of CO2 annually.",
            "Globally, water usage in agriculture is expected to increase by 20% by 2050."
        };

        private readonly List<string> _challenges = new()
        {
            "Visit all rooms today without using sleep.",
            "Assign a villager to plant at least one tree.",
            "Keep all villagers fed for 2 consecutive days.",
            "Explore the lake and the forest in one turn."
        };

        private readonly List<string> _jokes = new()
        {
            "Why did the tree go to school? Because it wanted to be a little wiser!",
            "Why don’t fish use smartphones? They’re afraid of being caught in the net!",
            "Why did the tomato turn red? Because it saw the salad dressing!",
            "Why did the farmer plant a light bulb? He wanted to grow a power plant!",
            "Why did the forest apply for a job? It wanted to branch out!"
        };

        private int _tipIndex;
        private int _statsIndex = 0;
        private int _challengeIndex = 0;
        private int _jokeIndex = 0;

        private const double WORLD_TREES = 3.04e12;           // ~3.04 trillion trees
        private const double WORLD_ANIMALS = 35.0e9;          // ~35 billion livestock 
        private const double WORLD_CEREALS_TONNES = 2.99e9;   // ~2.99 billion tonnes cereals/year
        private const double WORLD_SEED_TONNES = WORLD_CEREALS_TONNES * 0.06; // ~6% used as seed
        private const double WORLD_ROUNDWOOD_M3 = 4.0e9;      // ~4.0 billion m³ roundwood removals/year
        private const double WORLD_SAPLINGS_PER_YEAR = 5.0e9; // ~5 billion trees planted/year

        public void Talk()
        {
            if (!_introduced)
            {
                Console.WriteLine($"Hello there! I’m the village guide, but you can call me '{_nickname}'.");
                Console.WriteLine("My role is to help you make smart decisions and guide you through this village.");
                _introduced = true;
            }

            Console.WriteLine("Type 'help' to see what I can do while we chat!");

            var chatting = true;
            while (chatting)
            {
                Console.Write("> ");
                var input = Console.ReadLine()?.ToLower();

                switch (input)
                {
                    case "help":
                        Console.WriteLine("Available commands:");
                        Console.WriteLine("- stats       Show real-world sustainability statistics");
                        Console.WriteLine("- tip         Get a gameplay tip (loops sequentially)");
                        Console.WriteLine("- challenge   Receive a fun mini-challenge for the village");
                        Console.WriteLine("- joke        Hear a sustainability-related joke");
                        Console.WriteLine("- exit        Exit the advisor chat");
                        Console.WriteLine("- resources   Show current village resources + Sustainability Points");
                        Console.WriteLine("- world       Compare village numbers to world scale (1 unit = 1% of world)");
                        break;

                    case "stats":
                        Console.WriteLine(_stats[_statsIndex]);
                        _statsIndex = (_statsIndex + 1) % _stats.Count;
                        break;

                    case "tip":
                        Console.WriteLine(_tips[_tipIndex]);
                        _tipIndex = (_tipIndex + 1) % _tips.Count;
                        break;

                    case "challenge":
                        Console.WriteLine(_challenges[_challengeIndex]);
                        _challengeIndex = (_challengeIndex + 1) % _challenges.Count;
                        break;

                    case "joke":
                        Console.WriteLine(_jokes[_jokeIndex]);
                        _jokeIndex = (_jokeIndex + 1) % _jokes.Count;
                        break;

                    case "exit":
                        Console.WriteLine("Good luck! Remember, sustainability is key.");
                        chatting = false;
                        break;


                    case "resources":
                        ShowSimpleResources();
                        break;

                    case "world":
                        ShowWorldComparison();
                        break;

                    default:
                        Console.WriteLine("I didn’t understand that. Type 'help' to see your options.");
                        break;
                }
            }
        }
        private static void ShowSimpleResources()
        {
            Console.WriteLine("=== Current Resources ===");
            Console.WriteLine($"Sustainability Points : {Game.SustainabilityPoints}");
            Console.WriteLine($"Food                  : {Game.Resources.Food}");
            Console.WriteLine($"Hunger                : {Game.Resources.Hunger}");
            Console.WriteLine($"Trees                 : {Game.Resources.Trees}");
            Console.WriteLine($"Animals               : {Game.Resources.Animals}");
            Console.WriteLine($"Wood                  : {Game.Resources.Wood}");
            Console.WriteLine($"Saplings              : {Game.Resources.Saplings}");
            Console.WriteLine($"Grains                : {Game.Resources.Grains}");
            Console.WriteLine($"GrainSeeds            : {Game.Resources.GrainSeeds}");
            Console.WriteLine();
        }

        private static void ShowWorldComparison()
        {
            int trees     = Math.Max(0, Game.Resources.Trees);
            int animals   = Math.Max(0, Game.Resources.Animals);
            int grains    = Math.Max(0, Game.Resources.Grains);
            int grainSeed = Math.Max(0, Game.Resources.GrainSeeds);
            int wood      = Math.Max(0, Game.Resources.Wood);
            int saplings  = Math.Max(0, Game.Resources.Saplings);

            Console.WriteLine("=== Village → World (1 unit = 1% of world) ===");

            // Trees
            double treesWorld = (trees / 100.0) * WORLD_TREES;
            Console.WriteLine($"Trees: {trees} → ~{trees}% of world (~{treesWorld:N0} trees)");

            // Animals 
            double animalsWorld = (animals / 100.0) * WORLD_ANIMALS;
            Console.WriteLine($"Animals: {animals} → ~{animals}% of world (~{animalsWorld:N0} animals)");

            // Grains 
            double grainsWorldTonnes = (grains / 100.0) * WORLD_CEREALS_TONNES;
            Console.WriteLine($"Grains: {grains} → ~{grains}% of world cereals (~{grainsWorldTonnes:N0} tonnes/year)");

            // Grain seeds 
            double seedWorldTonnes = (grainSeed / 100.0) * WORLD_SEED_TONNES;
            Console.WriteLine($"GrainSeeds: {grainSeed} → ~{grainSeed}% of seed use (~{seedWorldTonnes:N0} tonnes/year)");

            // Wood 
            double woodWorldM3 = (wood / 100.0) * WORLD_ROUNDWOOD_M3;
            Console.WriteLine($"Wood: {wood} → ~{wood}% of world removals (~{woodWorldM3:N0} m³/year)");

            // Saplings 
            double saplingsWorld = (saplings / 100.0) * WORLD_SAPLINGS_PER_YEAR;
            Console.WriteLine($"Saplings: {saplings} → ~{saplings}% of world planting (~{saplingsWorld:N0} trees/year)");

            Console.WriteLine();
            Console.WriteLine("Tip: Planting supports long-term balance; moderate hunting and smart storage reduce waste.");
        }
    }
}
