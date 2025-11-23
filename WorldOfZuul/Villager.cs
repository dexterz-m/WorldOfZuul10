using System.Runtime.CompilerServices;

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
            if (Game.Resources.Food < foodAmount)
            {
                Console.WriteLine("Not enough food available to feed the villager.");
                Console.WriteLine($"Remaining Food / Tried to feed: {Game.Resources.Food}/{{foodAmount}}");
                return;
            }

            if (Hunger + foodAmount * 10 > 100)
            {
                Game.Resources.Food -= (100 - Hunger) / 10;
                Hunger = 100;
                Console.WriteLine($"You can't overfeed a villager. Using only {foodAmount}");
            }
            else
            {
                Game.Resources.Food -= foodAmount;
                Hunger += foodAmount * 10;
            }
            Game.NextTurn();
            Work();
        }
        
        public void Starve(int turn = 1)
        {
            Hunger -= turn * 10;
            if (Hunger < 0) Hunger = 0;
            Work();
        }

        private void Work()
        {
            CanWork = this.Hunger > 25;
        }
    }


}
