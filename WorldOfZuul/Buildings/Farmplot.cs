using System;
using WorldOfZuul;

namespace WorldOfZuul.Buildings
{
    public class FarmPlot : Building
    {
        private double _fertility; // 0..1
        private double _growth;    // 0..1
        private bool _isPlanted;

        public double Fertility => _fertility;
        public bool IsPlanted => _isPlanted;
        public double Growth => _growth;

        public FarmPlot(double initialFertility = 0.8)
            : base("Farm Plot",
                   "A small cultivated plot for growing grains.",
                   100,
                   3,
                   new ResourceCost { Wood = 4, Saplings = 2 },
                   new ResourceCost { Food = 0 })
        {
            if (initialFertility < 0) initialFertility = 0;
            if (initialFertility > 1) initialFertility = 1;
            _fertility = initialFertility;
            _growth = 0.0;
            _isPlanted = false;
        }
        public bool Plant(Resources resources, int seedCost = 1)
        {
            if (_isPlanted) return false;

            var cost = new ResourceCost { GrainSeeds = seedCost };
            if (!cost.TryPay(resources)) return false;

            _isPlanted = true;
            _growth = 0.0;
            return true;
        }
        public int Harvest(Resources resources)
        {
            if (!_isPlanted || _growth < 1.0) return 0;

            double staffBonus = 1.0 + (0.25 * this.Staff.Count);
            int yield = (int)Math.Max(1, Math.Round(10 * _fertility * staffBonus));
            resources.Grains = yield;

            _isPlanted = false;
            _growth = 0.0;

            _fertility -= 0.05 * staffBonus;
            if (_fertility < 0) _fertility = 0;

            return yield;
        }

        public override void Tick(Resources resources)
        {
            if (!IsOperational) return;
            this.Degrade(1);

            if (_isPlanted)
            {
                double staffBonus = 1.0 + (0.25 * this.Staff.Count);
                _growth += 0.2 * _fertility * staffBonus;
                if (_growth > 1.0) _growth = 1.0;

                // Intensive farming slowly depletes fertility
                if (this.Staff.Count > 0)
                {
                    _fertility -= 0.002 * this.Staff.Count;
                    if (_fertility < 0) _fertility = 0;
                }
            }
        }
        public void Rest()
        {
            _isPlanted = false;
            _growth = 0.0;
            _fertility += 0.02;
            if (_fertility > 1.0) _fertility = 1.0;
        }
    }
}
