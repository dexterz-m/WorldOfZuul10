using WorldOfZuul.Domain.CommandHandler;

namespace WorldOfZuul.Domain.Rooms;

public class Farmland : Room
{
    private int FarmlandAmount { get; set; }
    private int PossibleFarmland { get; set; } = 1;
    public int FarmlandRipped { get; private set; } = 0;
    private List<int> FarmlandPlanted { get; set; } = new List<int>();

    private Resources _resources = new Resources();

    public Farmland(string shortDesc, string longDesc) : base(shortDesc, longDesc)
    {
        FarmlandAmount = 1;
    }

    public override string GetEnterRoomMessage()
    {
        return "You have entered the Farmland.\n\nBelow are the current stats:\n" +
               $"Farmlands: {FarmlandAmount}\n" +
               $"Free farmlands: {PossibleFarmland - FarmlandAmount}\n" +
               $"Planted farmlands: {FarmlandPlanted.Count}\n" +
               $"Ripe farmlands: {FarmlandRipped}\n\n" +
               "Available actions:\n" +
               " - build farmland           : Build a new farmland\n" +
               " - cut forest               : Cut 5 trees to make freeland (reduces sustainability)\n" +
               " - plant farmland           : Plant on your farmland\n" +
               " - harvest                  : Harvest your ripe farmland\n";
    }

    public override string RoomCommandHandler(Command command, Resources resources)
    {
        _resources = resources;

        switch (command.Name)
        {
            case "build":
                if (command.SecondWord == "farmland")
                    return BuildFarmland();
                else
                    return "Build what?";
            case "cut":
                if (command.SecondWord == "forest")
                    return CutForest();
                else
                    return "Cut what?";
            case "harvest":
                return Harvest();
            case "plant":
                if (command.SecondWord == "farmland")
                    return PlantFarmland();
                else
                    return "Plant what?";
                
            default:
                return "Invalid command in the farmland.";
        }
    }

    public string BuildFarmland()
    {
        if (_resources.Wood >= 5 && FarmlandAmount < PossibleFarmland)
        {
            FarmlandAmount += 1;
            _resources.Wood -= 5;
            return $"You have built a new farmland. Now you have: {FarmlandAmount} farmlands.";
        }
        else if (_resources.Wood < 5 && FarmlandAmount == PossibleFarmland)
        {
            return "You dont have enough wood and freeland to build farmland!!";
        }
        else if (_resources.Wood < 5)
        {
            return "You dont have enough wood to build farmland!!";
        }
        else
        {
            return "You dont have enough freeland to build farmland!!";
        }
    }

    public string CutForest()
    {
        if (PossibleFarmland > FarmlandAmount)
        {
            return "There is freeland no need to cut more trees for now.";
        }

        if (_resources.Trees <= 0)
        {
            return "No trees left to cut.";
        }

        _resources.Trees -= 5;
        _resources.Wood += 10;
        PossibleFarmland += 1;
        _resources.SustainabilityPoints += 10;

        return "You now have space for 1 more farmland.";
    }

    public string PlantFarmland()
    {
        if (FarmlandPlanted.Count < FarmlandAmount && _resources.GrainSeeds >= 4)
        {
            int plantedAt = DataHandler.Instance?.TotalTurns ?? 0;
            FarmlandPlanted.Add(plantedAt);
            _resources.GrainSeeds -= 4;
            _resources.SustainabilityPoints += 8;

            return $"You have planted 1 more farmland.\nNow you have {FarmlandPlanted.Count} planted farmlands.";
        }
        else
        {
            if (FarmlandPlanted.Count == FarmlandAmount && _resources.GrainSeeds < 4)
            {
                return "All your farmlands are planted and you dont have enough Grain seeds to plant a farmland";
            }
            else if (FarmlandPlanted.Count == FarmlandAmount)
            {
                return "All your farmlands are planted.";
            }
            else
            {
                return "You dont have enough Grain seeds to plant a farmland";
            }
        }
    }

    public string Harvest()
    {
        if (FarmlandRipped > 0)
        {
            FarmlandRipped -= 1;
            _resources.GrainSeeds += 1;
            _resources.Food += 4;

            return "Harvested! Now you have " + FarmlandRipped + " ripe farmlands left.";
        }
        else
        {
            return "None of your farmlands are ripe yet.";
        }
    }

    public void RipenFarmland(int currentTurn)
    {
        foreach (var farmland in FarmlandPlanted.Where(f => currentTurn - f >= 4).ToList())
        {
            FarmlandRipped += 1;
            FarmlandPlanted.Remove(farmland);
        }
    }
}
