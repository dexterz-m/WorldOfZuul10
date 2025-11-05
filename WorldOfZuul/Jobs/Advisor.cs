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
        private readonly string nickname = "Leafy Guide";
        private bool introduced = false;

        private readonly List<string> tips = new()
        {
            "Assign villagers to plant trees regularly; it keeps your village thriving.",
            "Balance fishing and farming; overfishing might deplete resources.",
            "Feed your villagers before assigning them tasks—they perform better when happy.",
            "Rotate crops to maintain soil quality.",
            "Use renewable jobs for villagers for long-term efficiency.",
            "Check the balance between animal population and hunting to avoid depletion."
        };

        private readonly List<string> stats = new()
        {
            "Around 1/3 of all food produced globally is wasted each year.",
            "Every year, we lose about 10 million hectares of forests worldwide.",
            "If everyone recycled just 10% more, it would save over 50 million tons of CO2 annually.",
            "Globally, water usage in agriculture is expected to increase by 20% by 2050."
        };

        private readonly List<string> challenges = new()
        {
            "Visit all rooms today without using sleep.",
            "Assign a villager to plant at least one tree.",
            "Keep all villagers fed for 2 consecutive days.",
            "Explore the lake and the forest in one turn."
        };

        private readonly List<string> jokes = new()
        {
            "Why did the tree go to school? Because it wanted to be a little wiser!",
            "Why don’t fish use smartphones? They’re afraid of being caught in the net!",
            "Why did the tomato turn red? Because it saw the salad dressing!",
            "Why did the farmer plant a light bulb? He wanted to grow a power plant!",
            "Why did the forest apply for a job? It wanted to branch out!"
        };

        private int tipIndex = 0;
        private int statsIndex = 0;
        private int challengeIndex = 0;
        private int jokeIndex = 0;

        public void Talk()
        {
            if (!introduced)
            {
                Console.WriteLine($"Hello there! I’m the village guide, but you can call me '{nickname}'.");
                Console.WriteLine("My role is to help you make smart decisions and guide you through this village.");
                introduced = true;
            }

            Console.WriteLine("Type 'help' to see what I can do while we chat!");

            bool chatting = true;
            while (chatting)
            {
                Console.Write("> ");
                string? input = Console.ReadLine()?.ToLower();

                switch (input)
                {
                    case "help":
                        Console.WriteLine("Available commands:");
                        Console.WriteLine("- stats       Show real-world sustainability statistics");
                        Console.WriteLine("- tip         Get a gameplay tip (loops sequentially)");
                        Console.WriteLine("- challenge   Receive a fun mini-challenge for the village");
                        Console.WriteLine("- joke        Hear a sustainability-related joke");
                        Console.WriteLine("- exit        Exit the advisor chat");
                        break;

                    case "stats":
                        Console.WriteLine(stats[statsIndex]);
                        statsIndex = (statsIndex + 1) % stats.Count;
                        break;

                    case "tip":
                        Console.WriteLine(tips[tipIndex]);
                        tipIndex = (tipIndex + 1) % tips.Count;
                        break;

                    case "challenge":
                        Console.WriteLine(challenges[challengeIndex]);
                        challengeIndex = (challengeIndex + 1) % challenges.Count;
                        break;

                    case "joke":
                        Console.WriteLine(jokes[jokeIndex]);
                        jokeIndex = (jokeIndex + 1) % jokes.Count;
                        break;

                    case "exit":
                        Console.WriteLine("Good luck! Remember, sustainability is key.");
                        chatting = false;
                        break;

                    default:
                        Console.WriteLine("I didn’t understand that. Type 'help' to see your options.");
                        break;
                }
            }
        }
    }
}
