using System.Collections.Generic;
using System.Linq;

namespace WorldOfZuul.Domain;

public class Resources
{
    public int SustainabilityPoints { get; set; }

    private int _food = 10;
    private int _grainSeeds = 2;
    private int _hunger = 0;

    private int _animals = 100;
    private int _trees = 100;
    private int _wood = 0;
    private int _saplings = 0;

    private List<int> _plantedSaplings = new List<int>();

    public int Food
    {
        get => _food;
        set => _food = value;
    }

    public int GrainSeeds
    {
        get => _grainSeeds;
        set => _grainSeeds = value;
    }

    public int Hunger
    {
        get => _hunger;
        set => _hunger = value;
    }

    public int Animals
    {
        get => _animals;
        set => _animals = value;
    }

    public int Trees
    {
        get => _trees;
        set => _trees = value;
    }

    public int Wood
    {
        get => _wood;
        set => _wood = value;
    }

    public int Saplings
    {
        get => _saplings;
        set => _saplings = value;
    }

    public bool TryPlantSapling(int currentTurn)
    {
        if (Saplings <= 0) return false;
        Saplings -= 1;
        _plantedSaplings.Add(currentTurn);
        return true;
    }

    public int TurnToTrees(int currentTurn)
    {
        int grown = 0;

        foreach (var plantedAt in _plantedSaplings.Where(t => currentTurn - t >= 3).ToList())
        {
            Trees += 1;
            SustainabilityPoints += 1;
            _plantedSaplings.Remove(plantedAt);
            grown++;
        }

        return grown;
    }
}
