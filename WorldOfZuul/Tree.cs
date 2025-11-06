using System;

namespace WorldOfZuul
{
    public class Tree : Item
    {
        public int WoodYieldPerTree { get; }
        public int SaplingsPerTree { get; }
        public int AverageAgeDays { get; private set; }
        public int MaturityDays { get; }

        public Tree(int quantity, int woodYieldPerTree = 1, int saplingsPerTree = 1, int maturityDays = 30)
            : base("Tree", "A stand of trees that can be cut for wood and saplings.", quantity)
        {
            WoodYieldPerTree = woodYieldPerTree;
            SaplingsPerTree = saplingsPerTree;
            MaturityDays = maturityDays;
        }

        public int Cut(Resources resources, int amount)
        {
            int cut = Remove(amount);
            if (cut <= 0) return 0;

            resources.Wood = WoodYieldPerTree * cut;
            resources.Saplings = SaplingsPerTree * cut;
            return cut;
        }
        
        public void Plant(int amount)
        {
            Add(amount);
        }

        public override void Tick(Resources resources)
        {
            if (Quantity <= 0) return;

            AverageAgeDays += 1;

            if (AverageAgeDays >= MaturityDays)
            {
                int naturalSaplings = Quantity / 10;
                if (naturalSaplings > 0)
                {
                    resources.Saplings = naturalSaplings;
                }
            }
        }
    }
}