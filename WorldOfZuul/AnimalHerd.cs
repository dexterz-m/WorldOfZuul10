using System;

namespace WorldOfZuul
{
    public class AnimalHerd : Item
    {
        public int CarryingCapacity { get; }
        public double ReproductionRate { get; } // per turn

        public AnimalHerd(int population, int carryingCapacity = 200, double reproductionRate = 0.05)
            : base("Animal Herd", "A herd of wild animals that can be hunted for food.", population)
        {
            CarryingCapacity = carryingCapacity;
            ReproductionRate = reproductionRate;
        }

  
        public int Hunt(Resources resources, int amount)
        {
            int taken = Remove(amount);
            if (taken > 0)
            {
                resources.Food = taken;
            }
            return taken;
        }


        public int RemoveAnimals(int amount)
        {
            return Remove(amount);
        }

        public override void Tick(Resources resources)
        {
            if (Quantity <= 0) return;

            double q = Quantity;
            double k = CarryingCapacity <= 0 ? q : CarryingCapacity;
            if (k <= 0) k = q;

            double growth = ReproductionRate * q * (1.0 - (q / k));
            int delta = (int)Math.Floor(growth);
            if (delta > 0) Add(delta);
        }
    }
}