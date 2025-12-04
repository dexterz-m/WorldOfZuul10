namespace WorldOfZuul.Domain;

public class Advisor
{
    private readonly string _nickname = "Leafy Guide";
    private bool _introduced = false;

    private readonly List<string> _tips = new()
    {
        "Assign villagers to plant trees regularly; it keeps your village thriving.",
        "Balance fishing and farming; overfishing might deplete resources.",
        "Feed your villagers before assigning them tasks—they perform better when happy.",
        "Rotate crops to maintain soil quality.",
        "Use renewable jobs for villagers for long-term efficiency.",
        "Check the balance between animal population and hunting to avoid depletion."
    };

    private readonly List<string> _stats = new()
    {
        "Around 1/3 of all food produced globally is wasted each year.",
        "Every year, we lose about 10 million hectares of forests worldwide.",
        "If everyone recycled just 10% more, it would save over 50 million tons of CO2 annually.",
        "Globally, water usage in agriculture is expected to increase by 20% by 2050."
    };

    private readonly List<string> _challenges = new()
    {
        "Visit all rooms today without using sleep.",
        "Assign a villager to plant at least one tree.",
        "Keep all villagers fed for 2 consecutive days.",
        "Explore the lake and the forest in one turn."
    };

    private readonly List<string> _jokes = new()
    {
        "Why did the tree go to school? Because it wanted to be a little wiser!",
        "Why don't fish use smartphones? They're afraid of being caught in the net!",
        "Why did the tomato turn red? Because it saw the salad dressing!",
        "Why did the farmer plant a light bulb? He wanted to grow a power plant!",
        "Why did the forest apply for a job? It wanted to branch out!"
    };

    private int _tipIndex;
    private int _statsIndex = 0;
    private int _challengeIndex = 0;
    private int _jokeIndex = 0;

    private const double WORLD_TREES = 3.04e12;
    private const double WORLD_ANIMALS = 35.0e9;
    private const double WORLD_CEREALS_TONNES = 2.99e9;
    private const double WORLD_SEED_TONNES = WORLD_CEREALS_TONNES * 0.06;
    private const double WORLD_ROUNDWOOD_M3 = 4.0e9;
    private const double WORLD_SAPLINGS_PER_YEAR = 5.0e9;

    public string Nickname => _nickname;
    public bool IsIntroduced => _introduced;

    public void MarkAsIntroduced()
    {
        _introduced = true;
    }

    public string GetIntroduction()
    {
        return $"Hello there! I'm the village guide, but you can call me '{_nickname}'.\n" +
               "My role is to help you make smart decisions and guide you through this village.";
    }

    public string GetHelpText()
    {
        return "Available commands:\n" +
               "- stats       Show real-world sustainability statistics\n" +
               "- tip         Get a gameplay tip (loops sequentially)\n" +
               "- challenge   Receive a fun mini-challenge for the village\n" +
               "- joke        Hear a sustainability-related joke\n" +
               "- exit        Exit the advisor chat\n" +
               "- resources   Show current village resources + Sustainability Points\n" +
               "- world       Compare village numbers to world scale (1 unit = 1% of world)";
    }

    public string GetNextStat()
    {
        string stat = _stats[_statsIndex];
        _statsIndex = (_statsIndex + 1) % _stats.Count;
        return stat;
    }

    public string GetNextTip()
    {
        string tip = _tips[_tipIndex];
        _tipIndex = (_tipIndex + 1) % _tips.Count;
        return tip;
    }

    public string GetNextChallenge()
    {
        string challenge = _challenges[_challengeIndex];
        _challengeIndex = (_challengeIndex + 1) % _challenges.Count;
        return challenge;
    }

    public string GetNextJoke()
    {
        string joke = _jokes[_jokeIndex];
        _jokeIndex = (_jokeIndex + 1) % _jokes.Count;
        return joke;
    }

    public string GetExitMessage()
    {
        return "Good luck! Remember, sustainability is key.";
    }

    public string FormatResources(Resources resources, int sustainabilityPoints)
    {
        return "=== Current Resources ===\n" +
               $"Sustainability Points : {sustainabilityPoints}\n" +
               $"Food                  : {resources.Food}\n" +
               $"Hunger                : {resources.Hunger}\n" +
               $"Trees                 : {resources.Trees}\n" +
               $"Animals               : {resources.Animals}\n" +
               $"Wood                  : {resources.Wood}\n" +
               $"Saplings              : {resources.Saplings}\n" +
               $"GrainSeeds            : {resources.GrainSeeds}\n";
    }

    public string GetWorldComparison(Resources resources)
    {
        int trees = Math.Max(0, resources.Trees);
        int animals = Math.Max(0, resources.Animals);
        int grainSeed = Math.Max(0, resources.GrainSeeds);
        int wood = Math.Max(0, resources.Wood);
        int saplings = Math.Max(0, resources.Saplings);

        double treesWorld = (trees / 100.0) * WORLD_TREES;
        double animalsWorld = (animals / 100.0) * WORLD_ANIMALS;
        double seedWorldTonnes = (grainSeed / 100.0) * WORLD_SEED_TONNES;
        double woodWorldM3 = (wood / 100.0) * WORLD_ROUNDWOOD_M3;
        double saplingsWorld = (saplings / 100.0) * WORLD_SAPLINGS_PER_YEAR;

        return "=== Village → World (1 unit = 1% of world) ===\n" +
               $"Trees: {trees} → ~{trees}% of world (~{treesWorld:N0} trees)\n" +
               $"Animals: {animals} → ~{animals}% of world (~{animalsWorld:N0} animals)\n" +
               $"GrainSeeds: {grainSeed} → ~{grainSeed}% of seed use (~{seedWorldTonnes:N0} tonnes/year)\n" +
               $"Wood: {wood} → ~{wood}% of world removals (~{woodWorldM3:N0} m³/year)\n" +
               $"Saplings: {saplings} → ~{saplings}% of world planting (~{saplingsWorld:N0} trees/year)\n" +
               "\nTip: Planting supports long-term balance; moderate hunting and smart storage reduce waste.";
    }
}