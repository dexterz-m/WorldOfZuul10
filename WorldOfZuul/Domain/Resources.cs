namespace WorldOfZuul.Domain;

public class Resources
{
    public int SustainabilityPoints { get; set; }

    // Food and farming variables
    private int _food = 10;
    private int _grainSeeds = 2;
    private int _hunger = 0;

    // animals and forest variables 
    private int _animals = 100;
    private int _trees = 100;
    private int _wood = 0;
    private int _saplings = 0;

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
}