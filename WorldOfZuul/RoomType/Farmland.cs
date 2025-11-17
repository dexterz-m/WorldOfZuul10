namespace WorldOfZuul.RoomType
{
    public class Farmland : Room
    {
        int FarmlandAmount {  get; set; }

        int FarmlandPlanted { get; set; } = 0;
        
        int PossibleFarmland { get; set; } = 1; 


        public Farmland(string shortDesc, string longDesc) : base(shortDesc, longDesc)
        {
            FarmlandAmount = 1;
        }

        public override void EnterRoom()
        {
            Console.WriteLine("You have entered the Farmland. Below are the current stats:");
            Console.WriteLine($"Farmlands: {FarmlandAmount}");
            Console.WriteLine();

            Console.WriteLine("Available actions:");
            Console.WriteLine(" - build farmland           : Build a new farmland");
            Console.WriteLine(" - cut forest               : Cut 5 trees to make freeland (reduces sustainability)");
            Console.WriteLine(" - plant farmland           : Plant on your farmland");
            Console.WriteLine(" - farm                     : Farm your planted farmland");
            Console.WriteLine();
            Console.WriteLine("Type a command to perform the action.");
            Console.WriteLine();
        }
        
        public override void CommandList(Command command)
        {
            switch (command.Name)
            {
                case "build":
                    if (command.SecondWord == "farmland")
                        BuildFarmland();
                    else
                        Console.WriteLine("Build what?");
                    break;
                case "cut":
                    if (command.SecondWord == "forest")
                        CutForest();
                    else
                        Console.WriteLine("Cut what?");
                    break;
                case "farm":
                    Farm();
                    break;
                case "plant":
                    if (command.SecondWord == "farmland")
                        PlantFarmland();
                    else
                        Console.WriteLine("Plant what?");
                    break;
                default:
                    Console.WriteLine("Invalid command in the farmland.");
                    break;
            }
        }

        public void BuildFarmland()
        {
            if (Game.Resources.Wood >= 5 && FarmlandAmount < PossibleFarmland)
            {
                FarmlandAmount += 1;
                Console.WriteLine($"You have built a new farmland. Now you have: {FarmlandAmount} farmlands.");
            }
            else if(Game.Resources.Wood < 5 && FarmlandAmount == PossibleFarmland)
            {
                Console.WriteLine("You dont have enough wood and freeland to build farmland!!");
            }
            else if(Game.Resources.Wood < 5)
            {
                Console.WriteLine("You dont have enough wood to build farmland!!");
            }
            else
            {
                Console.WriteLine("You dont have enough freeland to build farmland!!");
            }

        }

        public void CutForest()
        {
            if (PossibleFarmland > FarmlandAmount)
            {
                Console.WriteLine("There is freeland no need to cut more trees for now.");
                return;
            }
            
            if (Game.Resources.Trees <= 0)
            {
                Console.WriteLine("No trees left to cut.");
                return;
            }
            
            Game.Resources.Trees = -5;
            Game.Resources.Wood = 10;
            Game.SustainabilityPoints -= 10;
            PossibleFarmland += 1;

            Console.WriteLine("You now have space for 1 more farmland.");
            
            Console.WriteLine($"Sustainability Points: {Game.SustainabilityPoints}");
        }

        public void PlantFarmland()
        {
            if(FarmlandPlanted < FarmlandAmount && Game.Resources.GrainSeeds >= 4)
            {
                FarmlandPlanted += 1;
                Game.Resources.GrainSeeds = -4;
                //Console.WriteLine($"Garainseeds: {Game.Resources.GrainSeeds}");
                Game.SustainabilityPoints += 8;
                
                Console.WriteLine("You have planted 1 more farmland.");
                Console.WriteLine($"Now you have {FarmlandPlanted} planted farmlands.");
            }
            else
            {
                if (FarmlandPlanted == FarmlandAmount && Game.Resources.GrainSeeds < 4)
                {
                    Console.WriteLine("All your farmlands are planted and you dont have enough Grain seeds to plant a farmland");
                }
                else if(FarmlandPlanted == FarmlandAmount)
                {
                    Console.WriteLine("All your farmlands are planted.");
                }
                else
                {
                    Console.WriteLine("You dont have enough Grain seeds to plant a farmland");
                }
                
            }
        }

        public void Farm()
        {

            if (FarmlandPlanted > 0)
            {
                FarmlandPlanted -= 1;
                Game.Resources.GrainSeeds = 1;
                //Console.WriteLine($"Garainseeds: {Game.Resources.GrainSeeds}");
                Game.Resources.Food = 4;
                Game.SustainabilityPoints -= 4;
                Console.WriteLine($"Now you have {FarmlandPlanted} planted farmlands.");
            }
            else
            {
                Console.WriteLine("None of your farmlands are planted.");
            }
        }
        
        
        
    }
        
        
}

