using System;

namespace WorldOfZuul
{
    public class Villager
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Hunger { get; set; } = 0;
        public bool CanWork { get; set; }

        public Villager(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Villager(int id, string name, int hunger) : this(id, name)
        {
            Hunger = hunger;
            Work();
        }

        public void Feed(int foodAmount)
        {
            var resources = WorldOfZuul.Domain.DataHandler.Instance?.Resources;
            if (resources == null)
            {
                Console.WriteLine("Resources are not initialized.");
                return;
            }

            if (foodAmount <= 0)
            {
                Console.WriteLine("Food amount must be greater than 0.");
                return;
            }

            if (resources.Food < foodAmount)
            {
                Console.WriteLine("Not enough food available to feed the villager.");
                Console.WriteLine($"Remaining Food / Tried to feed: {resources.Food}/{foodAmount}");
                return;
            }

            int hungerNeeded = 100 - Hunger;
            if (hungerNeeded <= 0)
            {
                Console.WriteLine("This villager is already full.");
                return;
            }

            int maxFoodUsable = (int)Math.Ceiling(hungerNeeded / 10.0);
            int foodUsed = Math.Min(foodAmount, maxFoodUsable);

            resources.Food -= foodUsed;
            Hunger = Math.Min(100, Hunger + foodUsed * 10);

            if (foodUsed < foodAmount)
            {
                Console.WriteLine($"You can't overfeed a villager. Using only {foodUsed}");
            }

            Work();
        }

        public bool Starve(int turn = 1)
        {
            Hunger -= turn * 2;
            if (Hunger < 0) Hunger = 0;
            return Work();
        }

        private bool Work()
        {
            var latestCanWork = CanWork;
            CanWork = Hunger >= 25;
            return latestCanWork && !CanWork;
        }
    }
}
