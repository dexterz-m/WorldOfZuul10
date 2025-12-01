using WorldOfZuul.ConsoleUi;
using WorldOfZuul.Domain.CommandHandler;

namespace WorldOfZuul.Domain.Rooms;

public class Farmland : Room
{
    public int FarmlandAmount { get; private set; }
    public int PossibleFarmland { get; private set; } = 1;
    private int FarmlandRipped { get; set; } = 0;
    private List<int> FarmlandPlanted { get; set; } = new List<int>();

    Random Rnd = new Random();

    public Farmland(string shortDesc, string longDesc) : base(shortDesc, longDesc)
    {
        FarmlandAmount = 1;
    }

    public override string GetEnterRoomMessage()
    {
        return "You have entered the Farmland. Below are the current stats:\n" +
               $"Farmlands: {FarmlandAmount}\n" +
               $"Free farmlands: {PossibleFarmland - FarmlandAmount}\n\n" +
               "Available actions:\n" +
               " - build farmland           : Build a new farmland\n" +
               " - cut forest               : Cut 5 trees to make freeland (reduces sustainability)\n" +
               " - plant farmland           : Plant on your farmland\n" +
               " - farm                     : Farm your planted farmland\n\n" +
               "Type a command to perform the action.\n";
    }

    public string BuildFarmland(Resources resources)
    {
        if (resources.Wood >= 5 && FarmlandAmount < PossibleFarmland)
        {
            FarmlandAmount += 1;
            resources.Wood -= 5;
            return $"You have built a new farmland. Now you have: {FarmlandAmount} farmlands.";
        }
        else if (resources.Wood < 5 && FarmlandAmount == PossibleFarmland)
        {
            return "You dont have enough wood and freeland to build farmland!!";
        }
        else if (resources.Wood < 5)
        {
            return "You dont have enough wood to build farmland!!";
        }
        else
        {
            return "You dont have enough freeland to build farmland!!";
        }
    }

    public (string message, int sustainabilityChange) CutForest(Resources resources)
    {
        if (PossibleFarmland > FarmlandAmount)
        {
            return ("There is freeland no need to cut more trees for now.", 0);
        }

        if (resources.Trees <= 0)
        {
            return ("No trees left to cut.", 0);
        }

        resources.Trees -= 5;
        resources.Wood += 10;
        PossibleFarmland += 1;

        return ("You now have space for 1 more farmland.", -10);
    }

    public (string message, int sustainabilityChange) PlantFarmland(Resources resources)
    {
        if (FarmlandPlanted.Count < FarmlandAmount && resources.GrainSeeds >= 4)
        {
            FarmlandPlanted.Add(DataHandler.TotalTurns);
            resources.GrainSeeds -= 4;

            return ($"You have planted 1 more farmland.\nNow you have {FarmlandPlanted} planted farmlands.", 8);
        }
        else
        {
            if (FarmlandPlanted.Count < FarmlandAmount && resources.GrainSeeds >= 4)
            {
                

                return ("All your farmlands are planted and you dont have enough Grain seeds to plant a farmland", 0);
            }
            else if(FarmlandPlanted.Count == FarmlandAmount)
            {
                return ("All your farmlands are planted.", 0);
            }
            else
            {
                return ("You dont have enough Grain seeds to plant a farmland", 0);
            }
        }
    }

    public (string message, int sustainabilityChange) Harvest(Resources resources)
    {
        if (FarmlandRipped > 0)
        {
            FarmlandRipped -= 1;
            resources.GrainSeeds += 1;
            resources.Food += 4;
            
            return ($"Now you have {FarmlandRipped} ripped farmlands.", -4);
        }
        else
        {
            return ("None of your farmlands are ripped.", 0);
        }
    }

    public void RipenFarmland()
    {
        foreach (var farmland in FarmlandPlanted.Where(farmland => DataHandler.TotalTurns - farmland >= 4).ToList())
        {
            FarmlandRipped += 1;
            FarmlandPlanted.Remove(farmland);
        }
    }
}