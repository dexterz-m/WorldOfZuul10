using System;
using WorldOfZuul;

namespace WorldOfZuul.Buildings
{
    public class Granary : Building
    {
        public int Capacity { get; }
        public int StoredGrain { get; private set; }

        public Granary(int capacity = 200)
            : base("Granary",
                   "Stores harvested grain and protects it from spoilage.",
                   100,
                   2,
                   new ResourceCost { Wood = 12 },
                   new ResourceCost { Food = 0 })
        {
            if (capacity < 0) capacity = 0;
            Capacity = capacity;
            StoredGrain = 0;
        }
        public int StoreFromResources(Resources resources, int amount)
        {
            if (amount <= 0) return 0;
            int space = Capacity - StoredGrain;
            if (space <= 0) return 0;

            int available = resources.Grains;
            int toMove = amount > available ? available : amount;
            if (toMove > space) toMove = space;
            if (toMove <= 0) return 0;

            resources.Grains = -toMove;
            StoredGrain += toMove;

            return toMove;
        }

        public int RetrieveToResources(Resources resources, int amount)
        {
            if (amount <= 0) return 0;
            int toGive = amount > StoredGrain ? StoredGrain : amount;
            if (toGive <= 0) return 0;

            StoredGrain -= toGive;
            resources.Grains = toGive;
            return toGive;
        }

        public override void Tick(Resources resources)
        {
            if (!IsOperational) return;

            this.Degrade(1);
            if (StoredGrain > 0)
            {
                int spoilage = (int)Math.Floor(StoredGrain * 0.01);
                if (spoilage > 0) StoredGrain -= spoilage;
            }
        }
    }
}
