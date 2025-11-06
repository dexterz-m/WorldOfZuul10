using System.Collections.Generic;

namespace WorldOfZuul
{
    public abstract class Building
    {
        public string Name { get; }
        public string Description { get; }
        public int Durability { get; private set; } // 0..100
        public int StaffCapacity { get; }
        public List<Villager> Staff { get; } = new List<Villager>();
        public ResourceCost BuildCost { get; }
        public ResourceCost MaintenanceCost { get; }

        protected Building(string name, string description, int durability, int staffCapacity, ResourceCost buildCost, ResourceCost maintenanceCost)
        {
            Name = name;
            Description = description;
            Durability = durability < 0 ? 0 : (durability > 100 ? 100 : durability);
            StaffCapacity = staffCapacity < 0 ? 0 : staffCapacity;
            BuildCost = buildCost;
            MaintenanceCost = maintenanceCost;
        }

        public bool IsOperational => Durability > 0;
        public bool TryBuild(Resources resources)
        {
            return BuildCost.TryPay(resources);
        }
        
        public bool Maintain(Resources resources)
        {
            if (!IsOperational) return false;
            if (!MaintenanceCost.TryPay(resources)) return false;
            Repair(1);
            return true;
        }
        
        public void Repair(int amount)
        {
            if (amount <= 0) return;
            int next = Durability + amount;
            Durability = next > 100 ? 100 : next;
        }
        
        public void Degrade(int amount)
        {
            if (amount <= 0) return;
            int next = Durability - amount;
            Durability = next < 0 ? 0 : next;
        }
        public virtual bool AssignVillager(Villager v)
        {
            if (Staff.Count >= StaffCapacity) return false;
            Staff.Add(v);
            return true;
        }
        
        public virtual void UnassignVillager(Villager v)
        {
            Staff.Remove(v);
        }
        public virtual void Tick(Resources resources) { }
    }
    
    public struct ResourceCost
    {
        public int Food;
        public int GrainSeeds;
        public int Grains;
        public int Animals;
        public int Trees;
        public int Wood;
        public int Saplings;

        public bool CanAfford(Resources resources)
        {
            if (Food > 0 && resources.Food < Food) return false;
            if (GrainSeeds > 0 && resources.GrainSeeds < GrainSeeds) return false;
            if (Grains > 0 && resources.Grains < Grains) return false;
            if (Animals > 0 && resources.Animals < Animals) return false;
            if (Trees > 0 && resources.Trees < Trees) return false;
            if (Wood > 0 && resources.Wood < Wood) return false;
            if (Saplings > 0 && resources.Saplings < Saplings) return false;
            return true;
        }

        public bool TryPay(Resources resources)
        {
            if (!CanAfford(resources)) return false;

            if (Food > 0) resources.Food = -Food;
            if (GrainSeeds > 0) resources.GrainSeeds = -GrainSeeds;
            if (Grains > 0) resources.Grains = -Grains;
            if (Animals > 0) resources.Animals = -Animals;
            if (Trees > 0) resources.Trees = -Trees;
            if (Wood > 0) resources.Wood = -Wood;
            if (Saplings > 0) resources.Saplings = -Saplings;

            return true;
        }
    }
}
