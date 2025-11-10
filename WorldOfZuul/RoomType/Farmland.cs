namespace WorldOfZuul.RoomType
{
    public class Farmland : Room
    {
        int FarmlandAmount {  get; set; }
        
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
            Console.WriteLine(" - build-farmland           : Build a new farmland");
            Console.WriteLine(" - cut forest for farmland  : Convert forest to farmland (reduces sustainability)");
            Console.WriteLine();
            Console.WriteLine("Type a command to perform the action.");
            Console.WriteLine();
        }
        
        public override void CommandList(Command command)
        {
            switch (command.Name)
            {
                case "build-farmland":
                    BuildFarmland();
                    break;
                case "cut-tree":
                    CutForest();
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
            
            Game.Resources.Trees -= 5;
            SustainabilityPoints -= 10;
            PossibleFarmland += 1;

            Console.WriteLine("You now have space for 1 more farmland.");
            
            Console.WriteLine($"Sustainability Points: {SustainabilityPoints}");
        }
    }
        
        
}

