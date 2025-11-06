using System;

namespace WorldOfZuul
{
    public abstract class Item
    {
        public string Name { get; }
        public string Description { get; }
        public int Quantity { get; protected set; }

        protected Item(string name, string description, int quantity)
        {
            Name = name;
            Description = description;
            Quantity = quantity < 0 ? 0 : quantity;
        }
        public virtual void Add(int amount)
        {
            if (amount <= 0) return;
            Quantity += amount;
        }
        public virtual int Remove(int amount)
        {
            if (amount <= 0) return 0;
            int removed = amount > Quantity ? Quantity : amount;
            Quantity -= removed;
            return removed;
        }
        
        public virtual void Tick(Resources resources) { }
    }
}